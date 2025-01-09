using Npgsql;

namespace Share;

public class DataSource : IDataSource
{
    #region Members
    private readonly NpgsqlDataSource _dataSource;
    #endregion

    #region Constructor
    public DataSource(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    #endregion

    #region IDataSource
    public NpgsqlConnection Connection()
    {
        var connection = _dataSource.CreateConnection();
        connection.Open();

        return connection;
    }

    public NpgsqlTransaction? Transaction()
    {
        return null;
    }
    #endregion
}