namespace BitCoinsWebApp.BLL
{
    using BitCoinsWebApp.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFundService
    {
        List<CurrencyType> GetAllCurrencyType();
        bool Create(Transactions transfer);
        bool CheckConfirmPass(Transactions transfer);
        Transactions GetTransactionsLastest(UserProfile user);
        bool CheckAddFundInMonth(Transactions trans);
        List<Transactions> GetAllTransactionsFromUser(UserProfile user);
        bool ActiveOrder(string id,string type);
        bool CreateRequest(Transactions transfer);
        Transactions GetTransactionByID(string id);
        List<Transactions> GetAllTransactions();
        int TotalAmountInvest(UserProfile user);
    }
}
