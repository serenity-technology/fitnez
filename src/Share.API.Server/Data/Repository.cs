using Npgsql;

namespace Share;

public abstract class Repository
{
    #region Constructor
    public Repository()
    { }
    #endregion

    #region Protected
    protected static NpgsqlCommand Command(IDataSource dataSource, string commandText)
    {
        var command = new NpgsqlCommand(commandText, dataSource.Connection(), dataSource.Transaction());
        return command;
    }

    protected static NpgsqlBatch Batch(IDataSource dataSource)
    {
        var batch = new NpgsqlBatch(dataSource.Connection(), dataSource.Transaction());
        return batch;
    }


    protected static NpgsqlBatchCommand BatchCommand(NpgsqlBatch batch, string commandText)
    {
        var command = new NpgsqlBatchCommand(commandText);
        batch.BatchCommands.Add(command);

        return command;
    }
    #endregion
}