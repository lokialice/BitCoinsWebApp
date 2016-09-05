namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    
    public enum TransferTypes
    {       
        Deposit = 1,
        Transfer = 2,
        Withdraw = 3
    }
}
