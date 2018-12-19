using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace CMOV_Stocks {
    public class CompanyDatabase {
        readonly SQLiteAsyncConnection database;
        public static int connected = 0;

        public CompanyDatabase(string dbPath) {
            try {
                database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<Company>().Wait();
                connected = 1;
            } catch (System.Exception e) {
                connected = 2;
            }
        }

        public Task<List<Company>> GetItemsAsync() {
            return database.Table<Company>().ToListAsync();
        }

        public Task<List<Company>> GetItemsNotDoneAsync() {
            return database.QueryAsync<Company>("SELECT * FROM [Companies] WHERE [Done] = 0");
        }

        public Task<Company> GetItemAsync(int id) {
            return database.Table<Company>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Company item) {
            if (item.ID != 0)
                return database.UpdateAsync(item);
            else
                return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Company item) {
            return database.DeleteAsync(item);
        }
    }
}
