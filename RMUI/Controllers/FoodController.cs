using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Manager")]
    public class FoodController : Controller
    {
        private readonly IFoodData _data;

        public FoodController(IFoodData data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> InsertFoodType(FoodTypeDisplayModel fType)
        {
            if (ModelState.IsValid)
            {
                FoodTypeModel newFoodType = new FoodTypeModel
                {
                    FoodType = fType.FoodType,
                };

                await _data.InsertFoodType(newFoodType);

                return RedirectToAction("ViewFoodTypes");
            }
            return View();
        }


        public async Task<IActionResult> ViewFoodTypes()
        {
            var results = await _data.GetAllFoodTypes();

            List<FoodTypeDisplayModel> foodTypes = new List<FoodTypeDisplayModel>();
            foreach (var type in results)
            {
                foodTypes.Add(new FoodTypeDisplayModel
                {
                    Id = type.Id,
                    FoodType = type.FoodType
                });
            }

            return View(foodTypes);
        }


        // Insert food into database
        public async Task<IActionResult> InsertFood(FoodDisplayModel food)
        {
            if (ModelState.IsValid)
            {
                FoodModel newFood = new FoodModel
                {
                    FoodType = food.FoodType,
                    FoodName = food.FoodName,
                    Price = food.Price
                };

                int typeId = await _data.GetTypeIdByFoodType(newFood.FoodType);

                newFood.TypeId = typeId;

                await _data.InsertFood(newFood);

                return RedirectToAction("ViewFoods");
            }

            return View();
        }

        // View all foods as as list
        public async Task<IActionResult> ViewFoods()
        {
            var allFoods = await _data.GetAllFoods();

            List<FoodDisplayModel> foods = new List<FoodDisplayModel>();

            foreach (var food in allFoods)
            {
                foods.Add(new FoodDisplayModel
                {
                    Id = food.Id,
                    FoodType = food.FoodType,
                    FoodName = food.FoodName,
                    Price = food.Price,
                    TypeId = food.TypeId
                });
            }

            return View(foods);
        }

      
        // Edit food with database Id = id
        public async Task<IActionResult> EditFood(int id)
        {
            FoodModel foundFood = await _data.GetFoodById(id);

            FoodDisplayModel food = new FoodDisplayModel
            {
                Id = id,
                FoodType = foundFood.FoodType,
                FoodName = foundFood.FoodName,
                Price = foundFood.Price,
                TypeId = foundFood.TypeId
            };

            return View(food);
        }


        // Update food info
        public async Task<IActionResult> UpdateFood(FoodDisplayModel food)
        {
            FoodModel updatedFood = new FoodModel
            {
                Id = food.Id,
                FoodType = food.FoodType,
                FoodName = food.FoodName,
                Price = food.Price,
                TypeId = food.TypeId
            };

            await _data.UpdateFood(updatedFood);

            return RedirectToAction("ViewFoods");
        }


        // Delete food with database Id = id
        public async Task<IActionResult> DeleteFood(int id)
        {
            await _data.DeleteFood(id);

            return RedirectToAction("ViewFoods");
        }


        public async Task<IActionResult> DeleteFoodType(int id)
        {
            await _data.DeleteFoodType(id);

            return RedirectToAction("ViewFoodTypes");
        }
    }
}