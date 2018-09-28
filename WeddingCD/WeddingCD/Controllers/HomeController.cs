using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeddingCD.Business.Interface;
using WeddingCD.Common.Security;
using WeddingCD.DAL.Entities;
using WeddingCD.Models.Home;

namespace WeddingCD.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The authentication interface
        /// </summary>
        private readonly IAuth auth;

        /// <summary>
        /// The home management instance
        /// </summary>
        private readonly IGalleryManagement galleryManagement;

        /// <summary>
        /// The user management instance
        /// </summary>
        private readonly IUserManagement userManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="auth">The authentification.</param>
        /// <param name="homeManagement">The home management.</param>
        /// <param name="userManagement">The user management.</param>
        public HomeController(IAuth auth, IGalleryManagement galleryManagement, IUserManagement userManagement)
        {
            this.auth = auth;
            this.galleryManagement = galleryManagement;
            this.userManagement = userManagement;
        }

        public async Task<ActionResult> Index()
        {
            var model = new HomeViewModel();

            model.Categories = await this.galleryManagement.GetCategoriesAsync();
            model.People = await this.galleryManagement.GetPeopleAsync();

            foreach (var category in model.Categories)
            {
                model.CategorieListItem.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.Name
                });
            }

            foreach (var person in model.People.OrderBy(x => x.Name))
            {
                model.AddByListItem.Add(new SelectListItem()
                {
                    Text = person.Name,
                    Value = person.Name
                });
            }

            model.Pictures = await this.galleryManagement.GetPicturesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Login(string login, string password)
        {
            try
            {
                var user = await this.userManagement.GetUserByLoginAndPasswordAsync(login, password);

                if(user != null)
                {
                    this.auth.DoAuth(login, true);

                    var principal = new GenericPrincipal(new GenericIdentity(login), null);

                    this.HttpContext.User = principal;
                    Thread.CurrentPrincipal = principal;
                }
                else
                {
                    return this.Json(new { Success = false }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public async Task<JsonResult> UploadPictureAsync(string Categorie, string AddBy, List<HttpPostedFileBase> files)
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