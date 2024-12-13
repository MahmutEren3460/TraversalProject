using AutoMapper;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ContactDTOs;
using EntityLayer.Concrete;

namespace Traversal.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AddAnnouncementDTOs, Announcement>();
            CreateMap<Announcement, AddAnnouncementDTOs>();

            CreateMap<AppUserRegisterDTOs, AppUser>();
            CreateMap<AppUser, AppUserRegisterDTOs>();

            CreateMap<AppUserSignInDTOs, AppUser>();
            CreateMap<AppUser, AppUserSignInDTOs>();

            CreateMap<AnnouncementListDTO, Announcement>();
            CreateMap<Announcement,AnnouncementListDTO>();

            CreateMap<AnnouncementUpdateDTO, Announcement>();
            CreateMap<Announcement, AnnouncementUpdateDTO>();

            CreateMap<SendMessageDTO, ContactUs>().ReverseMap();
            //CreateMap<ContactUs, SendMessageDTO>();
        }
    }
}
