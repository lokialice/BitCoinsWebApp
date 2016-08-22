namespace BitCoinsWebApp.DAL.Repositories
{
    using BitCoinsWebApp.DAL.Entites;
    using BitCoinsWebApp.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>UserAccount.</returns>
        UserProfile GetUser(int userId);
        /// <summary>
        /// Gets the name of the user by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>UserProfile.</returns>
        UserProfile GetUserByUserName(string userName);
        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns>User.</returns>
        UserProfile Login(string userName, string password, bool isActive);
        /// <summary>
        /// Creates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Create(UserProfile user);
        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Update(UserProfile user);
        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Delete(UserProfile user);
        /// <summary>
        /// Checks the name of the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        UserProfile CheckUserName(string username);

        /// <summary>
        /// Checks the email exist.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        UserProfile CheckEmailExist(string email);

    }
}
