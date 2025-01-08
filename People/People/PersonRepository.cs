using SQLite;
using System.Threading.Tasks;
namespace People.Models;

public class PersonRepository
{
    string _dbPath;

    public string StatusMessage { get; set; }

    private SQLiteAsyncConnection TOconn;

    private async Task Init()
    {
        if (TOconn != null)
            return;

        TOconn = new SQLiteAsyncConnection(_dbPath);
        TOconn.CreateTableAsync<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public async Task AddNewPerson(string name)
    {            
        int result = 0;
        try
        {
            await Init();

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = await TOconn.InsertAsync(new Person { Name = name });
            //result = 0;

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public async Task<List<Person>> GetAllPeople()
    {
        try
        {
            await Init();
            return await TOconn.Table<Person>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }

    internal async Task DeletePerson(Person personaAEliminar)
    {
        await Init();
        await TOconn.DeleteAsync(personaAEliminar);
    }
}
