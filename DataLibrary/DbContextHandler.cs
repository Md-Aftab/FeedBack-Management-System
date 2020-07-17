using DataLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class DbContextHandler
    {

        FMSEntities context = new FMSEntities();
        public User InsertUser(User objmodel)
        {
            var result = (from c in context.Users
                          where c.Email == objmodel.Email
                          select c).FirstOrDefault();
            if (result != null)
            {
                result.FirstName = objmodel.FirstName;
                result.LastName = objmodel.LastName;
                result.Mobile = objmodel.Mobile;
                if (!string.IsNullOrWhiteSpace(objmodel.UserPhoto))
                {
                    result.UserPhoto = objmodel.UserPhoto;
                }
                
                result.UpdatedOn = DateTime.Now;
                var res = context.SaveChanges();
            }
            else
            {
                context.Users.Add(objmodel);
                var res = context.SaveChanges();
            }
            return objmodel;
        }

        public User InsertUpdateUserById(User objmodel)
        {
            var result = (from c in context.Users
                          where c.Id == objmodel.Id
                          select c).FirstOrDefault();
            if (result != null)
            {
                result.FirstName = objmodel.FirstName;
                result.LastName = objmodel.LastName;
                result.Mobile = objmodel.Mobile;
                result.AboutInfo = objmodel.AboutInfo;
                result.HIgestQualification = objmodel.HIgestQualification;
                if (!string.IsNullOrWhiteSpace(objmodel.UserPhoto))
                {
                    result.UserPhoto = objmodel.UserPhoto;
                }

                result.UpdatedOn = DateTime.Now;
                var res = context.SaveChanges();
            }   
            return objmodel;
        }
        public User InsertUpdatepassword(User objmodel)
        {
            var result = (from c in context.Users
                          where c.Password == objmodel.Password
                          select c).FirstOrDefault();
            if (result != null)
            {
                result.Password = objmodel.NewPassword;
        
                result.UpdatedOn = DateTime.Now;
                var res = context.SaveChanges();
            
        }
            return objmodel;
        }

        public UserViewModel GetUserLogin(string Email, string Password)
        {
            var result = (from c in context.Users
                          join d in context.UserTypes on c.UserTypeId equals d.Id
                          where c.IsActive == true && c.Email == Email && c.Password == Password
                          select new UserViewModel
                          {
                              IsActive = c.IsActive,
                              AboutInfo = c.AboutInfo,
                              CreatedOn = c.CreatedOn,
                              UpdatedOn = c.UpdatedOn,
                              Email = c.Email,
                              FirstName = c.FirstName,
                              HIgestQualification = c.HIgestQualification,
                              Id = c.Id,
                              IsFirstLogin = c.IsFirstLogin,
                              LastName = c.LastName,
                              Mobile = c.Mobile,
                              Password = c.Password,
                              UserPhoto = c.UserPhoto,
                              UserTypeName = d.TypeName,
                              UserTypeId = c.UserTypeId,
                              Specialist = c.Specialist
                          }).FirstOrDefault();
            return result;
        }
        public User GetRegistrationCheckData(string Email)
        {
            var result = (from c in context.Users
                          where c.Email == Email
                          select c).FirstOrDefault();
            return result;
        }
        public List<UserViewModel> GetUserDataList()
        {
            var result = (from c in context.Users
                          join d in context.UserTypes on c.UserTypeId equals d.Id
                          where c.IsActive == true
                          select new UserViewModel
                          {
                              IsActive = c.IsActive,
                              AboutInfo = c.AboutInfo,
                              CreatedOn = c.CreatedOn,
                              UpdatedOn = c.UpdatedOn,
                              Email = c.Email,
                              FirstName = c.FirstName,
                              HIgestQualification = c.HIgestQualification,
                              Id = c.Id,
                              IsFirstLogin = c.IsFirstLogin,
                              LastName = c.LastName,
                              Mobile = c.Mobile,
                              Password = c.Password,
                              UserPhoto = c.UserPhoto,
                              UserTypeName = d.TypeName,
                              Specialist = c.Specialist
                          }).ToList();
            return result;
        }

        public List<UserViewModel> GetUserDataListByTypeId(int TypeId, int skip = 0, int limit = 5)
        {
            var result = (from c in context.Users
                          join d in context.UserTypes on c.UserTypeId equals d.Id
                          where c.IsActive == true && c.UserTypeId == TypeId
                          select new UserViewModel
                          {
                              IsActive = c.IsActive,
                              AboutInfo = c.AboutInfo,
                              CreatedOn = c.CreatedOn,
                              UpdatedOn = c.UpdatedOn,
                              Email = c.Email,
                              FirstName = c.FirstName,
                              HIgestQualification = c.HIgestQualification,
                              Id = c.Id,
                              IsFirstLogin = c.IsFirstLogin,
                              LastName = c.LastName,
                              Mobile = c.Mobile,
                              Password = c.Password,
                              UserPhoto = c.UserPhoto,
                              UserTypeName = d.TypeName,
                              Specialist = c.Specialist
                          }).OrderByDescending(x => x.Id).Skip(skip).Take(limit).ToList();
            return result;
        }
        public List<UserViewModel> GetTotalUserDataListByTypeId(int TypeId)
        {
            var result = (from c in context.Users
                          join d in context.UserTypes on c.UserTypeId equals d.Id
                          where c.IsActive == true && c.UserTypeId == TypeId
                          select new UserViewModel
                          {
                              IsActive = c.IsActive,
                              AboutInfo = c.AboutInfo,
                              CreatedOn = c.CreatedOn,
                              UpdatedOn = c.UpdatedOn,
                              Email = c.Email,
                              FirstName = c.FirstName,
                              HIgestQualification = c.HIgestQualification,
                              Id = c.Id,
                              IsFirstLogin = c.IsFirstLogin,
                              LastName = c.LastName,
                              Mobile = c.Mobile,
                              Password = c.Password,
                              UserPhoto = c.UserPhoto,
                              UserTypeName = d.TypeName,
                              Specialist = c.Specialist
                          }).OrderByDescending(x => x.Id).ToList();
            return result;
        }


        public UserViewModel GetUserDataById(int UserId)
        {
            var result = (from c in context.Users
                          join d in context.UserTypes on c.UserTypeId equals d.Id
                          where c.IsActive == true && c.Id == UserId
                          select new UserViewModel
                          {
                              IsActive = c.IsActive,
                              AboutInfo = c.AboutInfo,
                              CreatedOn = c.CreatedOn,
                              UpdatedOn = c.UpdatedOn,
                              Email = c.Email,
                              FirstName = c.FirstName,
                              HIgestQualification = c.HIgestQualification,
                              Id = c.Id,
                              IsFirstLogin = c.IsFirstLogin,
                              LastName = c.LastName,
                              Mobile = c.Mobile,
                              Password = c.Password,
                              UserPhoto = c.UserPhoto,
                              UserTypeName = d.TypeName,
                              Specialist = c.Specialist
                          }).FirstOrDefault();
            return result;
        }
        public Feedback InsertFeedback(Feedback objmodel)
        {
            var result = (from c in context.Feedbacks
                          where c.Id == objmodel.Id
                          select c).FirstOrDefault();
            if (result != null)
            {
                result.Recommend = objmodel.Recommend;
                result.Comments = objmodel.Comments;
                result.UpdatedOn = DateTime.Now;
                var res = context.SaveChanges();
            }
            else
            {
                context.Feedbacks.Add(objmodel);
                var res = context.SaveChanges();
            }
            return objmodel;
        }
        public List<FeedbackViewModel> GetFeedbackDataList(int FacultyId)
        {
            var result = (from c in context.Feedbacks
                          join d in context.Users on c.UserId equals d.Id

                          where c.FacultyId == FacultyId
                          select new FeedbackViewModel
                          {
                              CreatedOn = c.CreatedOn,
                              Comments = c.Comments,
                              Id = c.Id,
                              Recommend = c.Recommend,
                              UpdatedOn = c.UpdatedOn,
                              UserName = d.FirstName + " " + d.LastName,
                              UserPhoto = d.UserPhoto,
                              UserMobile = d.Mobile
                          }).ToList();
            return result;
        }

    }
}
