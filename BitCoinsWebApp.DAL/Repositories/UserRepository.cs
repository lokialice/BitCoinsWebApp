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
    using System.Data.Entity;
    using System.Web;

    using UserDTO = BitCoinsWebApp.Model.UserProfile;
    using System.Data.Objects;

    public class UserRepository : IUserRepository
    {
        #region member
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        private FundsRepository _fundRepository;
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserRepository).Name);  //Declaring Log4Net  
        #endregion

        #region constructor
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
            _pce = new BitCoinsEntities(connectionString);
        }
        #endregion

        #region method

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

        public UserDTO Login(string userName, string password)
        {
            try
            {
                UserAccount userAccount = _pce.UserAccounts.
                    FirstOrDefault(m => m.UserName.Equals(userName) &&
                        m.Password.Equals(password));
                if (userAccount == null)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserDTO>();
                UserDTO mappedUser = Mapper.Map<UserAccount, UserDTO>(userAccount);
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
                UserAccount mappedUser = Mapper.Map<UserProfile, UserAccount>(user);
                mappedUser.Password = SHA1.Encode(user.Password);
                mappedUser.IDRole = Convert.ToInt32(UserLevel.Standard);
                mappedUser.ImageProfile = 1;
                mappedUser.Token = user.Token;
                mappedUser.CreateDate = DateTime.Now;
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

        public bool Update(UserDTO savedUser)
        {
            try
            {
                UserAccount user = _pce.UserAccounts.Where(m => m.ID == savedUser.ID).First();
                if (!string.IsNullOrEmpty(savedUser.Name))
                    user.Name = savedUser.Name;              
                if (!string.IsNullOrEmpty(savedUser.Email))
                    user.Email = savedUser.Email;
                if (!string.IsNullOrEmpty(savedUser.Phone))
                    user.Phone = savedUser.Phone;
                if (!string.IsNullOrEmpty(savedUser.Address))
                    user.Address = savedUser.Address;
                user.Age = savedUser.Age;
                if (!string.IsNullOrEmpty(savedUser.BitCoinsCode))
                    user.BitCoinsCode = savedUser.BitCoinsCode;
                if (!string.IsNullOrEmpty(savedUser.Description))
                    user.Description = savedUser.Description;
                if (!string.IsNullOrEmpty(savedUser.FacebookLink))
                    user.FacebookLink = savedUser.FacebookLink;
                if (!string.IsNullOrEmpty(savedUser.SkypeID))
                    user.SkypeID = savedUser.SkypeID;
                if (!string.IsNullOrEmpty(savedUser.Gender))
                    user.Gender = savedUser.Gender;                
                user.IsActive = savedUser.IsActive;
                user.Amount = savedUser.Amount;
                user.IDRole = savedUser.IDRole;
                if (savedUser.ImageURL != null) 
                {
                    user.ImageProfile = savedUser.ImageURL.ID;
                }               
                _pce.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }
        public bool ChangePassword(UserDTO savedUser)
        {
            try
            {
                UserAccount user = _pce.UserAccounts.Where(m => m.ID == savedUser.ID).First();
                user.Password = savedUser.Password;
                _pce.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
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

        public UserProfile GetUserByUserName(string userName)
        {
            try
            {
                UserAccount userAccount = _pce.UserAccounts.FirstOrDefault(m => m.UserName == userName);

                if (userAccount == null)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserProfile>();
                UserProfile mappedUser = Mapper.Map<UserAccount, UserProfile>(userAccount);
                Mapper.CreateMap<ImageUpload, ImageFileUpload>();
                ImageFileUpload mappedImage = Mapper.Map<ImageUpload, ImageFileUpload>(userAccount.ImageUpload);
                mappedUser.ImageURL = mappedImage;
                return mappedUser;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public int GetTotalRefID(UserDTO user)
        {
            try
            {
                _fundRepository = new FundsRepository(_connectionString);                
                int count = 0;
                int countRef = 0;
                int countParent = 0;
                List<UserProfile> userRef = new List<UserProfile>();
                userRef = GetRefByUsername(user.UserName);
                foreach (var item in userRef)
                {
                    countRef = _fundRepository.GetAllTransactionsFromUser(item).Count;
                    countParent = _fundRepository.GetAllTransactionsFromUser(user).Count;
                    if (countRef != 0 && countParent != 0 && countParent == countRef || countRef > countParent)
                    {
                        count++;
                    }
                }
                return count;

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return 0;
            }
        }

        public float GetAccountBalance(UserDTO user)
        {
            try
            {
                var totalRef = GetTotalRefID(user);
                var amountCurrent = user.Amount - 5;
                var totalAmount = 0.0;
                if (amountCurrent > 0)
                {
                    totalAmount = amountCurrent + GetMoneyInterest(user);
                }
                return (float)totalAmount;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return (float)0;
            }
        }

        public List<UserDTO> GetRefByUsername(string username)
        {
            try
            {
                List<UserAccount> listRef = new List<UserAccount>();
                UserDTO user = GetUserByUserName(username);
                listRef = _pce.UserAccounts.Where(p => p.IDParent == user.ID && p.IsActive == true).ToList();

                if (listRef == null && listRef.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserDTO>();
                List<UserDTO> listRefOfUserName = Mapper.Map<List<UserAccount>, List<UserDTO>>(listRef);
                logger.Info("Complete GetRefByUsername");
                return listRefOfUserName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public List<UserDTO> GetAllUserLevel1()
        {
            try
            {
                List<UserAccount> listRef = new List<UserAccount>();
                var level = Convert.ToInt32(UserLevel.Standard);
                listRef = _pce.UserAccounts.Where(p => p.IDRole == level && p.IsActive == false).OrderByDescending(u => u.CreateDate).ToList();

                if (listRef == null && listRef.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserDTO>();
                List<UserDTO> listRefOfUserName = Mapper.Map<List<UserAccount>, List<UserDTO>>(listRef);
                foreach (var item in listRefOfUserName)
                {
                    item.BalanceAmount = (int)GetAccountBalance(item);
                }
                logger.Info("Complete GetAllUserLevel1");
                return listRefOfUserName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public List<UserDTO> GetAllUserLevel2()
        {
            try
            {
                List<UserAccount> listRef = new List<UserAccount>();
                var level = Convert.ToInt32(UserLevel.Gold);
                listRef = _pce.UserAccounts.Where(p => p.IDRole == level && p.IsActive == true).OrderByDescending(u => u.CreateDate).ToList();

                if (listRef == null && listRef.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserDTO>();
                List<UserDTO> listRefOfUserName = Mapper.Map<List<UserAccount>, List<UserDTO>>(listRef);
                foreach (var item in listRefOfUserName)
                {
                    item.BalanceAmount = (int)GetAccountBalance(item);
                }
                logger.Info("Complete GetAllUserLevel2");
                return listRefOfUserName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public bool ActiveUser(string id)
        {
            try
            {
                UserDTO user = new UserDTO();
                user = GetUser(Convert.ToInt32(id));
                if (user.IsActive)
                {
                    user.Amount = 0;
                    user.IDRole = Convert.ToInt32(UserLevel.Standard);
                    user.IsActive = false;
                }
                else
                {
                    user.IDRole = Convert.ToInt32(UserLevel.Gold);
                    user.IsActive = true;
                }
                if (Update(user))
                {
                    logger.Info("Complete ActiveUser");
                    return true;
                }
                else
                {
                    logger.Info("Complete ActiveUser");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }

        }

        public bool DefaultUser(string id)
        {
            try
            {
                UserDTO user = new UserDTO();
                user = GetUser(Convert.ToInt32(id));
                List<UserAccount> allUser = _pce.UserAccounts.ToList();
                Mapper.CreateMap<UserAccount, UserDTO>();
                List<UserDTO> listMapped = Mapper.Map<List<UserAccount>, List<UserDTO>>(allUser);
                foreach (var item in listMapped)
                {
                    if (item.ID != user.ID)
                    {
                        item.DefaultUser = false;
                        Update(item);
                    }
                    else
                    {
                        item.DefaultUser = true;
                        Update(item);
                    }
                }
                logger.Info("Complete DefaultUser");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        public int GetMoneyInterest(UserDTO user)
        {
            try
            {
                _fundRepository = new FundsRepository(_connectionString);
                int totalRef = GetTotalRefID(user);
                int totalAmount = 0;
                UserDTO users = GetUserByUserName(user.UserName);
                if (users == null)
                {
                    return 0;
                }

                totalAmount = 10 * totalRef;
                if (totalRef % 2 == 0)
                {
                    totalRef = (int)Math.Round((decimal)totalRef / 2);
                }
                else
                {
                    totalRef = (int)Math.Round((decimal)(totalRef - 1) / 2);
                }
                totalAmount = totalAmount + (30 * totalRef);

                logger.Info("Complete GetMoneyInterest");
                return totalAmount;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return 0;
            }
        }

        public List<UserDTO> GetAllUserOneRef()
        {
            try
            {
                List<UserAccount> listRef = new List<UserAccount>();
                var listUser = new List<UserAccount>();
                var level = Convert.ToInt32(UserLevel.Standard);
                listUser = _pce.UserAccounts.OrderByDescending(u => u.CreateDate).ToList();

                foreach (var item in listUser)
                {
                    int count = GetRefByUsername(item.UserName).Count;
                    if (count == 1)
                    {
                        listRef.Add(item);
                    }
                }

                if (listRef == null && listRef.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserDTO>();
                List<UserDTO> listRefOfUserName = Mapper.Map<List<UserAccount>, List<UserDTO>>(listRef);
                foreach (var item in listRefOfUserName)
                {
                    item.BalanceAmount = (int)GetAccountBalance(item);
                }
                logger.Info("Complete GetAllUserLevel1");
                return listRefOfUserName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public List<UserDTO> GetAllUserTwoRef()
        {
            try
            {
                List<UserAccount> listRef = new List<UserAccount>();
                var listUser = new List<UserAccount>();
                var level = Convert.ToInt32(UserLevel.Standard);
                listUser = _pce.UserAccounts.OrderByDescending(u => u.CreateDate).ToList();

                foreach (var item in listUser)
                {
                    int count = GetRefByUsername(item.UserName).Count;
                    if (count == 2)
                    {
                        listRef.Add(item);
                    }
                }

                if (listRef == null && listRef.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<UserAccount, UserDTO>();
                List<UserDTO> listRefOfUserName = Mapper.Map<List<UserAccount>, List<UserDTO>>(listRef);
                foreach (var item in listRefOfUserName)
                {
                    item.BalanceAmount = (int)GetAccountBalance(item);
                }
                logger.Info("Complete GetAllUserLevel1");
                return listRefOfUserName;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }

        }
        #endregion
    }
}
