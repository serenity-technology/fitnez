using Npgsql;

namespace Share;

public interface IDataSource
{
    NpgsqlConnection Connection();
    NpgsqlTransaction? Transaction();
}