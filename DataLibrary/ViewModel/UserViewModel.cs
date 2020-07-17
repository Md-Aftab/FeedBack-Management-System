using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataLibrary.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.FeedbackViewModels = new HashSet<FeedbackViewModel>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public int UserTypeId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Conformpassword { get; set; }
        public string UserPhoto { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public bool IsFirstLogin { get; set; }
        public string HIgestQualification { get; set; }
        public string AboutInfo { get; set; }
        public bool IsActive { get; set; }
        public string UserTypeName { get; set; }
        public string Specialist { get; set; }
        public HttpPostedFileBase file { get; set; }
        public virtual ICollection<FeedbackViewModel> FeedbackViewModels { get; set; }
        public virtual UserTypeViewModel UserTypeViewModels { get; set; }

        public List<UserViewModel> UserViewModelList { get; set; }


        public Enum UserViewModeldrop { get; set; }

       
    }
     public enum drop{
                    ChangePassword,
                    Fucltylist
                    };
                    
}
