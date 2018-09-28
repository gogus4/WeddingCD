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
    /// Interface for business methods related to User management.
    /// </summary>
    public interface IUserManagement
    {
        /// <summary>
        /// Get user
        /// </summary>
        /// <returns>The Task to be awaited.</returns>
        Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
    }
}