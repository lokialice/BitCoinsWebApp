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
        #endregion

        #region constructor
        public FundService() 
        {
            _connectionString = ConfigurationManager.ConnectionStrings["BitWebAppEntities"].ConnectionString;
            _repository = new FundsRepository(_connectionString);
        }
        #endregion

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
    }
}
