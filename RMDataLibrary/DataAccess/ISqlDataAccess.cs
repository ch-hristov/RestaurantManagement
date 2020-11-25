using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);
        Task DeleteData<T>(string storedProcedure, T parameters);
    }
}