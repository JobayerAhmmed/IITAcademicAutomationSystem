using Foolproof;
using IITAcademicAutomationSystem.Areas.One.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One
{
    // Course Credit check
    public class CourseCreditEqualAttribute : ModelAwareValidationAttribute
    {
        static CourseCreditEqualAttribute() { Register.Attribute(typeof(CourseCreditEqualAttribute)); }

        public override bool IsValid(object value, object container)
        {
            var model = (CourseCreateViewModel)container;
            double courseCredit = model.CourseCredit;
            double creditTheory = model.CreditTheory;
            double creditLab = (double)value;

            return creditTheory + creditLab == courseCredit;
        }
    }

    // Dependent course check
    public class DependentCourseEqualAttribute : ModelAwareValidationAttribute
    {
        static DependentCourseEqualAttribute() { Register.Attribute(typeof(DependentCourseEqualAttribute)); }

        public override bool IsValid(object value, object container)
        {
            var model = (CourseCreateViewModel)container;
            int dependentCourseId1 = model.DependentCourseId1;
            int dependentCourseId2 = (int)value;
            if (dependentCourseId1 == 0 && dependentCourseId2 == 0)
                return true;
            return dependentCourseId1 != dependentCourseId2;
        }
    }

    // First Dependent course selected
    public class FirstDependentCourseEmptyAttribute : ModelAwareValidationAttribute
    {
        static FirstDependentCourseEmptyAttribute() { Register.Attribute(typeof(FirstDependentCourseEmptyAttribute)); }

        public override bool IsValid(object value, object container)
        {
            var model = (CourseCreateViewModel)container;
            int dependentCourseId1 = model.DependentCourseId1;
            int dependentCourseId2 = (int)value;
            if (dependentCourseId1 == 0 && dependentCourseId2 == 0)
                return true;
            if (dependentCourseId1 == 0 && dependentCourseId2 != 0)
                return false;
            return true;
        }
    }
}