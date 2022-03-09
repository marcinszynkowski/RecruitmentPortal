using RP.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.UserModule.ViewModels
{
    public class CoursesViewModel
    {
       [Key]
        public int CourseID { get; set; }

        public int UserID { get; set; }

        public int CourseTypeId { get; set; } // lista rozwijana

        public int CourseKindId { get; set; } // lista rozwijana

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string Name { get; set; }

        public List<Course> courseList = new List<Course>();
        public List<CourseKind> courseKinds = new List<CourseKind>();
        public List<CourseType> courseTypes = new List<CourseType>();

        public int selectedId { get; set; }
    }
}
