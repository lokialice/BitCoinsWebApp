namespace BitCoinsWebApp.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;    
    using BitCoinsWebApp.DAL.Entites;
    using BitCoinsWebApp.Model;
    using TransferDTO = BitCoinsWebApp.Model.Transactions;
    using CurrencyDTO = BitCoinsWebApp.Model.CurrencyType;
    using log4net;
    using AutoMapper;
    using BitCoinsWebApp.Utilities;
    using System.Data.Objects;

    public class FundsRepository:IFundsRepository
    {
        #region member
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FundsRepository).Name);  //Declaring Log4Net       
        private readonly IUserRepository _userRepository;
        #endregion

        #region constructor
        public FundsRepository(string connectionString)
        {
            _connectionString = connectionString;
            _pce = new BitCoinsEntities(connectionString);
            _userRepository = new UserRepository(connectionString);
        }
        #endregion

        #region method
        public List<CurrencyDTO> GetAllCurrencyType()
        {
            try
            {
                List<Currency> listcurrency = new List<Currency>();                             
                listcurrency = _pce.Currencies.ToList();

                if (listcurrency == null && listcurrency.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<Currency, CurrencyDTO>();
                List<CurrencyDTO> listCurrencyType = Mapper.Map<List<Currency>, List<CurrencyDTO>>(listcurrency);
                logger.Info("Complete GetAllCurrencyType");
                return listCurrencyType;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public bool Create(TransferDTO transfer)
        {
            try
            {
                Mapper.CreateMap<TransferDTO, Transfer>();                
                transfer.OrderName = "Add Funds - " + transfer.FromUser.UserName;
                transfer.CreateDate = DateTime.Now;
                transfer.TransferType = Convert.ToInt32(TransferTypes.Deposit);
                transfer.Description = "Initialize Funds";
                transfer.Status = false;
                Transfer mappedTransder = Mapper.Map<TransferDTO, Transfer>(transfer);
                mappedTransder.FromUserID = transfer.FromUser.ID;
                mappedTransder.ToUserID = transfer.ToUser.ID;
                          
                UserProfile user = _userRepository.GetUser(transfer.FromUser.ID);
                user.Amount = user.Amount + transfer.Amount;
                _userRepository.Update(user);                           

                logger.Info("Complete Create(TransferDTO transfer)");
                _pce.AddToTransfers(mappedTransder);
                _pce.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }

        public bool CheckConfirmPass(TransferDTO transfer)
        {
            if (!string.IsNullOrEmpty(transfer.ConfirmPassword))
            {
                if (transfer.FromUser.Password.Equals(SHA1.Encode(transfer.ConfirmPassword)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        public Transactions GetTransactionsLastest(UserProfile user) 
        {
            try
            {
                Transfer trans = _pce.Transfers.Where(m=> m.FromUserID == user.ID).OrderByDescending(p => p.CreateDate).FirstOrDefault();
                if (trans == null)
                {
                    return null;
                }
                Mapper.CreateMap<Transfer, Transactions>();
                Transactions mappedTrans = Mapper.Map<Transfer, Transactions>(trans);
                return mappedTrans;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public bool CheckAddFundInMonth(TransferDTO trans)
        {
            var startDate = trans.CreateDate;
            var currentDate = DateTime.Today;
            CalTime time = new CalTime(startDate, currentDate);
            if (time.Days < 30)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        public List<TransferDTO> GetAllTransactionsFromUser(UserProfile user)
        {
            try
            {
                List<Transfer> listTransactions = new List<Transfer>();
                listTransactions = _pce.Transfers.Where(m => m.FromUserID == user.ID).OrderByDescending(p => p.CreateDate).ToList();

                if (listTransactions == null && listTransactions.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<Transfer, TransferDTO>();
                List<TransferDTO> listTransfer = Mapper.Map<List<Transfer>, List<TransferDTO>>(listTransactions);
                logger.Info("Complete GetAllTransactionsFromUser");
                return listTransfer;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }
        public TransferDTO GetTransactionByID(string id)
        {
            try
            {
                var transferId = Convert.ToInt32(id);
                Transfer transfer = _pce.Transfers.FirstOrDefault(m => m.ID == transferId );

                if (transfer == null)
                {
                    return null;
                }
                Mapper.CreateMap<Transfer, TransferDTO>();
                Transactions mappedTransfer = Mapper.Map<Transfer, TransferDTO>(transfer);
                logger.Info("Complete GetTransactionByID");
                return mappedTransfer;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public bool Update(TransferDTO transfer)
        {
            throw new NotImplementedException();
        }

        public bool ActiveOrder(string id,string type)
        {
            try
            {
                var transferID = Convert.ToInt32(id);
                var transferType = Convert.ToInt32(type);
                
                Transfer transferDB = _pce.Transfers.Where(m => m.ID == transferID).First();
                UserProfile user = _userRepository.GetUser(transferDB.FromUserID);
                user.Amount = 0;
                if (transferDB.Status == true)
                {                   
                    transferDB.Status = false;
                }
                else
                {
                    if (transferType == 3)
                    {
                        _userRepository.Update(user);
                    }
                    transferDB.Status = true;
                }
                _pce.SaveChanges();
                _pce.Refresh(RefreshMode.StoreWins, transferDB);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
            
        }

        public bool CreatRequest(TransferDTO transfer)
        {
            try
            {
                Mapper.CreateMap<TransferDTO, Transfer>();              
                transfer.OrderName = "Request From - " + transfer.FromUser.UserName;
                transfer.CreateDate = DateTime.Now;
                transfer.TransferType = Convert.ToInt32(TransferTypes.Withdraw);
                transfer.Description = "Request Send Money";
                transfer.Status = false;
                Transfer mappedTransder = Mapper.Map<TransferDTO, Transfer>(transfer);
                mappedTransder.FromUserID = transfer.FromUser.ID;
                mappedTransder.ToUserID = transfer.ToUser.ID;

                logger.Info("Complete CreatRequest(TransferDTO transfer)");
                _pce.AddToTransfers(mappedTransder);
                _pce.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return false;
            }
        }       

        public List<TransferDTO> GetAllTransactions()
        {
            try
            {
                List<Transfer> listTransactions = new List<Transfer>();
                listTransactions = _pce.Transfers.OrderByDescending(p => p.CreateDate).ToList();

                if (listTransactions == null && listTransactions.Count() == 0)
                {
                    return null;
                }
                Mapper.CreateMap<Transfer, TransferDTO>();
                List<TransferDTO> listTransfer = Mapper.Map<List<Transfer>, List<TransferDTO>>(listTransactions);
                logger.Info("Complete GetAllTransactions");
                return listTransfer;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return null;
            }
        }

        public int TotalAmountInvest(UserProfile user)
        {
            List<TransferDTO> allTransfer = new List<TransferDTO>();
            allTransfer = GetAllTransactionsFromUser(user);
            int totalInvest = (int)allTransfer.Sum(p => p.Amount);
            totalInvest = totalInvest - (5 * allTransfer.Count());
            return totalInvest;

        }
        #endregion       
    

       
    

       
    

        
    }
}
