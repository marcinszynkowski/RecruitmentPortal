using RP.Entities.User;
using System.Collections.Generic;

namespace RP.Entities.UserModule.ViewModels
{
    public class InterestsViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Interest> list;

        public int selectedId { get; set; }
    }
}
