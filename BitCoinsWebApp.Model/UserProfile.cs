namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class UserProfile
    {

        public UserProfile()
        {

        }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }      
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$",
            ErrorMessage = "The Email Address field is not a valid e-mail address.")]
        [DataMember]
        public string Email { get; set; }
        [RegularExpression(@"1?\W*([2-9][0-8][0-9])\W*([2-9][0-9]{2})\W*([0-9]{4})(\se?x?t?(\d*))?",
            ErrorMessage = "This is not a valid phone number")]
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string BitCoinsQR { get; set; }
        [DataMember]
        public string BitCoinsCode { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public string ImageProfile { get; set; }
        [DataMember]
        public string FacebookLink { get; set; }
        [DataMember]
        public string SkypeID { get; set; }     
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int IDParent { get; set; }

    }

}
