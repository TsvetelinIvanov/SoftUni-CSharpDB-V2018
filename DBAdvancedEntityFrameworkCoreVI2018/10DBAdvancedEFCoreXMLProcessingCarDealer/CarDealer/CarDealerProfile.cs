using AutoMapper;
using CarDealer.DTOs.ExportDTOs;
using CarDealer.DTOs.ImportDTOs;
using CarDealer.Models;
using System.Linq;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>();
            CreateMap<PartDto, Part>();
            CreateMap<CarDto, Car>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Car, CarWithDistanceDto>();
            CreateMap<Car, CarFromFerrariDto>();
            CreateMap<Supplier, LocalSupplierDto>().ForMember(dest => dest.PartsCount, opt => opt.MapFrom(src => src.Parts.Count));
            CreateMap<Part, ExportPartDto>();
            CreateMap<Car, ExportCarDto>().ForMember(dest => dest.Parts, opt => opt.MapFrom(src => src.PartCars.Select(pc => pc.Part)));
            CreateMap<Car, SoldCarDto>();
            CreateMap<Sale, ExportSaleDto>().ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));
        }
    }
}