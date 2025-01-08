using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Share;

public class RuleBuilder
{
    #region Members
    private readonly ILogger<RuleBuilder> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly List<IRule> _rules;
    private bool _evaluated = false;
    #endregion

    #region Constructor    
    public RuleBuilder(ILogger<RuleBuilder> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _rules = [];
    }
    #endregion

    #region Public    
    public void AddRule(IRule rule)
    {
        _rules.Add(rule);
    }

    public TRule GetRule<TRule>()
    {
        var rule = _serviceProvider.GetService<TRule>();
        if (rule is not null)
        {
            return rule;
        }
        else
        {
            var exception = new Exception($"Rule {nameof(TRule)} not found");
            _logger.LogError(exception, "RuleBuilder GetRule Failed.");
            throw exception;
        }
    }

    public bool IsValid
    {
        get
        {
            if (!_evaluated)
            {
                var exception = new Exception("Rules not Evaluated");
                _logger.LogError(exception, "RuleBuilder IsValid Failed.");
                throw exception;
            }
            
            return !_rules.Any(w => w.IsValid == false);
        }
    }

    public List<Error> Errors
    {
        get
        {
            var errors = new List<Error>();

            foreach (var rule in _rules.Where(w => w.IsValid == false))
            {
                if (rule.Error is not null)
                {
                    errors.Add(rule.Error);
                }
                else
                {
                    var exception = new Exception("InValid Rule must have a error");
                    _logger.LogError(exception, "RuleBuilder Getting Errors Failed.");
                    throw exception;
                }
            }

            return errors;
        }
    }

    public async Task EvaluateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var rule in _rules)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    await rule.EvaluateAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "RuleBuilder EvaluateAsync Failed.");
                    rule.Error = new Error { Message = $"Rule [{rule.GetType().Name}] execution failed! [{exception.Message}]", Context = "Global.Exception" };
                }
            }
        }
        catch (Exception exception)
        {
            // We need to create dummy rule if something went wrong
            var rule = new ValidRuleBuilderRule { Message = exception.Message };
            AddRule(rule);
        }
        finally
        {
            _evaluated = true;
        }
    }
    #endregion
}