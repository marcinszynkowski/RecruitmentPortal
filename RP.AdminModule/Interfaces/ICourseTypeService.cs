using System.Collections.Generic;
using RP.Entities.User;

namespace RP.AdminModule.Interfaces
{
    public interface ICourseTypeService
    {
        void CreateCourseType(string courseTypeName);
        void DeleteCourseType(string courseTypeName);
        List<CourseType> GetAllCourseTypes();
        CourseType GetCourseTypeById(int courseTypeId);
        CourseType GetCourseTypeByName(string courseTypeName);
        string GetCourseTypeName(int courseTypeId);
        void SetCourseTypeName(int courseTypeId, string courseTypeName);
    }
}