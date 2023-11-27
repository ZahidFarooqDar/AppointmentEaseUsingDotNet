using AppointmentEaseAPI.DomainModels;
using AppointmentEaseAPI.ServiceModels;
using AutoMapper;

namespace AppointmentEaseAPI.Mapper
{
    public class AppointmentEaseMapper : Profile
    {
        public AppointmentEaseMapper() 
        { 
            CreateMap<DoctorDM,DoctorSM>().ReverseMap();
            CreateMap<PatientDM,PatientSM>().ReverseMap();
            CreateMap<AppointmentDM,AppointmentSM>().ReverseMap();
        }
    }
}
