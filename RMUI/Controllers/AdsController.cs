using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdsController : Controller
    {
        private readonly IPersonData _people;
        private readonly IStringLocalizer<AdsController> _localizer;

        public AdsController(IPersonData people, IStringLocalizer<AdsController> localizer)
        {
            _people = people;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _people.GetAds();
            var latest = model.OrderByDescending(x => x.CreateDate).FirstOrDefault();
            AdDisplayModel vm = new AdDisplayModel();

            ViewBag.DisableAdLabel = _localizer["Disable Ad"];
            ViewBag.ModifyAdsLabel = _localizer["Modify Ads"];


            if (latest != null)
            {
                vm = new AdDisplayModel
                {
                    Ad1 = latest?.Ad1,
                    Ad2 = latest?.Ad2,
                    Ad3 = latest?.Ad3,
                    Id = latest.Id,
                    CreateDate = latest.CreateDate,
                    Ad1Blocked = latest.Ad1Blocked,
                    Ad2Blocked = latest.Ad2Blocked,
                    Ad3Blocked = latest.Ad3Blocked
                };
            }


            return View(vm);
        }

        [HttpGet]
        public async Task<JsonResult> GetAds()
        {
            var model = await _people.GetAds();
            var latest = model.OrderByDescending(x => x.CreateDate).FirstOrDefault();
            var vm = new AdDisplayModel();

            if (latest != null)
            {
                vm = new AdDisplayModel
                {
                    Ad1 = latest?.Ad1,
                    Ad2 = latest?.Ad2,
                    Ad3 = latest?.Ad3,
                    Id = latest.Id,
                    CreateDate = latest.CreateDate,
                    Ad1Blocked = latest.Ad1Blocked,
                    Ad2Blocked = latest.Ad2Blocked,
                    Ad3Blocked = latest.Ad3Blocked,

                };
            }


            return Json(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Index(AdDisplayModel model)
        {
            if (ModelState.IsValid)
            {

                await _people.InsertAds(new AdsModel()
                {
                    Ad1 = model.Ad1,
                    Ad2 = model.Ad2,
                    Ad3 = model.Ad3,
                    Ad1Blocked = model.Ad1Blocked,
                    Ad2Blocked = model.Ad2Blocked,
                    Ad3Blocked = model.Ad3Blocked,
                    CreateDate = model.CreateDate
                });
            }

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
