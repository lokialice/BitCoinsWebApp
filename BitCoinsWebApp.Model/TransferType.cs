namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class TransferTypes
    {
        public TransferTypes() 
        {
        
        }

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TransferType { get; set; }
        [DataMember]
        public string Description { get; set; }


    }
}
