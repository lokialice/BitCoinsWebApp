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

    public class FundsRepository:IFundsRepository
    {
        #region member
        private readonly BitCoinsEntities _pce;
        private readonly string _connectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FundsRepository).Name);  //Declaring Log4Net  
        #endregion

        #region constructor
        public FundsRepository(string connectionString)
        {
            _connectionString = connectionString;
            _pce = new BitCoinsEntities(connectionString);
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
                Random rd = new Random(10000);
                transfer.OrderName = "Add Funds - " + rd.Next();
                transfer.CreateDate = DateTime.Now;
                transfer.TransferType = Convert.ToInt32(TransferTypes.Deposit);
                transfer.Description = "Initialize Funds";
                Transfer mappedTransder = Mapper.Map<TransferDTO, Transfer>(transfer);
                mappedTransder.FromUserID = transfer.FromUser.ID;
                mappedTransder.ToUserID = transfer.ToUser.ID;
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
            if (transfer.FromUser.Password.Equals(SHA1.Encode(transfer.ConfirmPassword)))
            {
                return true;
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

        #endregion

        
    }
}
