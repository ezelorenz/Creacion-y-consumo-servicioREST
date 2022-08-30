using AutoMapper;
using ClientesAPI.Dto;
using ClientesAPI.Entidades;

namespace ClientesAPI.Utilidades
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().
                ReverseMap();
            CreateMap<CreateCustomerDTO, Customer>();
        }
    }
}
