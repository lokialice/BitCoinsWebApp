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
        bool CheckAddFundInMonth(Transactions trans);
        List<Transactions> GetAllTransactions();
    }
}
