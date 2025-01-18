namespace Fitnez;

public class PersonRepository : Repository
{
    #region Members
    private readonly ILogger<PersonRepository> _logger;
    #endregion

    #region Constructor
    public PersonRepository(ILogger<PersonRepository> logger)
    { 
        _logger = logger;
    }
    #endregion

    #region Public
    public async Task<Result> CreateAsync(IDataSource dataSource, Person person)
    {
        const string sql = @"
            INSERT INTO public.person
            (
                id,
                name,
                surname
                gender,
                date_of_birth,
                id_passport,
                address,
                is_trainer,
                status
            )
            VALUES
            (
                $1, $2, $3, $4, $5, $6, $7, $8, $9
            );
        ";

        try
        {
            var cmd = Command(dataSource, sql);
            cmd.Parameters.AddUUID(person.Id);
            cmd.Parameters.AddVarchar(person.Name);
            cmd.Parameters.AddVarchar(person.Surname);
            cmd.Parameters.AddEnum<Gender>(person.Gender);
            cmd.Parameters.AddDate(person.DateOfBirth);
            cmd.Parameters.AddVarchar(person.IdPassport);
            cmd.Parameters.AddText(person.Address);
            cmd.Parameters.AddBoolean(person.IsTrainer);
            cmd.Parameters.AddEnum<PersonStatus>(PersonStatus.Inactive);

            await cmd.ExecuteNonQueryAsync();
            return new Result();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository Create failed");
            return new Result("Unable to create Person record.", "Person.Repository");
        }        
    }

    public async Task<Result<PersonRead>> ReadAsync(IDataSource dataSource, Guid id)
    {
        const string sql = @"
            SELECT
                id,
                name,
                surname,
                gender,
                date_of_birth
                id_passport,
                address,
                is_trainer,
                status
            FROM public.person
            WHERE id = $1;
        ";

        try
        {
            var cmd = Command(dataSource, sql);
            cmd.Parameters.AddUUID(id);

            var reader = await cmd.ExecuteReaderAsync();
            if (reader != null && reader.Read()) 
            {
                var person = new PersonRead
                {
                    Id = reader.GetUUID("id"),
                    Name = reader.GetVarchar("name"),
                    Surname = reader.GetVarchar("surname"),
                    Gender = reader.GetEnum<Gender>("gender"),
                    DateOfBirth = reader.GetDate("date_of_birth"),
                    IdPassport = reader.GetVarchar("id_passport"),
                    Address = reader.GetText("address"),
                    IsTrainer = reader.GetBoolean("is_trainer"),
                    Status = reader.GetEnum<PersonStatus>("status"),
                };

                return new Result<PersonRead>(person);
            }
            else
            {
                return new Result<PersonRead>("Person record not found.", "Person.Repository");
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository Read failed");
            return new Result<PersonRead>("Unable to read Person record.", "Person.Repository");
        }
    }

    public async Task<Result> UpdateAsync(IDataSource dataSource, Person person)
    {
        const string sql = @"
            UPDATE public.person
            SET name = $1,
                surname = $2
                gender = $3,
                date_of_birth = $4,
                id_passport = $5,
                address = $6,
                is_trainer = $7
            WHERE id = $8;            
        ";

        try
        {
            var cmd = Command(dataSource, sql);
            
            cmd.Parameters.AddVarchar(person.Name);
            cmd.Parameters.AddVarchar(person.Surname);
            cmd.Parameters.AddEnum<Gender>(person.Gender);
            cmd.Parameters.AddDate(person.DateOfBirth);
            cmd.Parameters.AddVarchar(person.IdPassport);
            cmd.Parameters.AddText(person.Address);
            cmd.Parameters.AddBoolean(person.IsTrainer);

            cmd.Parameters.AddUUID(person.Id);

            await cmd.ExecuteNonQueryAsync();
            return new Result();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository Update failed");
            return new Result("Unable to update Person record.", "Person.Repository");
        }
    }

    public async Task<Result> DeleteAsync(IDataSource dataSource, Guid id)
    {
        const string sql = @"
            UPDATE public.person
            SET status = $1
            WHERE id = $2;
        ";

        try
        {
            var cmd = Command(dataSource, sql);

            cmd.Parameters.AddEnum<PersonStatus>(PersonStatus.Deleted);
            cmd.Parameters.AddUUID(id);

            await cmd.ExecuteNonQueryAsync();
            return new Result();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository Delete failed");
            return new Result("Unable to delete Person record.", "Person.Repository");
        }
    }

    public async Task<Result> ActivateAsync(IDataSource dataSource, Guid id)
    {
        const string sql = @"
            UPDATE public.person
            SET status = $1
            WHERE id = $2;
        ";

        try
        {
            var cmd = Command(dataSource, sql);

            cmd.Parameters.AddEnum<PersonStatus>(PersonStatus.Active);
            cmd.Parameters.AddUUID(id);

            await cmd.ExecuteNonQueryAsync();
            return new Result();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository Activate failed");
            return new Result("Unable to activate Person record.", "Person.Repository");
        }
    }

    public async Task<Result> DeactivateAsync(IDataSource dataSource, Guid id)
    {
        const string sql = @"
            UPDATE public.person
            SET status = $1
            WHERE id = $2;
        ";

        try
        {
            var cmd = Command(dataSource, sql);

            cmd.Parameters.AddEnum<PersonStatus>(PersonStatus.Inactive);
            cmd.Parameters.AddUUID(id);

            await cmd.ExecuteNonQueryAsync();
            return new Result();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository Deactivate failed");
            return new Result("Unable to deactivate Person record.", "Person.Repository");
        }
    }

    public async Task<Result> OnHoldAsync(IDataSource dataSource, Guid id)
    {
        const string sql = @"
            UPDATE public.person
            SET status = $1
            WHERE id = $2;
        ";

        try
        {
            var cmd = Command(dataSource, sql);

            cmd.Parameters.AddEnum<PersonStatus>(PersonStatus.OnHold);
            cmd.Parameters.AddUUID(id);

            await cmd.ExecuteNonQueryAsync();
            return new Result();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository OnHold failed");
            return new Result("Unable to put Person record on hold.", "Person.Repository");
        }
    }

    public async Task<Result<List<PersonRead>>> ListAsync(IDataSource dataSource)
    {
        const string sql = @"
            SELECT
                id,
                name,
                surname,
                gender,
                date_of_birth
                id_passport,
                address,
                is_trainer,
                status
            FROM public.person
            ORDER BY surname, name;
        ";

        try
        {
            var records = new List<PersonRead>();

            var cmd = Command(dataSource, sql);            
            var reader = await cmd.ExecuteReaderAsync();
            if (reader != null)
            {                
                while (reader.Read())
                {
                    var person = new PersonRead
                    {
                        Id = reader.GetUUID("id"),
                        Name = reader.GetVarchar("name"),
                        Surname = reader.GetVarchar("surname"),
                        Gender = reader.GetEnum<Gender>("gender"),
                        DateOfBirth = reader.GetDate("date_of_birth"),
                        IdPassport = reader.GetVarchar("id_passport"),
                        Address = reader.GetText("address"),
                        IsTrainer = reader.GetBoolean("is_trainer"),
                        Status = reader.GetEnum<PersonStatus>("status"),
                    };

                    records.Add(person);
                }               
            }

            return new Result<List<PersonRead>>(records);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Person Repository List failed");
            return new Result<List<PersonRead>>("Unable to list Persons records.", "Person.Repository");
        }
    }
    #endregion
}