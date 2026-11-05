using SQLite;
using System.IO;

namespace Inverso_20070401
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_inverso.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<InversoResult>();
        }

        public async Task<List<InversoResult>> GetResultados()
        {
            return await _connection.Table<InversoResult>().ToListAsync();
        }

        public async Task Create(InversoResult resultado)
        {
            await _connection.InsertAsync(resultado);
        }

        public async Task Update(InversoResult resultado)
        {
            await _connection.UpdateAsync(resultado);
        }

        public async Task Delete(InversoResult resultado)
        {
            await _connection.DeleteAsync(resultado);
        }
    }
}