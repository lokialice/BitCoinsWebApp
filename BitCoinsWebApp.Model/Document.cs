namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class Document
    {
        public Document() 
        {
        
        }

        [Required]
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string DocumentName { get; set; }
        [DataMember]
        public string DocumentFile { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }


    }
}
