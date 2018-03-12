﻿using System;
using System.Reflection;
using System.Collections.Generic;
using CSharp.LibrayFunction;

namespace CSharp.LibrayDataBase
{
    /// <summary>
    /// 解析-模型属性值为表列
    /// </summary>
    public static class Analysis
    {
        public static ColumnItemModel[] PropertyColumns<M>() where M: AbsModel_Null {
            Type modelT = typeof(M);
            if (!modelT.IsDefined(typeof(TableAttribute), false)) {
                return new ColumnItemModel[] { };
            }
            List<ColumnItemModel> colms = new List<ColumnItemModel>();
            PropertyInfo[] protertys = modelT.GetProperties();
            foreach (PropertyInfo pro in protertys) {
                ColumnAttribute columnAttr = ReflexHelper.FindAttributeOnly<ColumnAttribute>(pro);
                if (CheckData.IsObjectNull(columnAttr))
                    continue;
                colms.Add(new ColumnItemModel() {
                    Property = pro,
                    Attribute = columnAttr,
                    Explain = ExplainAttribute.Extract(pro),
                });
            }
            colms.Sort(ColumnItemModel.ColumnInfoSort);
            return colms.ToArray();
        }
    }

    /// <summary>
    /// 列信息模型
    /// </summary>
    public class ColumnItemModel : AbsBasicsDataModel
    {
        /// <summary>
        /// 解释翻译信息
        /// </summary>
        public ExplainAttribute Explain { get { return _explain; } set { _explain = value; } }
        private ExplainAttribute _explain = null;

        /// <summary>
        /// 数据列特性
        /// </summary>
        public ColumnAttribute Attribute { get { return _attribute; } set { _attribute = value; } }
        private ColumnAttribute _attribute = null;

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo Property { get { return _property; } set { _property = value; } }
        private PropertyInfo _property = null;

        /// <summary>
        /// 排序比较 实现委托: Comparison泛型
        /// </summary>
        public static int ColumnInfoSort(ColumnItemModel x, ColumnItemModel y) {
            // 主键
            if (x.Attribute.IsPrimaryKey != y.Attribute.IsPrimaryKey) {
                return y.Attribute.IsPrimaryKey ? 1 : -1;
            }
            // 标识列
            if (x.Attribute.IsIDentity != y.Attribute.IsIDentity) {
                return y.Attribute.IsIDentity ? 1 : -1;
            }
            // 自动生成的值
            if (x.Attribute.IsDbGenerated != y.Attribute.IsDbGenerated) {
                return y.Attribute.IsDbGenerated ? 1 : -1;
            }
            // 允许为空的值
            if (x.Attribute.IsCanBeNull != y.Attribute.IsCanBeNull) {
                return y.Attribute.IsCanBeNull ? 1 : -1;
            }
            int indexResult = Sort.Int(x.Attribute.SortIndex, y.Attribute.SortIndex);
            return indexResult == 0 ? Sort.String(x.Property.Name, y.Property.Name) : indexResult;
        }
    }
}
