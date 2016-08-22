namespace BitCoinsWebApp.BLL.Process
{
    using AutoMapper;
    using BitCoinsWebApp.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using BitCoinsWebApp.DAL.Entites;

    public class AutoMapperConfigurator
    {
        public static IMapper Mapper;
        public static void ConfigureAutoMapper() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            Mapper = config.CreateMapper();
            config.AssertConfigurationIsValid();
        }

        public class MappingProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UserAccount, UserProfile>().ForMember(x=> x.ImageProfile,opt => opt.Ignore()).ReverseMap();
                CreateMap<UserProfile, UserAccount>().ReverseMap();   
                    
            }
        }

    }
}
