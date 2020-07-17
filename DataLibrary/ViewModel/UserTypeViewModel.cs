using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ViewModel
{
    public class UserTypeViewModel
    {
        public UserTypeViewModel()
        {
            this.UserViewModels = new HashSet<UserViewModel>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }

        public virtual ICollection<UserViewModel> UserViewModels { get; set; }
    }
}
