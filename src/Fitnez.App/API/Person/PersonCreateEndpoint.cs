namespace Fitnez;

public class PersonCreateEndpoint : ApiEndpoint
{
    #region Members
    private readonly ILogger<PersonCreateEndpoint> _logger;
    private readonly UnitOfWorkFactory _unitOfWork;
    private readonly PersonRepository _repository;
    #endregion

    #region Constructor
    public PersonCreateEndpoint(ILogger<PersonCreateEndpoint> logger, UnitOfWorkFactory unitOfWork, PersonRepository repository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    #endregion

    #region Public
    public async Task<IResult> ExecuteAsync(PersonCreateRequest request, HttpRequest httpRequest, ClaimsPrincipal user)
    {
        _logger.LogInformation("{Method} {Path} {Email}", httpRequest.Method, httpRequest.Path, UserName(user));

        var response = new PersonCreateResponse();

        using var unitOfWork = _unitOfWork.Create();
        try
        {
            var result = await _repository.CreateAsync(unitOfWork, request.Person);
            if (result.Successful)
            {
                unitOfWork.Commit();
            }
            else
            {
                response.AddError(result.Error!);
                unitOfWork.Rollback();
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Create Endpoint failed");

            unitOfWork.Rollback();
            response.AddError(new Error { Message = "Creating Person failed.", Context = "Person.ApiEndpoint" });
        }

        return Ok(response);
    }
    #endregion
}