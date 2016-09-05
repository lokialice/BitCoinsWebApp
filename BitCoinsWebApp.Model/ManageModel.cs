namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ManageModel
    {
        #region member
        public UserProfile UserManage { get; set; }
        public int CountRefID { get; set; }
        public float ProInterestWallet { get; set; }
        public float SystemWallet { get; set; }
        public float AccountBalance { get; set; }
        #endregion


    }
}
