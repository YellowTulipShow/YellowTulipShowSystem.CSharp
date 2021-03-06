﻿using System;
using System.Data;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.IOAccess
{
    /// <summary>
    /// 接口: 数据库结果
    /// </summary>
    public interface IDataBaseResult<M> where M : AbsShineUpon
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="top">返回的记录数</param>
        /// <param name="sql_where">查询条件</param>
        /// <param name="sql_order">排序条件</param>
        /// <returns>结果数据表</returns>
        DataSet QueryRecords(int top, string sql_where, string sql_order);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageCount">定义: 每页记录数</param>
        /// <param name="pageIndex">定义: 浏览到第几页</param>
        /// <param name="recordCount">得到: 总记录数</param>
        /// <param name="sql_where">定义: 查询条件</param>
        /// <param name="sql_order">定义: 字段排序集合, true 为正序, false 倒序</param>
        /// <returns>结果数据表</returns>
        DataSet QueryRecords(int pageCount, int pageIndex, out int recordCount, string sql_where, string sql_order);

        /// <summary>
        /// 数据集 转为 模型列表
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>模型列表</returns>
        M[] DataSetToModels(DataSet ds);

        /// <summary>
        /// 数据行 转为 单个映射模型
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>单个映射模型</returns>
        M DataRowToModel(DataRow row);
    }
}
