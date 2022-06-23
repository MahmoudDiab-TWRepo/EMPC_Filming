using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

    public static class Extensions
    {
        public static dynamic ManageNumber(this HtmlHelper htmlHelper, decimal? number)
        {
            if (number == null)
                return 0;
            return number % 1 == 0 ? Convert.ToInt32(number) : Math.Round((decimal)number, 2);
        }
    
}