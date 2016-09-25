namespace BitCoinsWebApp.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.DAL.Repositories;
    using System.Configuration;
    using BitCoinsWebApp.Utilities;

    public class FundService:IFundService
    {
        #region member
        private readonly IFundsRepository _repository;
        private readonly string _connectionString;
        private readonly IUserService _userservice;
        #endregion

        #region constructor
        public FundService() 
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BitWebAppEntities"].ConnectionString;
            _repository = new FundsRepository(_connectionString);
            _userservice = new UserService();
        }
        #endregion

        #region method
        public List<CurrencyType> GetAllCurrencyType()
        {
            return _repository.GetAllCurrencyType();
        }

        public bool Create(Transactions transfer) 
        {                     
            return _repository.Create(transfer);                     
        }

        public bool CheckConfirmPass(Transactions transfer) 
        {
            return _repository.CheckConfirmPass(transfer);
        }

        public Transactions GetTransactionsLastest(UserProfile user)
        {
            return _repository.GetTransactionsLastest(user);
        }

        public bool CheckAddFundInMonth(Transactions trans) 
        {
            return _repository.CheckAddFundInMonth(trans);
        }

        public List<Transactions> GetAllTransactionsFromUser(UserProfile user)
        {
            return _repository.GetAllTransactionsFromUser(user);
        }

        public bool ActiveOrder(string id,string type)
        {
            return _repository.ActiveOrder(id,type);
        }

        public bool CreateRequest(Transactions transfer)
        {
            return _repository.CreatRequest(transfer);
        }

        public Transactions GetTransactionByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<Transactions> GetAllTransactions()
        {
            return _repository.GetAllTransactions();
        }

        public int TotalAmountInvest(UserProfile user)
        {
            if (user.Amount > 0)
            {
                return _repository.TotalAmountInvest(user);
            }
            else
            {
                return 0;
            }
        }
        #endregion

    }
}
