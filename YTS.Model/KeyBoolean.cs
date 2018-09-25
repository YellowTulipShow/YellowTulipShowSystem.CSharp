﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTS.Model
{
    /// <summary>
    /// '键':'值' 数据模型
    /// </summary>
    public class KeyBoolean : KeyBasic
    {
        public KeyBoolean() : base() { }

        /// <summary>
        /// Boolean 类型值:
        /// </summary>
        public bool Value { get { return _value; } set { _value = value; } }
        private bool _value = false;
    }
}
