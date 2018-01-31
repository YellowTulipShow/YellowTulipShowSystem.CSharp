﻿using CSharp.LibrayFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.ConsoleProgram
{
    //[TestClass]
    internal class MainProram
    {
        //[TestMethod]
        internal static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8;
            do {
                ExecuteCaseText();
            } while (IsRepeatExecute());
        }
        private static void ExecuteCaseText() {
            ITestCase[] tcs = new TestCaseLibray().GetTestCase(true);

            if (CheckData.IsSizeEmpty(tcs)) {
                Console.WriteLine(@"(→_→) => 没有设置好的需要测试的实例子弹, 怎么打仗? 快跑吧~ running~ running~ running~ ");
            }

            foreach (ITestCase iTC in tcs) {
                Console.WriteLine(string.Empty);
                Console.WriteLine(@"===测试开始: {0} ===", iTC.TestNameSign());

                DelegateTimeTest dtt = new DelegateTimeTest();
                dtt.SetEventHandlers(iTC.TestMethod);
                dtt.ExecuteEventHandler();

                Console.WriteLine(@"============================ 运行时间: {0}", dtt.GetRunTimeTotalSeconds());
                Console.WriteLine(string.Empty);
            }
        }

        private static bool IsRepeatExecute() {
            Console.WriteLine(string.Empty);
            Console.WriteLine(@"请输入命令: Q(退出) R(重复执行) C(清空屏幕)");
            ConsoleKeyInfo keyinfo = Console.ReadKey(false);
            Console.WriteLine(string.Empty);
            switch (keyinfo.Key) {
                case ConsoleKey.Q:
                    return false;
                case ConsoleKey.R:
                    return true;
                case ConsoleKey.C:
                    Console.Clear();
                    return IsRepeatExecute();
                default:
                    return IsRepeatExecute();
            }
        }
    }
}
