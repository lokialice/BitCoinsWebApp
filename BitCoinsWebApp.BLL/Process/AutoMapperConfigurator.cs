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
                CreateMap<UserAccount, UserProfile>().ForMember(x => x.ImageProfile, opt => opt.Ignore()).ForMember(x => x.ListRef, opt => opt.Ignore()).ForMember(x => x.ListUserLevel1, opt => opt.Ignore()).ForMember(x => x.ListUserLevel2, opt => opt.Ignore()).ForMember(x => x.BalanceAmount, opt => opt.Ignore()).ReverseMap();
                CreateMap<UserProfile, UserAccount>().ReverseMap();
                CreateMap<Currency, CurrencyType>().ReverseMap();
                CreateMap<CurrencyType, Currency>().ReverseMap();
                CreateMap<Transfer, Transactions>().ForMember(x => x.FromUser, opt => opt.Ignore()).ForMember(x => x.ToUser, opt => opt.Ignore()).ForMember(x => x.UserCurrent, opt => opt.Ignore()).ForMember(x => x.CurrencyList, opt => opt.Ignore()).ForMember(x => x.ConfirmPassword, opt => opt.Ignore()).ForMember(x => x.GetAllTransactions, opt => opt.Ignore()).ReverseMap();

                CreateMap<Transactions, Transfer>().ReverseMap();
                CreateMap<Post, PostNews>().ForMember(x => x.GetAllListPostNews, opt => opt.Ignore()).ForMember(x => x.UserPost, opt => opt.Ignore()).ReverseMap();

                CreateMap<PostNews, Post>().ReverseMap();
                
                    
            }
        }

    }
}
