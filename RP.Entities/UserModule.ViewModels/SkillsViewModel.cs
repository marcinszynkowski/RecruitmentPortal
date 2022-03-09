using RP.Entities.User;
using System.Collections.Generic;

namespace RP.Entities.UserModule.ViewModels
{
    public class SkillsViewModel
    {
       public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Skill> listSkills = new List<Skill>();

        public int selectedId { get; set; }
    }
}
