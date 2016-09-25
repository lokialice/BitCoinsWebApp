namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class ImageFileUpload
    {
        public ImageFileUpload() 
        {
        
        }

        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public string ImageFile { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
    }
}
