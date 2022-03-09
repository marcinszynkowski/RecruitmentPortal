// Foreign Languages
// write : UserForeignLanguages

using RP.Entities.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.UserModule.ViewModels
{
    public class ForeignLanguagesViewModel 
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ForeginLanguageId { get; set; } // lista rozwijana

        public int ForeignLangauegeLevelId { get; set; }  // lsita rozwijana

        public int selectedId { get; set; }

        public List<ForeignLanguage> langList = new List<ForeignLanguage>();

        public List<ForeignLanguageLevel> langLevelsList = new List<ForeignLanguageLevel>();

        public List<ForeignLanguage> langUserLangs = new List<ForeignLanguage>();
    }
}
