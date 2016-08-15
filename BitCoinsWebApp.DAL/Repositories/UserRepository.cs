namespace BitCoinsWebApp.DAL.Repositories
{
    using AutoMapper;
    using BitCoinsWebApp.DAL.Entites;
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.Utilities;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UserDTO = BitCoinsWebApp.Model.UserProfile;

    public class UserRepository : IUserRepository
    {
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserRepository).Name);  //Declaring Log4Net  

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
                Mapper.CreateMap<UserAccount, UserProfile>();
                UserProfile mappedUser = Mapper.Map<UserAccount, UserDTO>(userAccount);
                return mappedUser;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }

        }

        public UserDTO Login(string userName, string password, bool isActive)
        {
            try
            {
                UserAccount userAccount = _pce.UserAccounts.
                    FirstOrDefault(m => m.UserName.Equals(userName) &&
                        m.Password.Equals(password) && m.IsActive == true);
                if (userAccount == null)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserProfile>();
                UserProfile mappedUser = mappedUser = Mapper.Map<UserAccount, UserProfile>(userAccount);
                return mappedUser;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }




        public bool Create(UserDTO user)
        {
            try
            {               
                Mapper.CreateMap<UserProfile, UserAccount>();
                UserAccount mappedUser = mappedUser = Mapper.Map<UserProfile, UserAccount>(user);
                mappedUser.Password = SHA1.Encode(user.Password);
                mappedUser.IDRole =  Convert.ToInt32(UserLevel.Standard);
                _pce.AddToUserAccounts(mappedUser);
                _pce.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        public bool Update(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserDTO user)
        {
            throw new NotImplementedException();
        }


        public UserProfile CheckUserName(string username)
        {
            Mapper.CreateMap<UserAccount, UserProfile>();
            var user = _pce.UserAccounts.SingleOrDefault(m => m.UserName.Equals(username));
            UserProfile mappedUser = Mapper.Map<UserAccount, UserProfile>(user);
            return mappedUser;
        }

        public UserProfile CheckEmailExist(string email)
        {
            Mapper.CreateMap<UserAccount, UserProfile>();
            var user = _pce.UserAccounts.SingleOrDefault(m => m.Email.Equals(email));
            UserProfile mappedUser = Mapper.Map<UserAccount, UserProfile>(user);
            return mappedUser;
        }
    }
}
