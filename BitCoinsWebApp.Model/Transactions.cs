namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class Transactions
    {
        public Transactions() 
        {
           
        }

        [Required]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string OrderName { get; set; }
        [DataMember]
        public int TransferType { get; set; }
        [DataMember]
        public UserProfile FromUser { get; set; }
        [DataMember]
        public UserProfile ToUser { get; set; }
        [DataMember]
        public List<CurrencyType> CurrencyList { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
        [DataMember]
        public UserProfile UserCurrent { get; set; }
        [DataMember]
        public int CurrencyType { get; set; }
        [DataMember]
        public string ConfirmPassword { get; set; }
       
    }
}
