using System;
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

        /// <summary>
        /// Get category by name
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        Task<Category> GetCategoryByNameAsync(string name);

        /// <summary>
        /// Get pictures
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        Task<IList<Picture>> GetPicturesAsync();

        /// <summary>
        /// Insert picture
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        Task<IList<Picture>> InsertPictureAsync(Picture pictureToInsert);
    }
}