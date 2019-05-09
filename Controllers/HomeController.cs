using System.Web.Mvc;
using MongoDB.Driver;
using mvc5mongocrud.Models;
using mvc5mongocrud.App_Start;
using System.Collections.Generic;
using MongoDB.Bson;
using System;

namespace mvc5mongocrud.Controllers
{
    [RouteArea("api")]
    [RoutePrefix("v1")]
    public class HomeController : Controller
    {
        [Route("users")]
        public JsonResult Users()
        {
            IMongoCollection<Users> usersCollection = Common.Connector.GetCollection<Users>("users");
            List<Users> users = usersCollection.Find<Users>(Users => true).ToList();

            return (!(users.Count > 0)) ? Json(new { status = "ok", msg = "Any user not found" }, JsonRequestBehavior.AllowGet) : Json(users, JsonRequestBehavior.AllowGet);
        }

        [Route("users/{id}")]
        public JsonResult UserByID(string id)
        {
            IMongoCollection<Users> usersCollection = Common.Connector.GetCollection<Users>("users");
            List<Users> users = usersCollection.Find<Users>(x => x._id == id).ToList();

            return (!(users.Count > 0)) ? Json(new { status = "ok", msg = "User have id not found" }, JsonRequestBehavior.AllowGet) : Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, Route("users")]
        public JsonResult AddUser()
        {
            try
            {
                Users userModel = new Users();
                userModel.FullName = "Afulsamet";
                userModel.Username = "aful";
                userModel.Password = "123";
                userModel.Email = "afulsamet@gmail.com";
                userModel.Avatar = "https://avatars3.githubusercontent.com/u/27636298";
                userModel.Bio = "ben afulum";

                IMongoCollection<Users> usersCollection = Common.Connector.GetCollection<Users>("users");
                usersCollection.InsertOne(userModel);

                return Json(userModel);
            }
            catch (System.Exception)
            {
                return Json(new { status = "ok", account_created = false });
            }
        }

        [HttpDelete, Route("users/{id}")]
        public JsonResult DeleteUserByID(string id)
        {
            try
            {
                IMongoCollection<Users> usersCollection = Common.Connector.GetCollection<Users>("users");
                usersCollection.DeleteOne(x => x._id == id);

                return Json(new { status = "ok", account_deleted = true });
            }
            catch (System.Exception)
            {
                return Json(new { status = "ok", account_deleted = false });
            }
        }

        [HttpPut, Route("users/{id}")]
        public JsonResult UpdateUserByID(string id)
        {
            try
            {
                IMongoCollection<Users> usersCollection = Common.Connector.GetCollection<Users>("users");
                var update = Builders<Users>.Update.Set("registerDate", DateTime.Now);
                usersCollection.UpdateOne(x => x._id == id, update);

                return Json(new { status = "ok", account_updated = true });
            }
            catch (System.Exception)
            {
                return Json(new { status = "ok", account_updated = false });
            }
        }

    }
}