using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IFoodData
    {
        Task<List<FoodModel>> GetAllFoods();
        Task<List<FoodTypeModel>> GetAllFoodTypes();
        Task InsertFoodType(FoodTypeModel foodType);
        Task<List<FoodModel>> GetFoodByTypeId(int typeId);
        Task<FoodModel> GetFoodById(int id);
        Task<int> GetTypeIdByFoodType(string foodType);
        Task<FoodModel> GetFoodByName(string foodName);
        Task InsertFood(FoodModel food);
        Task UpdateFood(FoodModel food);
        Task DeleteFood(int id);
        Task DeleteFoodType(int id);
    }
}