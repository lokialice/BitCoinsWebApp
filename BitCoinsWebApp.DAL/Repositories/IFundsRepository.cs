namespace BitCoinsWebApp.DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BitCoinsWebApp.Model;

    public interface IFundsRepository
    {
        List<CurrencyType> GetAllCurrencyType();
        bool Create(Transactions transfer);
        bool CheckConfirmPass(Transactions transfer);
        Transactions GetTransactionsLastest(UserProfile user);
        Transactions GetTransactionByID(string id);
        bool Update(Transactions transfer);
        bool CheckAddFundInMonth(Transactions trans);
        List<Transactions> GetAllTransactionsFromUser(UserProfile user);
        bool ActiveOrder(string id,string type);
        bool CreatRequest(Transactions transfer);
        List<Transactions> GetAllTransactions();
        int TotalAmountInvest(UserProfile user);
    }
}
