﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeddingCD.DAL.Entities;

namespace WeddingCD.Business.Interface
{
    /// <summary>
    /// Interface for business methods related to Gallery management.
    /// </summary>
    public interface IGalleryManagement
    {
        /// <summary>
        /// Get categories
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        Task<IList<Category>> GetCategoriesAsync();
    }
}