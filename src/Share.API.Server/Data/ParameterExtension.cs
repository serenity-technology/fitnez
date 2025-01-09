using Npgsql;
using NpgsqlTypes;

namespace Share;

public static class ParameterExtension
{
    public static void AddVarchar(this NpgsqlParameterCollection parameters, string? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Varchar, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<string>() { NpgsqlDbType = NpgsqlDbType.Varchar, TypedValue = value });
    }

    public static void AddText(this NpgsqlParameterCollection parameters, string? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Text, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<string>() { NpgsqlDbType = NpgsqlDbType.Text, TypedValue = value });
    }

    public static void AddInteger(this NpgsqlParameterCollection parameters, int? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Integer, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<int>() { NpgsqlDbType = NpgsqlDbType.Integer, TypedValue = value.Value });
    }

    public static void AddNumeric(this NpgsqlParameterCollection parameters, decimal? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Numeric, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<decimal>() { NpgsqlDbType = NpgsqlDbType.Numeric, TypedValue = value.Value });
    }

    public static void AddBigInt(this NpgsqlParameterCollection parameters, long? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Bigint, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<long>() { NpgsqlDbType = NpgsqlDbType.Bigint, TypedValue = value.Value });
    }

    public static void AddJsonB<T>(this NpgsqlParameterCollection parameters, T value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Jsonb, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<T>() { NpgsqlDbType = NpgsqlDbType.Jsonb, TypedValue = value });
    }

    public static void AddUUID(this NpgsqlParameterCollection parameters, Guid? value)
    {
        if (value is null || value == Guid.Empty)
            parameters.AddWithValue(NpgsqlDbType.Uuid, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<Guid>() { NpgsqlDbType = NpgsqlDbType.Uuid, TypedValue = value.Value });
    }

    public static void AddByteA(this NpgsqlParameterCollection parameters, byte[]? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Bytea, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<byte[]>() { NpgsqlDbType = NpgsqlDbType.Bytea, TypedValue = value });
    }

    public static void AddDate(this NpgsqlParameterCollection parameters, DateOnly? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Date, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<DateOnly>() { NpgsqlDbType = NpgsqlDbType.Date, TypedValue = value.Value });
    }

    public static void AddTimeStamp(this NpgsqlParameterCollection parameters, DateTime? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Timestamp, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<DateTime>() { NpgsqlDbType = NpgsqlDbType.Timestamp, TypedValue = value.Value });
    }

    public static void AddTimeStampTZ(this NpgsqlParameterCollection parameters, DateTime? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Timestamp, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<DateTime>() { NpgsqlDbType = NpgsqlDbType.TimestampTz, TypedValue = value.Value });
    }

    public static void AddBoolean(this NpgsqlParameterCollection parameters, bool? value)
    {
        if (value is null)
            parameters.AddWithValue(NpgsqlDbType.Boolean, DBNull.Value);
        else
            parameters.Add(new NpgsqlParameter<bool>() { NpgsqlDbType = NpgsqlDbType.Boolean, TypedValue = value.Value });
    }

    public static void AddEnum<T>(this NpgsqlParameterCollection parameters, T value)
    {
        if (value is null)
            parameters.AddWithValue(DBNull.Value);
        else
            parameters.AddWithValue(value);
    }
}