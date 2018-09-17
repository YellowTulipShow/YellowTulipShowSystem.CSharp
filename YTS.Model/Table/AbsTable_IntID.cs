﻿using System;
using YTS.Model.Attribute;
using YTS.Model.Table.Attribute;

namespace YTS.Model.Table
{
    /// <summary>
    /// 表-基础-模型 记录ID
    /// </summary>
    [Serializable]
    public abstract class AbsTable_IntID : AbsTable_TimeAdd
    {
        public const int ERROR_DEFAULT_INT_VALUE = 0;

        /// <summary>
        /// 唯一ID标识Int类型
        /// </summary>
        [Explain(@"唯一ID标识Int类型")]
        [Column(IsPrimaryKey = true, IsIDentity = true, IsAutoGenerated = true, SortIndex = ushort.MinValue)]
        public int IID { get { return _IID; } set { _IID = value; } }
        private int _IID = ERROR_DEFAULT_INT_VALUE;
    }
}
