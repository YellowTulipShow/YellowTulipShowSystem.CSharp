﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using CSharp.ApplicationData;
using CSharp.LibrayDataBase;
using CSharp.LibrayDataBase.Utils;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram.Case.SonTests
{
    public class Test_DALSQLServer : CaseModel
    {
        public Test_DALSQLServer() {
            base.NameSign = @"测试 DALSQLServer类";
            base.ExeEvent = Method;
            base.SonCases = new CaseModel[] {
                //new Test_CAnalysisPropertyColumns(),
                //new Test_CDefaultModel(),
                new Test_CICreateSQL(),
                //new Test_CIAutoTable(),
                //new Test_CGetModelList(),
                //new Test_CInsertMethod_out_parmeter(),
                //new Test_CInsertSpeed(),
                //new Test_ColumnInfoSort(),
            };
        }

        public void Method() {
        }

        #region Son Test Case
        public class Test_CAnalysisPropertyColumns : CaseModel
        {
            public Test_CAnalysisPropertyColumns() {
                base.NameSign = @"实现解析属性的'行'特性";
                base.ExeEvent = Method;
            }

            public void Method() {
                //DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                //ColumnInfo[] dic = dal.AnalysisPropertyColumns();
                //foreach (ColumnInfo item in dic) {
                //    Print.WriteLine(String.Format("PropertyInfo Name: {0}", item.Property.Name));
                //    Print.WriteLine(String.Format("Column TypeId: {0}", item.Attribute.TypeId));
                //    Print.WriteLine(String.Format("Column IsIDentity: {0}", item.Attribute.IsIDentity));
                //    Print.WriteLine(String.Format("Column IsCanBeNull: {0}", item.Attribute.IsCanBeNull));
                //    Print.WriteLine(String.Format("Column IsDbGenerated: {0}", item.Attribute.IsDbGenerated));
                //    Print.WriteLine(String.Format("Column IsPrimaryKey: {0}", item.Attribute.IsPrimaryKey));
                //}
            }
        }
        public class Test_CDefaultModel : CaseModel
        {
            public Test_CDefaultModel() {
                base.NameSign = @"获得系统自动生成默认的实例";
                base.ExeEvent = Method;
            }

            public void Method() {
                DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                Print.WriteLine(dal.DefaultModel().ToString());
            }
        }
        public class Test_CICreateSQL : CaseModel
        {
            public Test_CICreateSQL() {
                base.NameSign = @"基础SQL语句";
                base.ExeEvent = Method;
            }

            public void Method() {
                //DALSQLServer<ModelArticles> bll = new DALSQLServer<ModelArticles>();
                //ModelArticles model = new ModelArticles() {
                //    Money = 287.88M,
                //    VipDiscountRate = 80,
                //    TimeAdd = DateTime.Now,
                //    Remark = @"测试数据,备注"
                //};

                DALSQLServer<ModelUser> bll = new DALSQLServer<ModelUser>();
                ModelUser model = new ModelUser() {
                    Email = @"1426689530@qq.com",
                    MobilePhone = @"18563920971",
                    NickName = @"YellowTulipShow",
                    Password = @"zrqytspass",
                    RealName = @"赵瑞青",
                    Remark = @"System Developer",
                    TelePhone = string.Empty,
                    TimeAdd = DateTime.Now,
                };

                Print.WriteLine("原始数据:");
                Print.WriteLine(model.ToString());
                Print.WriteLine(string.Empty);

                Print.Write("Insert 语句:");
                Print.WriteLine(bll.SQLInsert(model));
                Print.WriteLine(string.Empty);

                model.id = 8;
                Print.Write("Delete 语句:");
                Print.WriteLine(bll.SQLDelete(model));
                Print.WriteLine(string.Empty);

                Print.Write("Update 语句:");
                Print.WriteLine(bll.SQLUpdate(model));
            }
        }
        public class Test_CIAutoTable : CaseModel
        {
            public Test_CIAutoTable() {
                base.NameSign = @"IAutoTable 自动化表 SQL字符串";
                base.ExeEvent = Method;
            }

            public void Method() {
                DALSQLServer<ModelUser> dal = new DALSQLServer<ModelUser>();

                Print.WriteLine("创建 数据表 SQL:");
                string createsql = dal.SQLCreateTable();
                Print.WriteLine(createsql);
                Print.WriteLine(string.Empty);

                Print.WriteLine("清空 数据表 SQL:");
                Print.WriteLine(dal.SQLClearTable());
                Print.WriteLine(string.Empty);

                Print.WriteLine("'清除' 数据表 SQL:");
                Print.WriteLine(dal.SQLKillTable());
            }
        }
        public class Test_CGetModelList : CaseModel
        {
            public Test_CGetModelList() {
                base.NameSign = @"查询所有数据";
                base.ExeEvent = Method;
            }

            public void Method() {
                BLLUser dal = new BLLUser();
                DataTable dt = dal.GetList(0, string.Empty, null);
                foreach (ModelUser item in dal.GetModelList(dt)) {
                    Print.WriteLine(item.ToString());
                }
            }
        }
        public class Test_CInsertMethod_out_parmeter : CaseModel
        {
            public Test_CInsertMethod_out_parmeter() {
                base.NameSign = @"执行-基础的数据操作方法";
                base.ExeEvent = Method;
            }

            public void Method() {
                DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                ModelArticles model = new ModelArticles() {
                    id = 5555,
                    Money = 66.89M,
                    Remark = ConvertTool.CombinationContent(DateTime.Now.Year, DateTime.Now.Second, new Random().Next(DateTime.Now.Millisecond)),
                    TimeAdd = DateTime.Now,
                    VipDiscountRate = 95
                };
                int sign = 0;
                Print.WriteLine(dal.Insert(model, out sign));
                model.id = sign;
                Print.WriteLine(string.Format("最终的模型数据: {0}", model.ToString()));
                Print.WriteLine(string.Empty);

                model.id = model.id - 3;
                Print.Write("删掉刚添加的id小3的数据");
                Print.WriteLine(dal.Delete(model));
                model.id = 8;
                Print.Write("删掉id为8的数据");
                Print.WriteLine(dal.Delete(model));
                model.id = 10;
                Print.Write("删掉id为10的数据");
                Print.WriteLine(dal.Delete(model));

                model.id = 4;
                Print.Write("更新id为4的数据");
                Print.WriteLine(dal.Update(model));
            }
        }
        public class Test_CInsertSpeed : CaseModel
        {
            public Test_CInsertSpeed() {
                base.NameSign = @"执行添加大量测试数据-测试速度";
                base.ExeEvent = Method;
            }

            public void Method() {
                DALSQLServer<ModelArticles> dal = new DALSQLServer<ModelArticles>();
                double count = System.Math.Pow(10, 5);
                Print.WriteLine(@"执行循环次数: {0}", count);
                List<string> sqls = new List<string>();
                for (int i = 0; i < count; i++) {
                    sqls.Add(dal.SQLInsert(new ModelArticles() {
                        id = i,
                        Money = i * 2.5M,
                        Remark = string.Format("测试数据: {0} 条", i + 1),
                        TimeAdd = DateTime.Now
                    }));
                }
                Print.WriteLine(string.Format("生成完成sqls: 个数: {0}", sqls.Count));
                Print.WriteLine("结束了!");
            }
        }
        public class Test_ColumnInfoSort : CaseModel
        {
            public Test_ColumnInfoSort() {
                base.NameSign = @"列信息的排序问题";
                base.ExeEvent = Method;
            }

            public void Method() {
                DALSQLServer<ModelUser> dal = new DALSQLServer<ModelUser>();
                ColumnInfo[] ciAttr = dal.AnalysisPropertyColumns();
                for (int i = 0; i < ciAttr.Length; i++) {
                    Print.WriteLine("排序: {0} 列名: {1} 解释: {2} ", i, ciAttr[i].Property.Name, ciAttr[i].Explain.Text);
                }
            }
        }
        #endregion
    }
}
