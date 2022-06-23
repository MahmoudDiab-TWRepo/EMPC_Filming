using Eagles.LMS.App_GlobalResources;
using Eagles.LMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eagles.LMS.Web_Services
{
    public class PrivilagesController : ApiController
    {

        private readonly UnitOfWork ctx = new UnitOfWork();
        [HttpGet]
        [Route("api/web/Privilages/GetAll/Group/{groupId}")]
        public IHttpActionResult GetAllPrivilages(int? groupId)
        {
            GroupsTreePrivilageDTO privilages;
            privilages = groupId == null ? ctx.GroupsManager.GetGroupsTreePrivilage() : ctx.GroupsManager.GetGroupsTreePrivilage((int)groupId);

            List<PrivilageTreeCustome> PrivilageTreeCustome = new List<PrivilageTreeCustome>();
            foreach (var privilage in privilages.PrivilageTree)
            {
                List<ChildPrivilageTree> ChildPrivilageTree = new List<ChildPrivilageTree>();
                foreach (var item in privilage.Privilages)
                {
                    ChildPrivilageTree.Add(new ChildPrivilageTree
                    {
                        Id = item.Id,
                        IsChecked = item.IsChecked,
                        IsRoute = item.IsRoute,
                        OrderId = item.OrderId,
                        MenueName = item.MenueName,//Resources.
                        ParentId = item.ParentId,
                        ShowInMenue = item.ShowInMenue,
                    });
                }
                PrivilageTreeCustome.Add(new PrivilageTreeCustome
                {
                    Id = privilage.Id,
                    ParentName = privilage.ParentName,//
                    Privilages = ChildPrivilageTree,
                    IsChecked = privilage.Privilages.Any(m => m.IsChecked),

                });

            }

            return Ok(new
            {
                GroupId = privilages.Group.Id,
                GroupName = privilages.Group.Name,
                Tree = PrivilageTreeCustome
            });

        }
        //public IHttpActionResult GetAllPrivilages(int? groupId)
        //{
        //    GroupsTreePrivilageDTO privilages;
        //    privilages = groupId == null ? ctx.GroupsManager.GetGroupsTreePrivilage() : ctx.GroupsManager.GetGroupsTreePrivilage((int)groupId);
        //    var resourseManger = new ResourceManager(typeof(Resources.Resource));
        //    return Ok(new
        //    {
        //        GroupId = privilages.Group.Id,
        //        GroupName = privilages.Group.Name,

        //        Tree = privilages.PrivilageTree.Select(s => new
        //        {
        //            s.Id,
        //            ParentName = s.ParentName,
        //            IsChecked = s.Privilages.Any(m => m.IsChecked),
        //            Privilages = s.Privilages.Select(m => new
        //            {
        //                m.Id,
        //                m.IsChecked,
        //                m.IsRoute,
        //                m.OrderId,
        //                MenueName = m.MenueName,
        //                m.ParentId,
        //                m.ShowInMenue,
        //            }).ToList()
        //        })
        //    });

        //}
    }
}

