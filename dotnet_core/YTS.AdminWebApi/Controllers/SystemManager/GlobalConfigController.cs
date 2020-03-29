﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTS.Shop;
using YTS.Tools;
using YTS.WebApi;

namespace YTS.AdminWebApi.Controllers
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class GlobalConfigController : BaseApiController
    {
        protected YTSEntityContext db;
        public GlobalConfigController(YTSEntityContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 获取所有请求代码的详情
        /// </summary>
        [HttpGet]
        public object ResultCodes()
        {
            EnumInfo[] infos = EnumInfo.AnalysisList<ResultCode>();
            var list = infos
                .Select(info => new
                {
                    code = info.IntValue,
                    name = info.Name,
                    remark = info.Explain,
                })
                .ToList();
            return list;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult InitFirstManager()
        {
            if (db.Manager.Count() > 0)
            {
                return BadRequest("无须初始化!");
            }
            var manager = new Manager()
            {
                Account = "admin",
                Password = "123456",
                NickName = "admin",
                TrueName = "admin",
                AddManagerID = 0,
                AddTime = DateTime.Now,
                Phone = "",
                UserGroupID = null,
            };
            manager.Password = Manager.EncryptionPassword(manager.Password);
            db.Manager.Add(manager);
            db.SaveChanges();
            return Ok("初始化成功!");
        }
    }
}