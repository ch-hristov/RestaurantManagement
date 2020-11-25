using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IDiningTableData
    {
        Task DeleteTable(int id);
        Task<List<int>> GetAllTableNumbers();
        Task<List<DiningTableModel>> GetAllTables();
        Task<DiningTableModel> GetTableById(int id);
        Task<DiningTableModel> GetTableByTableNumber(int tableNumber);
        Task InsertTable(DiningTableModel table);
        Task<bool> IsValidTableNumber(int tableNumber);
        Task UpdateTable(DiningTableModel table);
    }
}