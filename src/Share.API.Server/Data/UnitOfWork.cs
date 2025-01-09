using Npgsql;

namespace Share;

public class UnitOfWork : IDataSource, IDisposable
{
    #region Members
    private readonly NpgsqlDataSource _dataSource;
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;
    #endregion

    #region Constructor    
    public UnitOfWork(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;

        _connection = _dataSource.CreateConnection();
        _connection.Open();

        _transaction = _connection.BeginTransaction();
    }
    #endregion

    #region Public
    public void Commit()
    {
        _transaction?.Commit();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }
    #endregion

    #region IDataSource
    public NpgsqlConnection Connection()
    {
        return _connection;
    }

    public NpgsqlTransaction? Transaction()
    {
        return _transaction;
    }
    #endregion

    #region IDisposable    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
    #endregion
}