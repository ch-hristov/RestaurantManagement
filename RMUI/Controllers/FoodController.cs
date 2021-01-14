﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Manager")]
    public class FoodController : Controller
    {
        private readonly IFoodData _food;
        private readonly IPersonData _people;
        private readonly IDiningTableData _table;
        private readonly IOrderData _order;
        private readonly IStringLocalizer<FoodController> _localizer;

        public FoodController(IFoodData food, 
                              IPersonData people, 
                              IDiningTableData table, 
                              IOrderData order,
                              IStringLocalizer<FoodController> localizer)
        {
            _food = food;
            _people = people;
            _table = table;
            _order = order;
            _localizer = localizer;
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
                    FoodType = _localizer[fType.FoodType],
                };

                await _food.InsertFoodType(newFoodType);

                return RedirectToAction("ViewFoodTypes");
            }
            return View();
        }

        public async Task<IActionResult> BlockFood(int id, bool status)
        {
            var foundFood = await _food.GetFoodById(id);

            foundFood.IsBlocked = status;

            await _food.UpdateFood(foundFood);

            return RedirectToAction(nameof(ViewFoods));

        }

        public async Task<IActionResult> PromoFood(int id, bool status)
        {
            var foundFood = await _food.GetFoodById(id);

            foundFood.IsPromo = status;

            await _food.UpdateFood(foundFood);

            return RedirectToAction(nameof(ViewFoods));

        }

        public async Task<IActionResult> ViewFoodTypes()
        {
            var results = await _food.GetAllFoodTypes();

            List<FoodTypeDisplayModel> foodTypes = new List<FoodTypeDisplayModel>();
            foreach (var type in results)
            {
                foodTypes.Add(new FoodTypeDisplayModel
                {
                    Id = type.Id,
                    FoodType = _localizer[type.FoodType]
                });
            }

            return View(foodTypes);
        }


        // Insert food into database
        public async Task<IActionResult> InsertFood(FoodDisplayModel food)
        {
            if (ModelState.IsValid)
            {
                var newFood = new FoodModel
                {
                    FoodType = _localizer[food.FoodType],
                    FoodName = _localizer[food.FoodName],
                    Price = food.Price,
                    DisplayPhoto1 = food.VisualUrl1 ?? "",
                    DisplayPhoto2 = food.VisualUrl2 ?? "",
                    ItemDescription = food.ItemDescription,
                    IsBlocked = food.IsBlocked,
                    IsPromo = food.IsPromo
                };
                var typeId = int.Parse(food.FoodType);

                newFood.TypeId = typeId;

                var types = await _food.GetAllFoodTypes();
                var typeName = types.FirstOrDefault(x => x.Id == typeId).FoodType;
                newFood.FoodType = typeName;

                await _food.InsertFood(newFood);

                return RedirectToAction("ViewFoods");
            }

            var tables = await GetAllTables();
            ViewBag.TableList = tables;

            var servers = await GetAllServers();
            ViewBag.ServerList = servers;

            var foods = await GetFoodTypes();
            ViewBag.FoodTypeList = foods;

            return View();
        }

        // Get all Dining Tables and populate as dropdown list items
        public async Task<List<SelectListItem>> GetAllTables()
        {
            var tables = await _table.GetAllTables();
            var list = new List<SelectListItem>();

            foreach (var table in tables)
            {
                list.Add(new SelectListItem
                {
                    Text = table.TableNumber.ToString(),
                    Value = table.Id.ToString()
                });
            }

            return list;
        }


        // Get all Servers and populate as dropdown list items
        public async Task<List<SelectListItem>> GetAllServers()
        {
            var persons = await _people.GetAllPeople();
            var list = new List<SelectListItem>();

            foreach (var person in persons)
            {
                list.Add(new SelectListItem
                {
                    Text = person.FullName,
                    Value = person.Id.ToString()
                });
            }

            return list;
        }


        // Get all Food Types and populate as dropdown list items
        public async Task<List<SelectListItem>> GetFoodTypes()
        {
            var foodTypes = await _food.GetAllFoodTypes();
            var list = new List<SelectListItem>();

            foreach (var foodType in foodTypes)
            {
                list.Add(new SelectListItem
                {
                    Text = _localizer[foodType.FoodType],
                    Value = foodType.Id.ToString()
                });
            }

            return list;
        }

        // View all foods as as list
        public async Task<IActionResult> ViewFoods()
        {
            var allFoods = await _food.GetAllFoods();

            var foods = new List<FoodDisplayModel>();

            foreach (var food in allFoods)
            {
                foods.Add(new FoodDisplayModel
                {
                    Id = food.Id,
                    FoodType = _localizer[food.FoodType],
                    FoodName = _localizer[food.FoodName],
                    Price = food.Price,
                    TypeId = food.TypeId,
                    IsBlocked = food.IsBlocked,
                    IsPromo = food.IsPromo,
                    ItemDescription = food.ItemDescription,
                    VisualUrl2 = food.DisplayPhoto2,
                    VisualUrl1 = food.DisplayPhoto1
                });
            }

            return View(foods);
        }


        // Edit food with database Id = id
        public async Task<IActionResult> EditFood(int id)
        {
            FoodModel foundFood = await _food.GetFoodById(id);

            FoodDisplayModel food = new FoodDisplayModel
            {
                Id = id,
                FoodType = foundFood.FoodType,
                FoodName = foundFood.FoodName,
                Price = foundFood.Price,
                TypeId = foundFood.TypeId,
                IsPromo = foundFood.IsPromo,
                VisualUrl1 = foundFood.DisplayPhoto1,
                VisualUrl2 = foundFood.DisplayPhoto2,
                ItemDescription = foundFood.ItemDescription,
                IsBlocked = foundFood.IsBlocked
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
                TypeId = food.TypeId,
                ItemDescription = food.ItemDescription,
                DisplayPhoto1 = food.VisualUrl1,
                DisplayPhoto2 = food.VisualUrl2,
                IsPromo = food.IsPromo,
                IsBlocked = food.IsBlocked
            };

            await _food.UpdateFood(updatedFood);

            return RedirectToAction("ViewFoods");
        }


        // Delete food with database Id = id
        public async Task<IActionResult> DeleteFood(int id)
        {
            await _food.DeleteFood(id);

            return RedirectToAction("ViewFoods");
        }


        public async Task<IActionResult> DeleteFoodType(int id)
        {
            await _food.DeleteFoodType(id);

            return RedirectToAction("ViewFoodTypes");
        }
    }
}