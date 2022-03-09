using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class CourseKindService : ICourseKindService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public CourseKindService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public CourseKind GetCourseKindByName(string courseKindName)
        {
            return _db.CourseKinds.Where(ck => ck.Name == courseKindName).FirstOrDefault();
        }

        public CourseKind GetCourseKindById(int courseKindId)
        {
            return _db.CourseKinds.Find(courseKindId);
        }

        public string GetCourseKindName(int courseKindId)
        {
            return _db.CourseKinds.Find(courseKindId).Name;
        }

        public List<CourseKind> GetAllCourseKinds()
        {
            return _db.CourseKinds.ToList();
        }
        #endregion

        #region Set
        public void SetCourseKindName(int courseKindId, string courseKindName)
        {
            CourseKind courseKind = GetCourseKindById(courseKindId);
            courseKind.Name = courseKindName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateCourseKind(string courseKindName)
        {
            if (GetCourseKindByName(courseKindName) == null)
            {
                CourseKind courseKind = new CourseKind
                {
                    Name = courseKindName,
                };
                _db.CourseKinds.Add(courseKind);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteCourseKind(string courseKindName)
        {
            CourseKind courseKind = GetCourseKindByName(courseKindName);

            if (courseKind != null)
            {
                _db.CourseKinds.Remove(courseKind);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}