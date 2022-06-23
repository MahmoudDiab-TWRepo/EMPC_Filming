using Eagles.LMS.DTO;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class PrivilageManager : Reposatory<Privilage>
    {
        private readonly UnitOfWork ctx = new UnitOfWork();
        public ParentPrivilageRouting GetParentPrivilageRouting(Privilage privilage)
        {
            return new ParentPrivilageRouting
            {
                PrivilageRoute = ctx.PrivilageRouteManager.GetPrivilageRoute(privilage.Id),
                ParentPrivilage = privilage
            };

        }
        public IEnumerable<Privilage> GetDirectParentLastRoot(IEnumerable<Privilage> lastRoot)
        {
            return ctx.PrivilageManager.GetAll().Where(s => lastRoot.Select(m => m.ParentId).Contains(s.Id)).OrderBy(s => s.OrderId).ToList();

        }
        public IEnumerable<Privilage> GetDirectParentLastRoot(int id)
        {
            return ctx.PrivilageManager.GetAll().Where(s => s.ParentId == id).OrderBy(s => s.OrderId);
        }
        public UsersTreePrivilage GetUserTreePrivilages(int groupId)
        {
            // return type
            List<PrivilageTreeToUser> result = new List<PrivilageTreeToUser>();

            // get parent All Privilage like all
            var parnet = ctx.PrivilageManager.GetAll().FirstOrDefault(S => S.ParentId == 0);
            // get level1 under parent like admin-area + .....
            var ParentLevel1 = ctx.PrivilageManager.GetAll().Where(S => S.ParentId == parnet.Id).OrderBy(s => s.OrderId);
            if (parnet == null || ParentLevel1 == null || !ParentLevel1.Any())
                return null;
            // loop in level1 and get all parent id == level1 id like groups-users+.....
            List<PartentPrivilgaeLevel2> privilageLev2;

            foreach (var Priv_Lev1 in ParentLevel1)
            {
                // diirect privilage  and get  routing 
                if (Priv_Lev1.ShowInMenue && Priv_Lev1.IsRoute)
                {
                    if (!ctx.GroupPriviageManager.CheckPrivInGroup(Priv_Lev1.Id, groupId))
                        continue;
                    // check user have permisstion 
                    result.Add(new PrivilageTreeToUser
                    {
                        ParentPrivilageRouting_Lev1 = GetParentPrivilageRouting(Priv_Lev1)

                    });
                }
                else
                {
                    privilageLev2 = new List<PartentPrivilgaeLevel2>();
                    foreach (var Priv_Lev2 in GetDirectParentLastRoot(Priv_Lev1.Id))
                    {
                        // diirect privilage  and get  routing 

                        if (Priv_Lev2.IsRoute && Priv_Lev2.ShowInMenue)
                        {
                            // check user have permisstion 
                            if (!ctx.GroupPriviageManager.CheckPrivInGroup(Priv_Lev2.Id, groupId))
                                continue;

                            privilageLev2.Add(new PartentPrivilgaeLevel2
                            {
                                ParentPrivilageRouting_Lev2 = GetParentPrivilageRouting(Priv_Lev2)


                            });
                        }
                        else
                        {
                            IEnumerable<PartentPrivilgaeLevel3> userEndPrivilage = GetAllPrivilageInGroup(groupId, Priv_Lev2.Id);
                            if (userEndPrivilage == null || !userEndPrivilage.Any()) continue;

                            //add level2 and child in level 2 like edit group in groups
                            privilageLev2.Add(new PartentPrivilgaeLevel2
                            {
                                ParentPrivilageRouting_Lev2 = new ParentPrivilageRouting { ParentPrivilage = Priv_Lev2 },
                                PartentPrivilgaeLevel3 = userEndPrivilage
                            });
                        }
                    }
                    if (privilageLev2 == null || !privilageLev2.Any()) continue;
                    result.Add(new PrivilageTreeToUser
                    {
                        ParentPrivilageRouting_Lev1 = new ParentPrivilageRouting { ParentPrivilage = Priv_Lev1 },
                        PartentPrivilgaeLevel2 = privilageLev2
                    });
                }


            }
            return new UsersTreePrivilage
            {
                PrivilageTreeToUser = result
            };

        }
        private List<Privilage> GetAllPrivilageInGroup(int groupId)
        {
            var allGroupPriv = ctx.GroupPriviageManager.GetAll().Where(s => s.GroupId == groupId).ToList();
            var privs = allGroupPriv.Select(k => k.PrivilageId).ToList();
            return ctx.PrivilageManager.GetAll().Where(s => privs.Contains(s.Id)).ToList();
        }
        // بتجيب اخر نود خالص موجودع في الصلاحيه
        public IEnumerable<PartentPrivilgaeLevel3> GetAllPrivilageInGroup(int groupId, int PrivId)
        {
            var PrivInGroup = GetAllPrivilageInGroup(groupId);
            if (PrivInGroup == null || PrivInGroup.Count == 0)
                return null;
            return PrivInGroup.Where(s => s.ParentId == PrivId && s.ShowInMenue && s.IsRoute).OrderBy(s => s.OrderId).Select(s => new PartentPrivilgaeLevel3
            {

                Privilage = s,
                PrivilageRoute = ctx.PrivilageRouteManager.GetAll().FirstOrDefault(k => k.PrivilageId == s.Id)
            });

        }
        public PrivilageManager(LMS.Data.EmcNewsContext ctx) : base(ctx)
        {

        }
    }
}