using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Eagles.LMS.BLL
{
    public class UserManager : Reposatory<User>
    {
        public UserManager(EmcNewsContext ctx) : base(ctx)
        {

        }
        public User GetByEmail(string email)
        {
            return ctx.Users.FirstOrDefault(s => !s.IsDeleted && s.EmailAddress.ToLower() == email.ToLower());
        }
        public User GetByMobile(string mobile)
        {
            return GetAll().FirstOrDefault(S => !S.IsDeleted && S.Mobile.ToLower() == mobile.ToLower());
        }

        public async Task<(User, string)> GetValidateUserBy(string emailAddress, string phone, int userId)
        {

            User user = new User();

            user = await ctx.Users.FirstOrDefaultAsync(s => s.IsDeleted != true && s.EmailAddress != null &&
            s.Id != userId && s.EmailAddress.ToLower() == emailAddress.ToLower());

            if (user != null && !string.IsNullOrEmpty(emailAddress))
            {

                return (user, "Email Address Is Already Existing");

            }
            user = await ctx.Users.FirstOrDefaultAsync(s => s.IsDeleted != true && s.Mobile != null &&
        s.Id != userId && s.Mobile == phone);

            if (user != null && !string.IsNullOrEmpty(phone))
            {

                return (user, "Phone-Number Is Already Existing");

            }
            return (null, "");



        }

        public User GetById(int Id)
        {
            return ctx.Users.AsNoTracking().FirstOrDefault(S => S.Id == Id);
        }
        public User Login(string loginName, string password)
        {
            var sss = ctx.Users.Where(s => !s.IsDeleted).ToList();
            var user = ctx.Users.FirstOrDefault(s => !s.IsDeleted && (s.Mobile == loginName || (s.EmailAddress != null && s.EmailAddress.ToLower() == loginName.ToLower())));
            if (user == null)
                return null;
            if (!Verification(user.PasswordHash, user.PasswordSalt, password))
                return null;
            else
                return user;
        }

        public User Register(User user, string password)
        {
            user.OTPTIME = DateTime.Now;
            byte[] passwordHash, passwordSalt;
            CreateUserPassword(out passwordHash, out passwordSalt, password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            try
            {
                var userCreated = ctx.Users.Add(user);

                ctx.SaveChanges();
                return userCreated;
            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public void CreateUserPassword(out byte[] passwordHash, out byte[] passwordSalt, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool Verification(byte[] passwordHash, byte[] passwordSalt, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computePasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computePasswordHash.Length; i++)
                {
                    if (computePasswordHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
        public User GetBindUserById(int id)
        {
            return ctx.Users.FirstOrDefault(c => c.Id == id);
        }
        public IEnumerable<User> GetAllAdminUser()
        {
            return ctx.Users.Where(s => s.IsDeleted != true).Where(s => s.UserTybe != UserTybe.Studnet && s.UserTybe != UserTybe.Parent)
                .OrderByDescending(s=>s.Id);
        }

        public bool CheckUserNotHavePermissionsToJoinApp(int userId)
        {
            var user = GetBy(userId);
            return user == null || user.IsDeleted || user.AccountState != AccountState.Approval;
        }
    }
}