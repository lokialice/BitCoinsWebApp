namespace BitCoinsWebApp.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    [DataContract]
    public class PostNews
    {
        public PostNews() { }

        [DataMember]
        public int ID { get; set; }
        
        [DataMember]
        public int PostAuthor { get; set; }

        [DataMember]
        public DateTime PostDate { get; set; }

        [DataMember]
        public string PostTittle { get; set; }

        [DataMember]
        public string PostExcerpt { get; set; }

        [DataMember]
        public string PostContent { get; set; }

        [DataMember]
        public string FeatureImage { get; set; }

        [DataMember]
        public bool PostStatus { get; set; }

        [DataMember]
        public UserProfile UserPost { get; set; }

        public List<PostNews> GetAllListPostNews { get; set; }
    }
}
