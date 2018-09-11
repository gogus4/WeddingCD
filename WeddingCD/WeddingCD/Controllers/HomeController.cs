using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingCD.Business.Interface;
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

            var categories = await this.galleryManagement.GetCategoriesAsync();
            model.Categories = categories;

            return View(model);
        }
    }
}