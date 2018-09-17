using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingCD.DAL.Entities;

namespace WeddingCD.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.Categories = new List<Category>();
            this.Pictures = new List<Picture>();
            this.CategorieListItem = new List<SelectListItem>();
        }

        /// <summary>
        /// Gets or sets the categories
        /// </summary>
        /// <value>
        /// The galleries
        /// </value>
        public IList<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the pictures
        /// </summary>
        /// <value>
        /// The pictures
        /// </value>
        public IList<Picture> Pictures { get; set; }

        /// <summary>
        /// Gets or sets logo image for form
        /// </summary>
        /// <value>
        /// The logo image
        /// </value>
        [Display(Name = "Propriete_PictureToUpload", ResourceType = typeof(WeddingCD.Resources.View.CommonResources))]
        [Required(ErrorMessageResourceName = "Validation_PictureToUpload_Required", ErrorMessageResourceType = typeof(WeddingCD.Resources.View.CommonResources))]
        public List<HttpPostedFileBase> PictureToUpload { get; set; }

        /// <summary>
        /// Gets or sets the selected category identifier.
        /// </summary>
        public string SelectedCategory { get; set; }

        /// <summary>
        /// Gets or sets the brand.
        /// </summary>
        [Display(Name = "Propriete_Category", ResourceType = typeof(WeddingCD.Resources.View.CommonResources))]
        [Required(ErrorMessageResourceName = "Validation_Category_Required", ErrorMessageResourceType = typeof(WeddingCD.Resources.View.CommonResources))]
        public List<SelectListItem> CategorieListItem { get; set; }

    }
}