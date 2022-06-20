using AutoMapper;
using Time_tracking_system.Domain.Entity;
using Time_tracking_system.Domain.ViewModels;

namespace Time_tracking_system.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Attributes, AttributesVM>().ReverseMap();
            CreateMap<Attributes_TypeRequest, Attributes_TypeRequestVM>().ReverseMap();
            CreateMap<Request, RequestVM>().ReverseMap();
            CreateMap<TypeRequest, TypeRequestVM>().ReverseMap();
            CreateMap<TypeRequest, TypeRequestVM>().ReverseMap();
            CreateMap<TypeVocation, TypeVocationVM>().ReverseMap();
            CreateMap<Employee, EmployeeVM>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(c => c.FirstName + " " + c.LastName + " " + c.Patronymic));
            CreateMap<EmployeeVM, Employee>();
            CreateMap<ValueRequestAttributes, ValueRequestAttributesVM>().ReverseMap();
            CreateMap<HolidayCalendar, HolidayCalendarVM>()
                .ForMember(x => x.Status, opt => opt.MapFrom(c => "Existing"));
            CreateMap<HolidayCalendarVM, HolidayCalendar>();
            CreateMap<Years, YearsVM>().ReverseMap();
        }
    }
}
