using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.ViewModel
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public Nullable<bool> Recommend { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public string UserName { get; set; }
        public string UserMobile { get; set; }
        public string UserPhoto { get; set; }
        public int FacultyId { get; set; }
        public int feedbackcount { get; set; }
        public List<FeedbackViewModel> FeedbackViewModelList { get; set; }

        public virtual UserViewModel UserViewModels { get; set; }
    }
}
