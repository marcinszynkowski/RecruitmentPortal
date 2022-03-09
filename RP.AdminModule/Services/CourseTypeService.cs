using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class CourseTypeService : ICourseTypeService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public CourseTypeService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public CourseType GetCourseTypeByName(string courseTypeName)
        {
            return _db.CourseTypes.Where(ct => ct.Name == courseTypeName).FirstOrDefault();
        }

        public CourseType GetCourseTypeById(int courseTypeId)
        {
            return _db.CourseTypes.Find(courseTypeId);
        }

        public string GetCourseTypeName(int courseTypeId)
        {
            return GetCourseTypeById(courseTypeId).Name;
        }

        public List<CourseType> GetAllCourseTypes()
        {
            return _db.CourseTypes.ToList();
        }
        #endregion

        #region Set
        public void SetCourseTypeName(int courseTypeId, string courseTypeName)
        {
            CourseType courseType = GetCourseTypeById(courseTypeId);
            courseType.Name = courseTypeName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateCourseType(string courseTypeName)
        {
            if (GetCourseTypeByName(courseTypeName) == null)
            {
                CourseType courseType = new CourseType
                {
                    Name = courseTypeName,
                };
                _db.CourseTypes.Add(courseType);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteCourseType(string courseTypeName)
        {
            CourseType courseType = GetCourseTypeByName(courseTypeName);

            if (courseType != null)
            {
                _db.CourseTypes.Remove(courseType);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}