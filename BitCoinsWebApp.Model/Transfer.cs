namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class Transfer
    {
        public Transfer() 
        {
        
        }

        [Required]
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int TransferType { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
    
    }
}
