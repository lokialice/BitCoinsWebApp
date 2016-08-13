namespace BitCoinsWebApp.DAL.Repositories
{
    using AutoMapper;
    using BitCoinsWebApp.DAL.Entites;
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UserDTO = BitCoinsWebApp.Model.User;

    public class UserRepository : IUserRepository
    {
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(UserRepository));  //Declaring Log4Net  
  
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
            _pce = new BitCoinsEntities(connectionString);
        }        

        public UserDTO GetUser(int userId)
        {
            try
            {
                UserAccount userAccount = _pce.UserAccounts.FirstOrDefault(m => m.ID == userId);

                if (userAccount == null)
                {
                    return null;
                }

                User mappedUser = Mapper.Map<UserAccount, UserDTO>(userAccount);
                return mappedUser;
            }
            catch (Exception ex) 
            {
                Logging.TrackError(_connectionString, ex, "GetUser");
                return null;
            }

        }

        public UserDTO Login(string userName, string password, bool isActive)
        {
            try
            {
                UserAccount userAccount = _pce.UserAccounts.FirstOrDefault(m => m.UserName.Equals(userName) && m.Password.Equals(password) && m.IsActive == true);
                if (userAccount == null)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount,User>();
                User mappedUser = new User();
                mappedUser = Mapper.Map<UserAccount, User>(userAccount, mappedUser);
                return mappedUser;
            }
            catch (Exception ex) 
            {
                logger.Error(ex.ToString());  
                return null;
            }
         }
    }
}
