using System.Collections.Generic;
using RP.Entities.User;

namespace RP.AdminModule.Interfaces
{
    public interface ICourseKindService
    {
        void CreateCourseKind(string courseKindName);
        void DeleteCourseKind(string courseKindName);
        List<CourseKind> GetAllCourseKinds();
        CourseKind GetCourseKindById(int courseKindId);
        CourseKind GetCourseKindByName(string courseKindName);
        string GetCourseKindName(int courseKindId);
        void SetCourseKindName(int courseKindId, string courseKindName);
    }
}