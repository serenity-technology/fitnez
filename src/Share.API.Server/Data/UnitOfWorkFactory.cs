using Microsoft.Extensions.DependencyInjection;

namespace Share;

public class UnitOfWorkFactory(IServiceProvider serviceProvider)
{
    #region Members
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    #endregion

    #region Public
    public UnitOfWork Create()
    {
        var unitOfWork = _serviceProvider.GetService<UnitOfWork>() ?? throw new NullReferenceException();
        return unitOfWork;
    }
    #endregion
}