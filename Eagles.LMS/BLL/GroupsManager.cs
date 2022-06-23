using Eagles.LMS.DTO;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class GroupsManager : Reposatory<Group>
    {

        public GroupsManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }
        public GroupsTreePrivilageDTO GetGroupsTreePrivilage(int groupId)
        {

            var group = GetBy(groupId);
            // all child
            var lastRoot = GetLastPrivilageRoot(group).ToList();
            // all parent of child
            var parentLastRoot = GetDirectParentLastRoot(lastRoot).ToList();
            // empty list to append 
            List<PrivilageTree> PrivilageTree = new List<PrivilageTree>();// new PrivilageTree();

            foreach (var parentRoot in parentLastRoot)
            {
                PrivilageTree.Add(new PrivilageTree
                {
                    Id = parentRoot.Id,
                    ParentName = parentRoot.MenueName,
                    Privilages = lastRoot.Where(s => s.ParentId == parentRoot.Id).ToList(),

                });
            }

            return new GroupsTreePrivilageDTO
            {
                Group = group,
                PrivilageTree = PrivilageTree
            };

        }
        public IEnumerable<Privilage> GetLastPrivilageRoot(Group group)
        {
            var allPriv = GetLastPrivilageRoot();
            foreach (var priv in allPriv)
            {
                if (CheckPrivInGroup(priv.Id, group.Id))
                    priv.IsChecked = true;
            }
            return allPriv;
        }
        public bool CheckPrivInGroup(int privId, int groupId)
        {
            return ctx.GroupPriviages.FirstOrDefault(s => s.GroupId == groupId && s.PrivilageId == privId) != null;
        }
        public GroupsTreePrivilageDTO GetGroupsTreePrivilage()
        {

            // all child
            var lastRoot = GetLastPrivilageRoot().ToList();
            // all parent of child
            var parentLastRoot = GetDirectParentLastRoot(lastRoot).ToList();
            // empty list to append 
            List<PrivilageTree> PrivilageTree = new List<PrivilageTree>();// new PrivilageTree();

            foreach (var parentRoot in parentLastRoot)
            {
                PrivilageTree.Add(new PrivilageTree
                {
                    Id = parentRoot.Id,
                    ParentName = parentRoot.MenueName,
                    Privilages = lastRoot.Where(s => s.ParentId == parentRoot.Id).ToList(),

                });
            }
            return new GroupsTreePrivilageDTO
            {
                PrivilageTree = PrivilageTree,
                Group = new Group()

            };
        }

        private IEnumerable<Privilage> GetLastPrivilageRoot()
        {
            return ctx.Privilages.Where(s => s.IsRoute).OrderBy(s => s.OrderId).ToList();
        }
        public IEnumerable<Privilage> GetDirectParentLastRoot(IEnumerable<Privilage> lastRoot)
        {
            var roots = lastRoot.ToList().Select(m => m.ParentId).ToList();
            return ctx.Privilages.Where(s => roots.Contains(s.Id)).OrderBy(s => s.OrderId).ToList();

        }

        public void Edit(GroupForManagerDTO groupForEditDTO)
        {
            UnitOfWork ctx = new UnitOfWork();
            Edit(new Group { Id = (int)groupForEditDTO.GroupId, Name = groupForEditDTO.GroupName });
            var allPrivilageInGroups = ctx.GroupPriviageManager.GetAllPrivilageInGroup((int)groupForEditDTO.GroupId);
            var removedPrivilages = allPrivilageInGroups.Where(s => !groupForEditDTO.privilages.Contains(s.PrivilageId));
            var NewsPrivilages = groupForEditDTO.privilages.Where(m => !allPrivilageInGroups.Select(s => s.PrivilageId).ToList().Contains(m));
            // item removed from group
            if (removedPrivilages.Any())
            {
                ctx.GroupPriviageManager.Delete(removedPrivilages.ToList());
            }
            // addedd news item
            foreach (var priv in NewsPrivilages)
            {
                ctx.GroupPriviageManager.Add(new GroupPriviage { GroupId = (int)groupForEditDTO.GroupId, PrivilageId = priv });
            }


        }

        public Group GetGroup(string name)
        {
            return ctx.Groups.AsNoTracking().FirstOrDefault(s => s.Name.ToLower() == name.ToLower());
        }
        public Group Add(GroupForManagerDTO groupForCreateDTO)
        {
            var group = Add(new Group { Name = groupForCreateDTO.GroupName });
            foreach (var priv in groupForCreateDTO.privilages)
            {
                ctx.GroupPriviages.Add(new GroupPriviage
                {
                    GroupId = group.Id,
                    PrivilageId = priv
                });
            }
            SaveChanges();
            return group;
        }
        public ServerResponseDTO AddGroupPrivilage(GroupsTreePrivilageDTO groupsTreePrivilage)
        {
            // check the user is select privilages
            if (groupsTreePrivilage == null || groupsTreePrivilage.PrivilageTree == null || !groupsTreePrivilage.PrivilageTree.Any(s => s.Privilages.Any(k => k.IsChecked)))
                return new ServerResponseDTO { ServerResponseState = ServerResponseState.Error, Message = "Error_InvalidData" };
            // check teh group not exists before
            if (GetGroup(groupsTreePrivilage.Group.Name) != null)
                return new ServerResponseDTO { ServerResponseState = ServerResponseState.Error, Message = "Error_GroupAlreadyExists" };



            Group group = Add(groupsTreePrivilage.Group);
            if (group == null) return new ServerResponseDTO { ServerResponseState = ServerResponseState.Error, Message = "Error_InvalidData" };
            foreach (var privilages in groupsTreePrivilage.PrivilageTree)
            {
                foreach (var priv in privilages.Privilages)
                {
                    if (priv.IsChecked)
                    {
                        ctx.GroupPriviages.Add(new GroupPriviage
                        {
                            GroupId = group.Id,
                            PrivilageId = priv.Id
                        });
                        ctx.SaveChanges();
                    }

                }

            }
            return new ServerResponseDTO
            {
                Message = "AddedSuccessfully",
                ServerResponseState = ServerResponseState.Success
            };

        }

    }
}