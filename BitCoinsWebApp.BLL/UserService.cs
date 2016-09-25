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
    using System.Web;

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
        public UserProfile Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                password = SHA1.Encode(password);               
            }
            return _repository.Login(userName, password);
        }

        public bool Create(UserProfile user)
        {
            user.Token = SHA1.RandomString(20);
            user.Password = SHA1.RandomString(20);
            if (_repository.Create(user))
            {
                SendEmail sent = new SendEmail();
                sent.SendMailRegister("Email", user.Email, new String[] { user.UserName ,user.Password,user.Token});
                return true;
            }
            else 
            {
                return false;
            }
            
        }

        public bool Update(UserProfile user)
        {                       
            return _repository.Update(user);
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

        public UserProfile GetUserByUserName(string userName)
        {
            return _repository.GetUserByUserName(userName);
        }

        public int GetTotalRefID(UserProfile user)
        {
            return _repository.GetTotalRefID(user);
        }

        public float GetAccountBalance(UserProfile user)
        {
            return _repository.GetAccountBalance(user);
        }

        public List<UserProfile> GetRefByUsername(string username)
        {
            return _repository.GetRefByUsername(username);
        }

        public List<UserProfile> GetAllUserLevel1(string username)
        {
            return _repository.GetAllUserLevel1(username);
        }

        public List<UserProfile> GetAllUserLevel2(string username)
        {
            return _repository.GetAllUserLevel2(username);
        }

        public bool ActiveUser(string id)
        {
            return _repository.ActiveUser(id);
        }

        public bool DefaultUser(string id)
        {
            return _repository.DefaultUser(id);
        }


        public int GetMoneyInterest(UserProfile user)
        {
            return _repository.GetMoneyInterest(user);
        }
    }
}
