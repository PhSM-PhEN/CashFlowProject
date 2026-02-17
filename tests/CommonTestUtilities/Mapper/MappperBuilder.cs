using AutoMapper;
using CashFlow.Application.AutoMapper;

namespace CommonTestUtilities.Mapper
{
    public  class MappperBuilder
    { 
        public static IMapper Build()
        {
            var mapper = new MapperConfiguration(config => config.AddProfile(
                new AutoMapping()));

            return mapper.CreateMapper();
        }
       
    }
}
