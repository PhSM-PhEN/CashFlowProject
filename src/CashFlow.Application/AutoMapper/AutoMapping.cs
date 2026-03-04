using AutoMapper;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Request.ToExpenses;
using CashFlow.Communication.Request.ToUser;
using CashFlow.Communication.Responses.ToExpenses;
using CashFlow.Communication.Responses.ToUser;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            UserResponse();
            RequestToEntitie();
            EntitieToResponse();


        }


        private void RequestToEntitie()
        {
            
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, config => config.Ignore());
            
            CreateMap<RequestExpenseJson, Expenses>()
                .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));

            CreateMap<TagEnum, Tag>()
                .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
        }
        private void EntitieToResponse()
        {
            CreateMap<Expenses, ResponseExpenseJson>() 
                .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value))); // Map from Expenses to ResponseExpenseJson
            CreateMap<Expenses, ResponseRegisterExpenseJson>(); // Map from Expenses to ResponseRegisterExpenseJson
            CreateMap<Expenses, ResponseShortExpenseJson>(); // Map from Expenses to ResponseShortExpenseJson
            
        }
        private void UserResponse()
        {
            CreateMap<User, ResponseUserProfile>();
        }
    }
}
