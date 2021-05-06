using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Data;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Manager,Admin,SuperAdmin")]
    public class FoodController : Controller
    {
        private readonly IFoodData _food;
        private readonly IPersonData _people;
        private readonly IDiningTableData _table;
        private readonly IOrderData _order;
        private readonly IStringLocalizer<FoodController> _localizer;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _management;
        private readonly IWebHostEnvironment hostingEnvironment;

        public FoodController(IFoodData food,
                              IPersonData people,
                              IDiningTableData table,
                              IOrderData order,
                              IStringLocalizer<FoodController> localizer,
                              ApplicationDbContext context,
                              UserManager<IdentityUser> managers,
                              IWebHostEnvironment environment)
        {
            _food = food;
            _people = people;
            _table = table;
            _order = order;
            _localizer = localizer;
            _context = context;
            _management = managers;
            hostingEnvironment = environment;
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
                var files = HttpContext.Request.Form.Files;
                var paths = new List<string>();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        var newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(hostingEnvironment.WebRootPath, "demoImages") + $@"\{newFileName}";

                        if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                        {
                            Directory.CreateDirectory(fileName);
                        }

                        // if you want to store path of folder in database
                        var PathDB = "demoImages/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        paths.Add(PathDB);

                    }
                }

                var newFood = new FoodModel
                {
                    FoodType = _localizer[food.FoodType],
                    FoodName = _localizer[food.FoodName],
                    Price = food.Price,
                    DisplayPhoto1 = paths.ElementAtOrDefault(0) ?? "",
                    DisplayPhoto2 = paths.ElementAtOrDefault(1) ?? "",
                    ItemDescription = food.ItemDescription,
                    IsBlocked = food.IsBlocked,
                    IsPromo = food.IsPromo,
                    Ingredients = ""
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
            var persons = await _management.Users.ToListAsync();
            var list = new List<SelectListItem>();

            foreach (var person in persons)
            {
                list.Add(new SelectListItem
                {
                    Text = person.Email,
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


        public async Task<IActionResult> PizzaCreator(FoodDisplayModel food)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                var paths = new List<string>();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        //Assigning Unique Filename (Guid)
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                        //Getting file Extension
                        var FileExtension = Path.GetExtension(fileName);

                        // concating  FileName + FileExtension
                        var newFileName = myUniqueFileName + FileExtension;

                        // Combines two strings into a path.
                        fileName = Path.Combine(hostingEnvironment.WebRootPath, "demoImages") + $@"\{newFileName}";

                        if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                        {
                            Directory.CreateDirectory(fileName);
                        }

                        // if you want to store path of folder in database
                        var PathDB = "demoImages/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        paths.Add(PathDB);

                    }
                }


                var newFood = new FoodModel
                {
                    FoodType = _localizer[food.FoodType],
                    FoodName = _localizer[food.FoodName],
                    Price = food.Price,
                    Ingredients = food.Ingredients,
                    DisplayPhoto1 = paths.ElementAtOrDefault(0) ?? "",
                    DisplayPhoto2 = paths.ElementAtOrDefault(1) ?? "",
                    ItemDescription = food.ItemDescription,
                    IsBlocked = food.IsBlocked,
                    IsPromo = food.IsPromo,
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
                    DisplayPhoto1 = string.Format("{0}://{1}{2}", Request.Scheme,
            Request.Host, food.DisplayPhoto1),
                    DisplayPhoto2 = string.Format("{0}://{1}{2}", Request.Scheme,
            Request.Host, food.DisplayPhoto2)
                });
            }

            return View(foods.OrderBy(x => x.FoodName));
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
                DisplayPhoto1 = string.Format("{0}://{1}/{2}", Request.Scheme,
            Request.Host, foundFood.DisplayPhoto1),
                DisplayPhoto2 = string.Format("{0}://{1}/{2}", Request.Scheme,
            Request.Host, foundFood.DisplayPhoto2),
                ItemDescription = foundFood.ItemDescription,
                IsBlocked = foundFood.IsBlocked,
                Ingredients = foundFood.Ingredients
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
                DisplayPhoto1 = food.DisplayPhoto1,
                DisplayPhoto2 = food.DisplayPhoto2,
                IsPromo = food.IsPromo,
                IsBlocked = food.IsBlocked,
                FoodDescriptionCR = food.FoodDescriptionCR,
                FoodDescriptionDE = food.FoodDescriptionDE,
                FoodDescriptionES = food.FoodDescriptionES,
                FoodDescriptionIT = food.FoodDescriptionIT,
                FoodNameCR = food.FoodNameCR,
                FoodNameDE = food.FoodNameDE,
                FoodNameES = food.FoodNameES,
                FoodNameIT = food.FoodNameIT

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