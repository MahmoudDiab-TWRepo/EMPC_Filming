using Eagles.LMS.BLL;
using Eagles.LMS.Helper;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class UserDTO
    {
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string GroupName { get; set; }
        public int? GroupId { get; set; }

        public int ID { get; set; }


    }
    public class UsersApiController : ApiController
    {

        [HttpPost]
        [Route("api/User/Add")]
        public async Task<IHttpActionResult> CreateUsers(UserDTO userDTO)
        {
            UnitOfWork ctx = new UnitOfWork();
            var oldUser = await ctx.UserManager.GetValidateUserBy(userDTO.EmailAddress, userDTO.Mobile, 0);
            if (oldUser.Item1 != null)
            {
                return BadRequest(oldUser.Item2);
            }

            var user = new User
            {
                AccountState = AccountState.Approval,
                CreatedTime = Shared.GetDateTime(),
                FireBaseToken = "",
                Mobile = userDTO.Mobile,
                UserTybe =  UserTybe.Users,
                EmailAddress = userDTO.EmailAddress,
                FullName = userDTO.FullName,
                Image = "-",
                GroupId = userDTO.GroupId
            };
            var userCreated = ctx.UserManager.Register(user, userDTO.Password);
            return Json(new { Message = "successfully Users Added", State = 200 });
        }
        [HttpPost]
        [Route("api/User/Edit")]
        public async Task<IHttpActionResult> EditUser(UserDTO userDTO)
        {
            UnitOfWork ctx = new UnitOfWork();
            var oldUser = await ctx.UserManager.GetValidateUserBy(userDTO.EmailAddress, userDTO.Mobile, userDTO.ID);
            if (oldUser.Item1 != null)
            {
                return BadRequest(oldUser.Item2);
            }
            var user = ctx.UserManager.GetById(userDTO.ID);
            user.Mobile = userDTO.Mobile;
            user.UserTybe =  UserTybe.Users;
            user.EmailAddress = userDTO.EmailAddress;
            user.FullName = userDTO.FullName;
            user.GroupId = userDTO.GroupId;
            if (!string.IsNullOrEmpty(userDTO.Password))
            {
                byte[] passwordHash, passwordSalt;
                ctx.UserManager.CreateUserPassword(out passwordHash, out passwordSalt, userDTO.Password);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            ctx.UserManager.Edit(user);
            return Json(new { Message = "successfully Users Updated", State = 200 });
        }
        [HttpGet]
        [Route("api/User/{id}/Get")]
        public async Task<IHttpActionResult> Get(int id)
        {
            UnitOfWork ctx = new UnitOfWork();
            var user = ctx.UserManager.GetBy(id);
            return Ok(new
            {
                user.FullName,
                user.EmailAddress,
                user.Mobile,
                user.GroupId,
                IsTeacher = user.UserTybe == UserTybe.Teacher,

            });
        }
        [HttpGet]
        [Route("api/User/GetAll")]
        public IHttpActionResult GetAll(UserDTO userDTO)
        {
            UnitOfWork ctx = new UnitOfWork();
            var list = ctx.UserManager.GetAll().Where(c => !c.IsDeleted && c.UserTybe == UserTybe.Users).ToList().Select(c => new UserDTO
            {
                EmailAddress = c.EmailAddress,
                FullName = c.FullName,
                GroupId = c.GroupId,
                ID = c.Id,
                Mobile = c.Mobile,
                GroupName = c.GroupId == null ? "" : ctx.GroupsManager.GetBy((int)c.GroupId).Name
            }).ToList();
            return Json(new { list = list, State = 200 });
        }

    }
}
