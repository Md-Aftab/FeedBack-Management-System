using DataLibrary;
using DataLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FMS.Controllers
{
    public class HomeController : Controller
    {
        DbContextHandler Db = new DbContextHandler();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration()
        {
            UserViewModel objModel = new UserViewModel();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel objModel)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            string StorageRoot1 = System.Web.HttpContext.Current.Server.MapPath("~/UserPhoto/" + objModel.Mobile);
            bool exists = System.IO.Directory.Exists(StorageRoot1);
            if (!exists)
                System.IO.Directory.CreateDirectory(StorageRoot1);

            if (objModel.file != null && objModel.file.ContentLength > 0)
            {

                string fileExt = System.IO.Path.GetExtension(objModel.file.FileName);
                string mimeType = System.Web.MimeMapping.GetMimeMapping(objModel.file.FileName);
                string s = mimeType;
                string pdfExt = objModel.file.ContentType;
                var supportedTypes = new[] { ".jpg", ".jpeg", ".bmp", ".png" };
                var filesize = 10000;
                if (objModel.file.ContentType == "image/jpeg" || objModel.file.ContentType == "image/png" || objModel.file.ContentType == "image/bmp")
                {
                    if (!supportedTypes.Contains(fileExt))
                    {
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File Extension Is InValid - Only Upload JPG/PNG/BMP File !!");
                    }
                    else if (objModel.file.ContentLength > (filesize * 1024))
                    {
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File size Should Be UpTo " + filesize + "KB");
                    }
                    else
                    {
                        var fullPath = StorageRoot1 + "/" + objModel.file.FileName;
                        objModel.file.SaveAs(fullPath);
                        objModel.UserPhoto = objModel.file.FileName;

                        User objuser = new User();
                        objuser.AboutInfo = objModel.AboutInfo;
                        objuser.CreatedOn = DateTime.Now;
                        objuser.Email = objModel.Email;
                        objuser.FirstName = objModel.FirstName;
                        objuser.HIgestQualification = objModel.HIgestQualification;
                        objuser.IsActive = true;
                        objuser.IsFirstLogin = false;
                        objuser.LastName = objModel.LastName;
                        objuser.Mobile = objModel.Mobile;
                        objuser.Password = objModel.Password;
                        objuser.UpdatedOn = DateTime.Now;
                        objuser.UserPhoto = objModel.UserPhoto;
                        objuser.UserTypeId = 4;

                        var result = Db.InsertUser(objuser);

                        if (result.Id > 0)
                        {
                            jsonResult.Add("success", true);
                            jsonResult.Add("msg", "User data save successfully");
                        }
                        else
                        {
                            jsonResult.Add("success", true);
                            jsonResult.Add("msg", "Unable to save user data");
                        }
                    }
                }
                else
                {
                    jsonResult.Add("success", true);
                    jsonResult.Add("msg", "Please upload User photo !!");
                }
            }


            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddFaculty()
        {
            UserViewModel objModel = new UserViewModel();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult AddFaculty(UserViewModel objModel)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            string StorageRoot1 = System.Web.HttpContext.Current.Server.MapPath("~/UserPhoto/" + objModel.Mobile);
            bool exists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/UserPhoto/" + objModel.Mobile));
            if (!exists)
                System.IO.Directory.CreateDirectory(StorageRoot1);
            if (objModel.file != null && objModel.file.ContentLength > 0)
            {

                string fileExt = System.IO.Path.GetExtension(objModel.file.FileName);
                string mimeType = System.Web.MimeMapping.GetMimeMapping(objModel.file.FileName);
                string s = mimeType;
                string pdfExt = objModel.file.ContentType;
                var supportedTypes = new[] { ".jpg", ".jpeg", ".bmp", ".png" };
                var filesize = 10000;
                if (objModel.file.ContentType == "image/jpeg" || objModel.file.ContentType == "image/png" || objModel.file.ContentType == "image/bmp")
                {
                    if (!supportedTypes.Contains(fileExt))
                    {
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File Extension Is InValid - Only Upload JPG/PNG/BMP File !!");
                    }
                    else if (objModel.file.ContentLength > (filesize * 1024))
                    {
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File size Should Be UpTo " + filesize + "KB");
                    }
                    else
                    {
                        var fullPath = StorageRoot1 + "/" + objModel.file.FileName;
                        objModel.file.SaveAs(fullPath);
                        objModel.UserPhoto = objModel.file.FileName;

                        User objuser = new User();
                        objuser.AboutInfo = objModel.AboutInfo;
                        objuser.CreatedOn = DateTime.Now;
                        objuser.Email = objModel.Email;
                        objuser.FirstName = objModel.FirstName;
                        objuser.HIgestQualification = objModel.HIgestQualification;
                        objuser.IsActive = true;
                        objuser.IsFirstLogin = true;
                        objuser.LastName = objModel.LastName;
                        objuser.Mobile = objModel.Mobile;
                        objuser.Password = objModel.Password;
                        objuser.UpdatedOn = DateTime.Now;
                        objuser.UserPhoto = objModel.UserPhoto;
                        objuser.Specialist = objModel.Specialist;
                        objuser.UserTypeId = 3;

                        var result = Db.InsertUser(objuser);

                        if (result.Id > 0)
                        {
                            jsonResult.Add("success", true);
                            jsonResult.Add("msg", "User data save successfully");
                        }
                        else
                        {
                            jsonResult.Add("success", true);
                            jsonResult.Add("msg", "Unable to save user data");
                        }
                    }
                }
                else
                {
                    jsonResult.Add("success", true);
                    jsonResult.Add("msg", "Please upload User photo !!");
                }
            }


            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegistrationCheck(string email)
        {
            var reuslt = Db.GetRegistrationCheckData(email);
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            if (reuslt != null)
            {
                jsonResult.Add("Exist", true);
            }
            else
            {
                jsonResult.Add("Exist", false);
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel objModel)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            var result = Db.GetUserLogin(objModel.Email, objModel.Password);
            if (result != null)
            {
                Session["UserData"] = result;
                jsonResult.Add("success", true);
                jsonResult.Add("msg", "Login Successfull");
            }
            else
            {
                jsonResult.Add("success", true);
                jsonResult.Add("msg", "Please enter valid username and password");
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FacultyViewPartial()
        {
            UserViewModel objModel = new UserViewModel();
            var result = Db.GetUserDataListByTypeId(3, 0, 3);
            if (result != null)
            {
                objModel.UserViewModelList = result;

            }
            return PartialView(objModel);
        }
        public ActionResult FacultyView()
        {
            UserViewModel objModel = new UserViewModel();
            var result = Db.GetUserDataListByTypeId(3, 0, 10);
            if (result != null)
            {
                objModel.UserViewModelList = result;

            }
            return View(objModel);
        }
        public ActionResult FacultyDetails(int Id)
        {
            UserViewModel objModel = new UserViewModel();
            var result = Db.GetUserDataById(Id);
            if (result != null)
            {
                objModel = result;
            }
            return PartialView(objModel);
        }

        public ActionResult StudentViewPartial()
        {
            UserViewModel objModel = new UserViewModel();
            var result = Db.GetUserDataListByTypeId(4, 0, 4);
            if (result != null)
            {
                objModel.UserViewModelList = result;

            }
            return PartialView(objModel);
        }
        public ActionResult StudentView()
        {
            UserViewModel objModel = new UserViewModel();
            var result = Db.GetUserDataListByTypeId(4, 0, 10);
            if (result != null)
            {
                objModel.UserViewModelList = result;

            }
            return View(objModel);
        }

        public ActionResult FeedBackEntry(FeedbackViewModel objModel)
        {
            Feedback objtbl = new Feedback();
            objtbl.Comments = objModel.Comments;
            objtbl.CreatedOn = DateTime.Now;
            objtbl.UpdatedOn = DateTime.Now;
            objtbl.Recommend = objModel.Recommend;
            objtbl.UserId = objModel.UserId;
            objtbl.FacultyId = objModel.FacultyId;

            var reuslt = Db.InsertFeedback(objtbl);
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            if (reuslt != null)
            {
                jsonResult.Add("success", true);
                jsonResult.Add("msg", "Feedback has been posted");
            }
            else
            {
                jsonResult.Add("success", false);
                jsonResult.Add("msg", "Unable to post feedback please try again");
            }

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewFeedbackPartial(int Id)
        {
            FeedbackViewModel objModel = new FeedbackViewModel();
            var result = Db.GetFeedbackDataList(Id);
            if (result != null)
            {
                objModel.FeedbackViewModelList = result;
            }
            return PartialView(objModel);
        }
        public ActionResult Logut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult FacultyList()
        {
            if (Session["UserData"] != null)
            {
                var userdata = (UserViewModel)Session["UserData"];
                if (userdata.UserTypeId == 2)
                {
                    UserViewModel objmodel = new UserViewModel();
                    var result = Db.GetTotalUserDataListByTypeId(3);
                    objmodel.UserViewModelList = result;
                    return View(objmodel);
                }
                else
                {
                    return View("NotAutorized");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult NotAutorized()
        {
            return View();
        }
        public ActionResult UpdateUser(int id)
        {
            var result = Db.GetUserDataById(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult UpdateUser(UserViewModel objModel)
        {
            int error = 0;
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            string StorageRoot1 = System.Web.HttpContext.Current.Server.MapPath("~/UserPhoto/" + objModel.Mobile);
            bool exists = System.IO.Directory.Exists(StorageRoot1);
            if (!exists)
                System.IO.Directory.CreateDirectory(StorageRoot1);

            if (objModel.file != null && objModel.file.ContentLength > 0)
            {
                string fileExt = System.IO.Path.GetExtension(objModel.file.FileName);
                string mimeType = System.Web.MimeMapping.GetMimeMapping(objModel.file.FileName);
                string s = mimeType;
                string pdfExt = objModel.file.ContentType;
                var supportedTypes = new[] { ".jpg", ".jpeg", ".bmp", ".png" };
                var filesize = 10000;
                if (objModel.file.ContentType == "image/jpeg" || objModel.file.ContentType == "image/png" || objModel.file.ContentType == "image/bmp")
                {
                    if (!supportedTypes.Contains(fileExt))
                    {
                        error = 1;
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File Extension Is InValid - Only Upload JPG/PNG/BMP File !!");
                    }
                    else if (objModel.file.ContentLength > (filesize * 1024))
                    {
                        error = 1;
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File size Should Be UpTo " + filesize + "KB");
                    }
                    else
                    {
                        var fullPath = StorageRoot1 + "/" + objModel.file.FileName;
                        objModel.file.SaveAs(fullPath);
                        objModel.UserPhoto = objModel.file.FileName;
                    }
                }

            }
            if (error == 0)
            {
                User objuser = new User();
                objuser.Id = objModel.Id;
                objuser.AboutInfo = objModel.AboutInfo;
                objuser.CreatedOn = DateTime.Now;
                objuser.Email = objModel.Email;
                objuser.FirstName = objModel.FirstName;
                objuser.HIgestQualification = objModel.HIgestQualification;
                objuser.IsActive = true;
                objuser.IsFirstLogin = false;
                objuser.LastName = objModel.LastName;
                objuser.Mobile = objModel.Mobile;
                objuser.Password = objModel.Password;
                objuser.UpdatedOn = DateTime.Now;
                objuser.UserPhoto = objModel.UserPhoto;
                objuser.UserTypeId = 4;

                var result = Db.InsertUpdateUserById(objuser);

                if (result.Id > 0)
                {
                    jsonResult.Add("success", true);
                    jsonResult.Add("msg", "User data save successfully");
                }
                else
                {
                    jsonResult.Add("success", true);
                    jsonResult.Add("msg", "Unable to save user data");
                }
            }


            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentsList()
        {
            if (Session["UserData"] != null)
            {
                var userdata = (UserViewModel)Session["UserData"];
                if (userdata.UserTypeId == 2)
                {
                    UserViewModel objmodel = new UserViewModel();
                    var result = Db.GetTotalUserDataListByTypeId(4);
                    objmodel.UserViewModelList = result;
                    return View(objmodel);
                }
                else
                {
                    return View("NotAutorized");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        public ActionResult UpdateProfile()
        {
            UserViewModel objmodel = new UserViewModel();
            if (Session["UserData"] != null)
            {
                var userdata = (UserViewModel)Session["UserData"];
                var result = Db.GetUserDataById(userdata.Id);
                return View(result);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult UpdateProfile(UserViewModel objModel)
        {
            int error = 0;
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            string StorageRoot1 = System.Web.HttpContext.Current.Server.MapPath("~/UserPhoto/" + objModel.Mobile);
            bool exists = System.IO.Directory.Exists(StorageRoot1);
            if (!exists)
                System.IO.Directory.CreateDirectory(StorageRoot1);

            if (objModel.file != null && objModel.file.ContentLength > 0)
            {
                string fileExt = System.IO.Path.GetExtension(objModel.file.FileName);
                string mimeType = System.Web.MimeMapping.GetMimeMapping(objModel.file.FileName);
                string s = mimeType;
                string pdfExt = objModel.file.ContentType;
                var supportedTypes = new[] { ".jpg", ".jpeg", ".bmp", ".png" };
                var filesize = 10000;
                if (objModel.file.ContentType == "image/jpeg" || objModel.file.ContentType == "image/png" || objModel.file.ContentType == "image/bmp")
                {
                    if (!supportedTypes.Contains(fileExt))
                    {
                        error = 1;
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File Extension Is InValid - Only Upload JPG/PNG/BMP File !!");
                    }
                    else if (objModel.file.ContentLength > (filesize * 1024))
                    {
                        error = 1;
                        jsonResult.Add("success", false);
                        jsonResult.Add("msg", "File size Should Be UpTo " + filesize + "KB");
                    }
                    else
                    {
                        var fullPath = StorageRoot1 + "/" + objModel.file.FileName;
                        objModel.file.SaveAs(fullPath);
                        objModel.UserPhoto = objModel.file.FileName;
                    }
                }
            }
            if (error == 0)
            {
                User objuser = new User();
                objuser.Id = objModel.Id;
                objuser.AboutInfo = objModel.AboutInfo;
                objuser.CreatedOn = DateTime.Now;
                objuser.Email = objModel.Email;
                objuser.FirstName = objModel.FirstName;
                objuser.HIgestQualification = objModel.HIgestQualification;
                objuser.IsActive = true;
                objuser.IsFirstLogin = false;
                objuser.LastName = objModel.LastName;
                objuser.Mobile = objModel.Mobile;
                objuser.Password = objModel.Password;
                objuser.UpdatedOn = DateTime.Now;
                objuser.UserPhoto = objModel.UserPhoto;
                objuser.UserTypeId = 4;

                var result = Db.InsertUpdateUserById(objuser);

                if (result.Id > 0)
                {
                    jsonResult.Add("success", true);
                    jsonResult.Add("msg", "User data save successfully");
                }
                else
                {
                    jsonResult.Add("success", true);
                    jsonResult.Add("msg", "Unable to save user data");
                }
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Changepassword()
        {
            UserViewModel objmodel = new UserViewModel();
            if (Session["UserData"] != null)
            {
                var userdata = (UserViewModel)Session["UserData"];
                var result = Db.GetUserDataById(userdata.Id);
                return View(result);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult Changepassword(UserViewModel objmodel)
        {
            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            User objuser = new User();
            objuser.Id = objmodel.Id;
            objuser.Password = objmodel.Password;
            objuser.NewPassword = objmodel.NewPassword;
            objuser.ConformPassword = objmodel.Conformpassword;
            var result = Db.InsertUpdatepassword(objuser);

            if (result.Id > 0)
            {
                jsonResult.Add("success", true);
                jsonResult.Add("msg", "User Password change successfully");
            }
            else
            {
                jsonResult.Add("success", true);
                jsonResult.Add("msg", "Unable to save user data");
            }
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

    }

}
