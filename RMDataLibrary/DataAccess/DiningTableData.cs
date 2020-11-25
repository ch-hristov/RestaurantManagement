using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class DiningTableData : IDiningTableData
    {
        private readonly ISqlDataAccess _sql;

        public DiningTableData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        // Insert DiningTable into database
        public async Task InsertTable(DiningTableModel table)
        {
            await _sql.SaveData<DiningTableModel>("DiningTable_Insert", table);
        }


        // Get all DiningTables info from database
        public async Task<List<DiningTableModel>> GetAllTables()
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("DiningTable_GetAll", new { });

            return results;
        }


        // Get all DiningTable numbers
        public async Task<List<int>> GetAllTableNumbers()
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("DiningTable_GetAll", new { });

            List<int> allTableNumbers = results.Select(x => x.TableNumber).ToList();

            return allTableNumbers;
        }


        // Get specific DiningTable info with Id = id
        public async Task<DiningTableModel> GetTableById(int id)
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("DiningTable_GetById", new { id });

            return results.FirstOrDefault();
        }


        public async Task<DiningTableModel> GetTableByTableNumber(int tableNumber)
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("DiningTable_GetByTableNumber", new { TableNumber = tableNumber });

            return results.FirstOrDefault();
        }

        // Update DiningTable info in database
        public async Task UpdateTable(DiningTableModel table)
        {
            await _sql.SaveData<DiningTableModel>("DiningTable_Update", table);
        }


        // Delete specific DiningTable from database with Id = id 
        public async Task DeleteTable(int id)
        {
            await _sql.DeleteData<dynamic>("DiningTable_Delete", new { id });
        }


        // Check whether the input tableNumber is a valid TableNumber 
        public async Task<bool> IsValidTableNumber(int tableNumber)
        {
            var tables = await GetAllTables();
            HashSet<int> tableNumbers = new HashSet<int>(tables.Select(x => x.TableNumber));
            return tableNumbers.Contains(tableNumber);
        }
    }
}
