using RP.Entities.User;
using System.Collections.Generic;

namespace RP.Entities.UserModule.ViewModels
{
    public class cvViewModel
    {
        public string Name;

        public string Birthday;

        public string City;

        public string Phone;

        public List<Education> education;

        public List<Course> courses;

        public List<UserForeignLanguage> languages;

        public List<Skill> skills;

        public List<WorkExperience> workexp;
    }
}
