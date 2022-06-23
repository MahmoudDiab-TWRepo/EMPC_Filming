using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class GroupPriviageManager : Reposatory<GroupPriviage>
    {

        public GroupPriviageManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }
        public IEnumerable<GroupPriviage> GetAllPrivilageInGroup(int groupId)
        {
            return ctx.GroupPriviages.Where(s => s.GroupId == groupId);
        }
        public void DeletePrivilageInGroup(int groupId)
        {
            Delete(GetAllPrivilageInGroup(groupId).ToList());
        }

        public bool CheckPrivInGroup(int privId, int groupId)
        {
            return ctx.GroupPriviages.FirstOrDefault(s => s.GroupId == groupId && s.PrivilageId == privId) != null;
        }

        public bool IsGroupHavePermission(int groupId, string controllerrName, string actionName, string areaName = "")
        {
           
            bool flag = false;
            var routing = ctx.PrivilageRoutes.FirstOrDefault(s => s.Action.ToLower() == actionName.ToLower() &&
           s.Controller.ToLower() == controllerrName.ToLower() && s.Area.ToLower() == areaName.ToLower()
            );
            if (routing != null)                        
                flag = ctx.GroupPriviages.FirstOrDefault(s => s.PrivilageId == routing.PrivilageId && s.GroupId == groupId) != null;
            return flag;
            
        }
    }
}