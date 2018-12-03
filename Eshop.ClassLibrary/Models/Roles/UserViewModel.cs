using System.Collections.Generic;

namespace Eshop.ClassLibrary.Models.Roles
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Id { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        //public string Email { get; set; }
    }
}
