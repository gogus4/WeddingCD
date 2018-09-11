using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WeddingCD.Business.Interface;
using WeddingCD.Configuration;
using WeddingCD.DAL.Context;
using WeddingCD.DAL.Entities;

namespace WeddingCD.Business
{
    /// <summary>
    /// Class that contains business methods related to gallery management.
    /// </summary>
    public partial class GalleryManagement : BaseManagement, IGalleryManagement
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GalleryManagement" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public GalleryManagement(WeddingCDDbContext dbContext, IConfiguration configuration)
            : base(dbContext, configuration)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get categories
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        public async Task<IList<Category>> GetCategoriesAsync()
        {
            try
            {
                return await this.DbContext.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}