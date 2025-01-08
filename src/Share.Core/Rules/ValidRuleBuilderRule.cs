namespace Share;

public class ValidRuleBuilderRule : Rule, IRule
{
    #region IRule    
    public Task EvaluateAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
    #endregion

    #region Public  
    public string Message
    {
        set
        {
            Error = new Error { Message = $"RuleBuilder failed Evaluating rules [{value}]", Context = "Global.Exception" };
        }
    }
    #endregion
}