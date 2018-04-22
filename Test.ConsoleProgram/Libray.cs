﻿using System;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace Test.ConsoleProgram
{
    public class Libray
    {
        public Libray() { }

        /// <summary>
        /// 在这里面手动设置要测试的实例
        /// </summary>
        public CaseModel[] InitCaseSource() {
            return new CaseModel[] {
                // new Case.SonTests.*****(),

                //new Case.Learn.Learn_Dictionary(),
                //new Case.Learn.Learn_Object(),
                //new Case.Learn.Learn_RegularExpression(),
                //new Case.Learn.Learn_XML(),

                //new Case.SonTests.Test_CaseModel_Tool_Value(),
                //new Case.SonTests.Test_AbsBasicsDataModel(),
                //new Case.SonTests.Test_Attribute(),
                //new Case.SonTests.Test_CheckData(),
                new Case.SonTests.Test_CommonData_RandomData(),
                //new Case.SonTests.Test_ConvertTool(),
                //new Case.SonTests.Test_DateTime(),
                //new Case.SonTests.Test_Enum(),
                //new Case.SonTests.Test_NewtonsoftJson(),
                //new Case.SonTests.Test_Random(),
                //new Case.SonTests.Test_Reflex(),
                //new Case.SonTests.Test_Lambda(),
                //new Case.SonTests.Test_WhereModel(),
                //new Case.SonTests.Test_BLLDALSQLServer(),
                //new Case.SonTests.Test_addRecord(),
                //new Case.SonTests.Test_BLLDALXML(),
            };
        }
    }
}
