using AppDrones.Core.Dto;
using AppDrones.Core.Models;
using AutoMapper;

namespace AppDrones.Core.Mappings
{
    public class DroneMappings : Profile
    {
        public static Mapper InitializerMapping()
        {
            var config = new MapperConfiguration(map =>
            {
                map.CreateMap<RegistryReqDto, Drone>()
                     .ForMember(dr => dr.SerialNumber, y => y.MapFrom(t => t.SerialNumber))
                     .ForMember(dr => dr.State, y => y.MapFrom(t => t.State))
                     .ForMember(dr => dr.BatteryCapacity, y => y.MapFrom(t => t.BatteryCapacity))
                     .ForMember(dr => dr.WeightLimit, y => y.MapFrom(t => t.WeightLimit))
                     .ForMember(dr => dr.BatteryCapacity, y => y.MapFrom(t => t.BatteryCapacity))
                     .ForMember(dr => dr.Model, y => y.MapFrom(t => t.Model));
                
                map.CreateMap<Drone, RegistryResDto>()
                 .ForMember(dr => dr.SerialNumber, y => y.MapFrom(t => t.SerialNumber))
                 .ForMember(dr => dr.State, y => y.MapFrom(t => t.State))
                 .ForMember(dr => dr.BatteryCapacity, y => y.MapFrom(t => t.BatteryCapacity))
                 .ForMember(dr => dr.WeightLimit, y => y.MapFrom(t => t.WeightLimit))
                 .ForMember(dr => dr.BatteryCapacity, y => y.MapFrom(t => t.BatteryCapacity))
                 .ForMember(dr => dr.Model, y => y.MapFrom(t => t.Model))
                 .ForMember(dr => dr.Id, y => y.MapFrom(t => t.DroneId));
            });
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
