using System.Data.Common;

namespace Share;

public static class ReaderExtension
{
    #region Private
    private static bool IsDBNull(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.IsDBNull(ordinal);
    }
    #endregion

    #region Varchar
    public static string GetVarchar(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetString(ordinal);
    }

    public static string? GetVarcharNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetVarchar(dataReader, columnName);
    }
    #endregion

    #region Text
    public static string GetText(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetString(ordinal);
    }

    public static string? GetTextNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetText(dataReader, columnName);
    }
    #endregion

    #region Integer
    public static int GetInteger(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetInt32(ordinal);
    }

    public static int? GetIntegerNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetInteger(dataReader, columnName);
    }
    #endregion

    #region Numeric
    public static decimal GetNumeric(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetDecimal(ordinal);
    }

    public static decimal? GetNumericNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetNumeric(dataReader, columnName);
    }
    #endregion

    #region BigInt
    public static long GetBigInt(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetInt64(ordinal);
    }

    public static long? GetBigIntNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetBigInt(dataReader, columnName);
    }
    #endregion

    #region UUID
    public static Guid GetUUID(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetGuid(ordinal);
    }

    public static Guid? GetUUIDNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetUUID(dataReader, columnName);
    }
    #endregion

    #region Date
    public static DateOnly GetDate(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetFieldValue<DateOnly>(ordinal);
    }

    public static DateOnly? GetDateNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetDate(dataReader, columnName);
    }
    #endregion

    #region TimeStamp
    public static DateTime GetTimeStamp(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetDateTime(ordinal);
    }

    public static DateTime? GetTimeStampNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetTimeStamp(dataReader, columnName);
    }
    #endregion

    #region Boolean
    public static bool GetBoolean(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetBoolean(ordinal);
    }

    public static bool? GetBooleanNullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetBoolean(dataReader, columnName);
    }
    #endregion

    #region Enum
    public static T GetEnum<T>(this DbDataReader dataReader, string columnName) where T : notnull, Enum
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetFieldValue<T>(ordinal);
    }

    public static Nullable<T> GetEnumNullable<T>(this DbDataReader dataReader, string columnName) where T : struct, Enum
    {
        if (IsDBNull(dataReader, columnName))
        {
            return null;
        }
        else
        {
            var ordinal = dataReader.GetOrdinal(columnName);
            return dataReader.GetFieldValue<T>(ordinal);
        }        
    }
    #endregion

    #region JsonB
    public static T GetJsonB<T>(this DbDataReader dataReader, string columnName) where T : notnull
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetFieldValue<T>(ordinal);
    }

    public static T? GetJsonBNullable<T>(this DbDataReader dataReader, string columnName) where T : class
    {
        if (IsDBNull(dataReader, columnName))
        {
            return null;
        }
        else
        {
            var ordinal = dataReader.GetOrdinal(columnName);
            return dataReader.GetFieldValue<T>(ordinal);
        }
    }
    #endregion

    #region ByteA
    public static byte[] GetByteA(this DbDataReader dataReader, string columnName)
    {
        var ordinal = dataReader.GetOrdinal(columnName);
        return dataReader.GetFieldValue<byte[]>(ordinal);
    }

    public static byte[]? GetByteANullable(this DbDataReader dataReader, string columnName)
    {
        if (IsDBNull(dataReader, columnName))
            return null;
        else
            return GetByteA(dataReader, columnName);
    }
    #endregion
}