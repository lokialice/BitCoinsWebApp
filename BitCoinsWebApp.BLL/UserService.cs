namespace BitCoinsWebApp.BLL
{
    using BitCoinsWebApp.DAL.Repositories;
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly string _connectionString;

        public UserService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BitWebAppEntities"].ConnectionString;
            _repository = new UserRepository(_connectionString);
        }
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User.</returns>
        public UserProfile GetUser(int userId)
        {
            return _repository.GetUser(userId);
        }

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <returns>User.</returns>
        public UserProfile Login(string userName, string password, bool isActive)
        {
            password = SHA1.Encode(password);
            return _repository.Login(userName, password, isActive);
        }


        public bool Create(UserProfile user)
        {              
            return _repository.Create(user);
        }

        public bool Update(UserProfile user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserProfile user)
        {
            throw new NotImplementedException();
        }


        public UserProfile CheckUserName(string username)
        {
            return _repository.CheckUserName(username);
        }

        public UserProfile CheckEmailExist(string email)
        {
            return _repository.CheckEmailExist(email);
        }
    }
}
