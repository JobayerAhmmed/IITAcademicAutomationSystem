using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
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

                // Teacher
                cfg.CreateMap<ApplicationUser, IITAcademicAutomationSystem.Models.TeacherIndexViewModel>();

                // Role
                cfg.CreateMap<IdentityRole, RoleIndexViewModel>().ReverseMap();

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