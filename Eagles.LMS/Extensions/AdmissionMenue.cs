using Eagles.LMS.BLL;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace Eagles.LMS.Extensions
{
    public static class AdmissionMenue
    {
        public static HtmlString BindMenue(this HtmlHelper htmlHelper, int userid)
        {
            return GetUserMenue(userid);
        }

        private static HtmlString GetUserMenue(int Userid)
        {

            var menueFromeSesstion = HttpContext.Current.Session["User_Menue"];
            string menue = "";
            menueFromeSesstion = null;
            if (true)
            {


                UnitOfWork ctx = new UnitOfWork();
                User user = ctx.UserManager.GetBy(Userid);
                if (user == null)
                    return null;

                var userPrivilages = ctx.PrivilageManager.GetUserTreePrivilages((int)user.GroupId);

                menue = "<ul class='metismenu list-unstyled' id='side-menu'>";
                if (userPrivilages != null && userPrivilages.PrivilageTreeToUser != null)
                {

                    foreach (var priv_Lev1 in userPrivilages.PrivilageTreeToUser)
                    {
                        var obj = priv_Lev1.ParentPrivilageRouting_Lev1;

                        if (priv_Lev1.ParentPrivilageRouting_Lev1.PrivilageRoute != null)
                        {

                            menue += "<li class='' ><a href='/" + obj.PrivilageRoute.Url + "' class='waves-effect'><i class='bx bx-layer'></i> <span class='' key='t-calendar'>" + obj.ParentPrivilage.MenueName + "</span> </a> </li>";

                        }
                        else
                        {
                            menue += "<li class='' >";
                            menue += "<a href='javascript: void(0);' class='has-arrow waves-effect'>";
                            menue += "<i class='bx bx-layer'>";
                            menue += "</i><span class=''>" + obj.ParentPrivilage.MenueName + "</span></a>";//resourceManager.GetString(obj.ParentPrivilage.MenueName) 
                            menue += " <ul class='sub-menu ' aria-expanded='false'>";

                            foreach (var priv_Lev2 in priv_Lev1.PartentPrivilgaeLevel2)
                            {
                                var obj2 = priv_Lev2.ParentPrivilageRouting_Lev2;
                                if (priv_Lev2.ParentPrivilageRouting_Lev2.PrivilageRoute != null)
                                {
                                    menue += "<li class=''><a href='/" + obj2.PrivilageRoute.Url + "'class=''><span class=''>" + obj2.ParentPrivilage.MenueName + "</span></a></li>  ";
                                }
                                else
                                {
                                    menue += "<li class=''>";
                                    menue += "<a href='javascript:;' class=''>";
                                    menue += "<i class=''>";
                                    menue += "</i><span class=''>" + obj2.ParentPrivilage.MenueName + "</span><i class=' la la-angle-right'></i></a>";//resourceManager.GetString(obj2.ParentPrivilage.MenueName)
                                    menue += " <ul class='' aria-expanded='true'>";
                                    if (priv_Lev2.PartentPrivilgaeLevel3 == null)
                                    {
                                        //menue += "</ul>";
                                        //menue += " </div>";
                                        //menue += " </li>";
                                        continue;
                                    }
                                    foreach (var priv_Lev3 in priv_Lev2.PartentPrivilgaeLevel3)
                                    {
                                        var obj3 = priv_Lev3;
                                        menue += "<li class=' ' > <a href='/" + obj3.PrivilageRoute.Url + "' class='kt-menu__link clickableLink'><i class='kt-menu__link-bullet kt-menu__link-bullet--dot'>   <span>  </span></i><span class='kt-menu__link-text'> " + obj3.Privilage.MenueName + "</span></a></li>  ";// resourceManager.GetString(obj3.Privilage.MenueName)

                                    }
                                    menue += "</ul>";
                                    menue += " </li>";


                                }
                            }
                            menue += "</ul>";
                            menue += " </li>";
                        }
                    }
                }
                menue += "</ul>";
                HttpContext.Current.Session.Add("User_Menue", menue);

                //var userPrivilages = ctx.PrivilageManager.GetUserTreePrivilages(user.GroupId);

                //menue = "<ul class='m-menu__nav  m-menu__nav--submenu-arrow '> ";
                //if (userPrivilages != null && userPrivilages.PrivilageTreeToUser != null)
                //{

                //    foreach (var priv_Lev1 in userPrivilages.PrivilageTreeToUser)
                //    {
                //        var obj = priv_Lev1.ParentPrivilageRouting_Lev1;

                //        if (priv_Lev1.ParentPrivilageRouting_Lev1.PrivilageRoute != null)
                //        {

                //            menue += "<li class='m-menu__item  m-menu__item ' aria-haspopup='true'> <a href='/" + obj.PrivilageRoute.Url + "' class='m-menu__link '><span class='m-menu__link-text'> " + obj.ParentPrivilage.MenueName + "</span></a></li>  ";//resourceManager.GetString(


                //        }
                //        else
                //        {
                //            menue += "<li class='m-menu__item m-menu__item--submenu m-menu__item--tabs m-menu__item--open-dropdown' m-menu-submenu-toggle='tab' aria-haspopup='true'>";
                //            menue += "<a href='javascript:;' class='m-menu__link m-menu__toggle'>";
                //            menue += "<i class='m-menu__link-icon fa fa-home'>";
                //            menue += "</i><span class='m-menu__link-text'>" + obj.ParentPrivilage.MenueName + "</span></a>";
                //            menue += "<div class='m-menu__submenu m-menu__submenu--classic m-menu__submenu--left m-menu__submenu--tabs'><span class='m-menu__arrow m-menu__arrow--adjust' style='left: 46.5px;'></span> <ul class='m-menu__subnav'>";

                //            foreach (var priv_Lev2 in priv_Lev1.PartentPrivilgaeLevel2)
                //            {
                //                var obj2 = priv_Lev2.ParentPrivilageRouting_Lev2;
                //                if (priv_Lev2.ParentPrivilageRouting_Lev2.PrivilageRoute != null)
                //                {
                //                    menue += "<li class='m-menu__item  m-menu__item ' aria-haspopup='true'> <a href='/" + obj2.PrivilageRoute.Url + "' class='m-menu__link '><span class='m-menu__link-text'> " + obj2.ParentPrivilage.MenueName + "</span></a></li>  ";//resourceManager.GetString(
                //                }
                //                else
                //                {
                //                    menue += "<li class='kt-menu__item kt-menu__item--submenu' aria-haspopup='true' data-ktmenu-submenu-toggle='hover'>";
                //                    menue += "<a href='javascript:;' class='kt-menu__link kt-menu__toggle'>";
                //                    menue += "<i class='kt-menu__link-icon flaticon-interface-8'>";
                //                    menue += "</i><span class='kt-menu__link-text'>" + resourceManager.GetString(obj2.ParentPrivilage.MenueName) + "</span><i class='kt-menu__ver-arrow la la-angle-right'></i></a>";
                //                    menue += "<div class='kt-menu__submenu '><span class='kt-menu__arrow'></span> <ul class='kt-menu__subnav'>";
                //                    if (priv_Lev2.PartentPrivilgaeLevel3 == null)
                //                    {
                //                        //menue += "</ul>";
                //                        //menue += " </div>";
                //                        //menue += " </li>";
                //                        continue;
                //                    }
                //                    foreach (var priv_Lev3 in priv_Lev2.PartentPrivilgaeLevel3)
                //                    {
                //                        var obj3 = priv_Lev3;
                //                        menue += "<li class='kt-menu__item ' aria-haspopup='true'> <a href='/" + obj3.PrivilageRoute.Url + "' class='kt-menu__link '><i class='kt-menu__link-bullet kt-menu__link-bullet--dot'>   <span>  </span></i><span class='kt-menu__link-text'> " + resourceManager.GetString(obj3.Privilage.MenueName) + "</span></a></li>  ";

                //                    }
                //                    menue += "</ul>";
                //                    menue += " </div>";
                //                    menue += " </li>";


                //                }
                //            }
                //            menue += "</ul>";
                //            menue += " </div>";
                //            menue += " </li>";
                //        }
                //    }
                //}
                //menue += "</ul>";
                //HttpContext.Current.Session.Add("User_Menue", menue);

            }
            else
            {
                menue = HttpContext.Current.Session["User_Menue"].ToString();
            }
            return new HtmlString(menue);

        }
    }
}