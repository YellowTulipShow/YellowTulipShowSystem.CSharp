﻿using System;
using System.Web;

namespace YTS.Web.UI.Page
{
    public partial class index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 方法执行的首页
            //HttpContext.Current.Response.Redirect(linkurl("Main_Arena"));

            string url = @"/TS-css3/FrostedGlass.html";
            HttpContext.Current.Response.Redirect(url);
        }
    }

}
