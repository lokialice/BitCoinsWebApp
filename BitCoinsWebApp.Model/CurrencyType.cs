namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class CurrencyType
    {
        #region construction
        public CurrencyType() { }
        #endregion

        #region member
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string CurrencyName { get; set; }

        #endregion
    }
}
