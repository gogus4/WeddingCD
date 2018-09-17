using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingCD.Business.Interface;
using WeddingCD.DAL.Entities;
using WeddingCD.Models.Home;

namespace WeddingCD.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The home management instance
        /// </summary>
        private readonly IGalleryManagement galleryManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="homeManagement">The home management.</param>
        public HomeController(IGalleryManagement galleryManagement)
        {
            this.galleryManagement = galleryManagement;
        }

        public async Task<ActionResult> Index()
        {
            var model = new HomeViewModel();

            model.Categories = await this.galleryManagement.GetCategoriesAsync();

            foreach (var category in model.Categories)
            {
                model.CategorieListItem.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Name
                });
            }

            model.Pictures = await this.galleryManagement.GetPicturesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UploadPictureAsync(string Categorie, List<HttpPostedFileBase> files)
        {
            try
            {
                var categoryDb = await this.galleryManagement.GetCategoryByNameAsync(Categorie);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(@"~\Content\Galerie\"), fileName);
                    file.SaveAs(path);

                    // Insert the picture
                    await this.galleryManagement.InsertPictureAsync(new Picture()
                    {
                        AddBy = "Diego",
                        Category = categoryDb,
                        Path = fileName
                    });
                }

                return this.Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
                return this.Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}