using RMDataLibrary.Models;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IBillData
    {
        Task InsertBill(BillModel bill);
    }
}