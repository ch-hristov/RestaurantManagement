using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Data;
using RMUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdsController : Controller
    {
        private readonly IPersonData _people;
        private readonly IStringLocalizer<AdsController> _localizer;
        private IWebHostEnvironment hostingEnvironment;

        private ApplicationDbContext _context { get; }

        public bool CanRequest()
        {
            return _context.PermissionSource
                                            .ToList()
                                            .LastOrDefault()
                                            .CanWorkWithAds;
        }

        public AdsController(IPersonData people, IStringLocalizer<AdsController> localizer, ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _people = people;
            _localizer = localizer;
            _context = context;
            hostingEnvironment = environment;
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var model = await _people.GetAds();
            var latest = model.OrderByDescending(x => x.CreateDate).FirstOrDefault();
            var vm = new AdDisplayModel();

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
                var files = HttpContext.Request.Form.Files;
                var newModel = new AdsModel()
                {
                    Ad1 = model.Ad1,
                    Ad2 = model.Ad2,
                    Ad3 = model.Ad3,
                    Ad1Blocked = model.Ad1Blocked,
                    Ad2Blocked = model.Ad2Blocked,
                    Ad3Blocked = model.Ad3Blocked,
                    CreateDate = model.CreateDate
                };
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
                        fileName = Path.Combine(hostingEnvironment.WebRootPath, "ads") + $@"\{newFileName}";

                        if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                            Directory.CreateDirectory(fileName);

                        // if you want to store path of folder in database
                        var PathDB = "ads/" + newFileName;

                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }

                        switch (file.Name)
                        {
                            case "id1":
                                newModel.Ad1 = PathDB;
                                break;
                            case "id2":
                                newModel.Ad2 = PathDB;

                                break;
                            case "id3":
                                newModel.Ad3 = PathDB;
                                break;
                        }


                    }
                }

                await _people.InsertAds(newModel);
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
