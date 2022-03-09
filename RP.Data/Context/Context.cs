using System.Data.Entity;
using RP.Entities.Account;
using RP.Entities.Recruitment;
using RP.Entities.User;

namespace RP.Data.Context
{
    public class RPDbContext : DbContext
    {
        public RPDbContext() : base("RPDbContext")
        {

        }

        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<FormOfEmployment> FormOfEmployment { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<ProcessStatus> ProcessStatus { get; set; }
        public virtual DbSet<RecruitmentProcess> RecruitmentProcesses { get; set; }
        public virtual DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual DbSet<SurveyQuestionType> SurveyQuestionTypes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseKind> CourseKinds { get; set; }
        public virtual DbSet<CourseType> CourseTypes { get; set; }
        public virtual DbSet<DrivingLicense> DrivingLicenses { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<ForeignLanguage> ForeignLanguages { get; set; }
        public virtual DbSet<ForeignLanguageLevel> ForeignLanguageLevels { get; set; }
        public virtual DbSet<Interest> Interest { get; set; }
        public virtual DbSet<PersonalData> PersonalData { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserForeignLanguage> UserForeignLanguages { get; set; }
        public virtual DbSet<WorkExperience> WorkExperience { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
    
}
