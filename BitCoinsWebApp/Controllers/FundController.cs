namespace BitCoinsWebApp.Controllers
{
    using BitCoinsWebApp.Model;
    using BitCoinsWebApp.Utilities;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    [SessionExpire]
    public class FundController : BaseController
    {
        #region member
        private static readonly ILog _log = LogManager.GetLogger(typeof(FundController).Name);

        #endregion

        #region public member

        #endregion

        public ActionResult Index()
        {         
            return View("Index", UserTransfer);
        }

        public ActionResult AddFunds(Transactions transfer)
        {
            UserTransfer.CurrencyList = _fundService.GetAllCurrencyType();
            UserTransfer.FromUser = UserCurrent;
            UserTransfer.ToUser = _userService.GetUserByUserName("lokialice");
            transfer.FromUser = UserCurrent;
            transfer.ToUser = _userService.GetUserByUserName("lokialice");
            if (_fundService.CheckConfirmPass(transfer))
            {
                if (transfer.Amount == 105)
                {
                    if (_fundService.GetTransactionsLastest(transfer.FromUser) != null)
                    {
                        if (_fundService.CheckAddFundInMonth(_fundService.GetTransactionsLastest(transfer.FromUser)))
                        {
                            _fundService.Create(transfer);
                        }
                        else
                        {
                            ViewBag.CheckFundInMonth = "Please waiting thirty days to add funds !";
                        }
                    }
                    else 
                    {
                        _fundService.Create(transfer);
                    }
                }
                else
                {
                    ViewBag.AmountError = "Please input $105 to active your account !";
                }
            }
            else
            {
                ViewBag.PasswordError = "Password Confirm not correct! Please try again!";
            }
            return View("Index", UserTransfer);
        }

        public ActionResult FundHistory() 
        {
            return View("FundsHistory", UserTransfer);
        }

    }
}
