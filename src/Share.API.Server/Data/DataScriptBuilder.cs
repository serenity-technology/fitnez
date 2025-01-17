﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System.Reflection;

namespace Share;

public class DataScriptBuilder
{
    #region Members
    private readonly ILogger<DataScript> _logger;
    private readonly NpgsqlDataSource _dataSource;
    private readonly NpgsqlDataSource _postgresDataSource;
    #endregion

    #region Constructor
    public DataScriptBuilder
        (
            ILogger<DataScript> logger,
            [FromKeyedServices("db")] NpgsqlDataSource dataSource, 
            [FromKeyedServices("adm")] NpgsqlDataSource postgresDataSource)
    {
        _logger = logger;
        _dataSource = dataSource;
        _postgresDataSource = postgresDataSource;
    }
    #endregion

    #region Public
    public void Execute(Assembly assembly, string databaseName)
    {
        _logger.LogInformation("Preparing database {Database}", databaseName);

        // Create Database if not exists
        const string dbExistsSql = @"SELECT EXISTS(SELECT 1 FROM pg_database WHERE datname = $1);";
        var existsCmd = _postgresDataSource.CreateCommand(dbExistsSql);
        existsCmd.Parameters.Add(new NpgsqlParameter<string>() { NpgsqlDbType = NpgsqlDbType.Varchar, TypedValue = databaseName });
        var dbResult = existsCmd.ExecuteScalar();
        var dbExists = Convert.ToBoolean(dbResult);
        if (!dbExists) 
        {
            _logger.LogInformation("Create database {Database}", databaseName);

            var createDbSql = $"CREATE DATABASE {databaseName};";
            var createDbCmd = _postgresDataSource.CreateCommand(createDbSql);
            createDbCmd.ExecuteNonQuery();
        }

        // Create scripts table if not exists
        _logger.LogInformation("Prepare scripts table");

        const string scriptsTableSql = @"
            CREATE TABLE IF NOT EXISTS public.scripts
            (
                no INTEGER STORAGE PLAIN NOT NULL,
                name VARCHAR STORAGE PLAIN NOT NULL,
                CONSTRAINT scripts_pkey PRIMARY KEY(no)
            );
        ";
                
        var scriptsTableCmd = _dataSource.CreateCommand(scriptsTableSql);
        scriptsTableCmd.ExecuteNonQuery();

        // get last run script
        const string lastScriptSql = @"SELECT MAX(no) AS last_no FROM public.scripts;";
        var lastScriptCmd = _dataSource.CreateCommand(lastScriptSql);
        var lastScriptResult = lastScriptCmd.ExecuteScalar();        
        var lastScriptNo = (lastScriptResult != null && lastScriptResult != DBNull.Value) ? Convert.ToInt32(lastScriptResult) : 0;

        // load script files
        var scripts = new List<DataScript>();
        var scriptFileNames = assembly.GetManifestResourceNames();
        foreach (var scriptFileName in scriptFileNames.Where(w => w.Contains(".sql", StringComparison.OrdinalIgnoreCase)))
        {
            var parts = scriptFileName.Split('-');

            // script no
            var pathParts = parts[0].Split(".");
            var scriptNo = Convert.ToInt32(pathParts[^1]);

            // script name
            var scriptName = parts[1];

            if (scriptNo >  lastScriptNo)
            {
                using var stream = assembly.GetManifestResourceStream(scriptFileName);
                if (stream is not null)
                {
                    using StreamReader reader = new(stream);
                    var sql = reader.ReadToEnd();

                    scripts.Add(new DataScript { No = scriptNo, Name = scriptName, Sql = sql });
                }                
            }                
        }

        foreach (var script in scripts.OrderBy(o => o.No)) 
        {
        
        }
    }
    #endregion

}