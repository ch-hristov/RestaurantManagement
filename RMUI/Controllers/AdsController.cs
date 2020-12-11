using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdsController : Controller
    {
        private readonly IPersonData _people;

        public AdsController(IPersonData people)
        {
            _people = people;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _people.GetAds();
            var latest = model.OrderByDescending(x => x.CreateDate).FirstOrDefault();
            AdDisplayModel vm = new AdDisplayModel();


            if (latest != null)
            {
                vm = new AdDisplayModel
                {
                    Ad1 = latest?.Ad1,
                    Ad2 = latest?.Ad2,
                    Ad3 = latest?.Ad3,
                    Id = latest.Id,
                    CreateDate = latest.CreateDate
                };
            }


            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Index(AdsModel model)
        {
            if (ModelState.IsValid)
                await _people.InsertAds(model);

            return await Index();
        }


        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
