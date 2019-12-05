﻿using System;
using System.Collections.Generic;
using System.Text;
using YTS.Tools;

namespace YTS.DAL
{
    /// <summary>
    /// 数据访问类:会员配置
    /// </summary>
    public partial class userconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.userconfig loadConfig(string configFilePath)
        {
            return (Model.userconfig)XML.Load(typeof(Model.userconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.userconfig saveConifg(Model.userconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                XML.Save(model, configFilePath);
            }
            return model;
        }

    }
}