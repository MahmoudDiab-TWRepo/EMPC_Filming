using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class ServiceImagesManager : Reposatory<ServiceImages>
    {
        public ServiceImagesManager(EmcNewsContext ctx) : base(ctx)
        {

        }
    }
}