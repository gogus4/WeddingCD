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
    /// Class that contains business methods related to user management.
    /// </summary>
    public partial class UserManagement : BaseManagement, IUserManagement
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManagement" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public UserManagement(WeddingCDDbContext dbContext, IConfiguration configuration)
            : base(dbContext, configuration)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get user
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            try
            {
                var passwordHash = PasswordHash.CreateHash(password);

                var dbUser = await this.DbContext.Users.FirstOrDefaultAsync(x => x.Email == login);

                var res = PasswordHash.ValidatePassword(password, dbUser.Password);
                if (!res)
                {
                    return null;
                }

                return dbUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}