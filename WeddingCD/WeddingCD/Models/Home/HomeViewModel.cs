using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingCD.DAL.Entities;

namespace WeddingCD.Models.Home
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            this.Categories = new List<Category>();
        }

        /// <summary>
        /// Gets or sets the categories
        /// </summary>
        /// <value>
        /// The galleries
        /// </value>
        public IList<Category> Categories { get; set; }
    }
}