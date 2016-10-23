using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration MapperConfiguration;
        public static void RegisterMappings()
        {
            MapperConfiguration = new MapperConfiguration(cfg => {

                // Course
                cfg.CreateMap<Course, CourseCreateViewModel>().ReverseMap();
                cfg.CreateMap<Course, CourseEditViewModel>().ReverseMap();
                cfg.CreateMap<Course, CourseIndexViewModel>();
                cfg.CreateMap<Course, CourseDetailsViewModel>();

                // Batch
                cfg.CreateMap<Batch, BatchCreateViewModel>();
                cfg.CreateMap<Batch, BatchIndexViewModel>();

            });
        }
    }
}