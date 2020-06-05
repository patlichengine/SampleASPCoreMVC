using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Profiles
{
    public class CentreProfile : Profile
    {
        public CentreProfile()
        {
            //listing of centres
            CreateMap<Entities.Centres, Models.CentresDto>()
                .ForMember(
                    dest => dest.CentreNumber,
                    opt => opt.MapFrom(src => src.CentreNo)
                )
                //.ForMember(
                //    dest => dest.SchoolCategory,
                //    opt =>
                //    {
                //        opt.PreCondition(src => src.SchoolCategory != null);
                //        opt.MapFrom(src => src.SchoolCategory);
                //    }

                //)
                .ForMember(
                    dest => dest.CentreSanctions,
                    opt =>
                    {
                        opt.PreCondition(src => src.CentreSanctions != null);
                        opt.MapFrom(src => src.CentreSanctions.ToList());
                    }
                );

            //create a new centre mapping
            CreateMap<Models.CentreCreateDto, Entities.Centres>()
                .ForMember(
                    dest => dest.CentreNo,
                    opt => opt.MapFrom(src => src.CentreNumber)
            );


            //update an existing centre
            CreateMap<Models.CentreUpdateDto, Entities.Centres>()
                .ForMember(
                    dest => dest.CentreNo,
                    opt => opt.MapFrom(src => src.CentreNumber)
            );
        }
    }
}
