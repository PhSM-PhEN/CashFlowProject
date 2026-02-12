using AutoMapper;
using CashFlow.Communication.Request;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntitie();
            EntitieToResponse();

        }


        private void RequestToEntitie()
        {
            CreateMap<RequestExpenseJson, Expenses>();


        }
        private void EntitieToResponse()
        {
            CreateMap<Expenses, ResponseRegisterExpenseJson>(); // Map from Expenses to ResponseRegisterExpenseJson
            CreateMap<Expenses, ResponseShortExpenseJson>(); // Map from Expenses to ResponseShortExpenseJson
            CreateMap<Expenses, ResponseExpenseJson>(); // Map from Expenses to ResponseExpenseJson
        }
    }
}
