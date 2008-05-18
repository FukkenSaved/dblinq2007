﻿#region MIT license
// 
// Copyright (c) 2007-2008 Jiri Moudry
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
#endregion

using System.Linq;
using System.Reflection;
using DbLinq.Linq.Data.Sugar.Expressions;
using DbLinq.Util;

namespace DbLinq.Linq.Data.Sugar
{
    partial class QueryBuilder
    {
        /// <summary>
        /// Returns a registered column, or null if not found
        /// This method requires the table to be already registered
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="builderContext"></param>
        /// <returns></returns>
        public QueryColumnExpression GetRegisteredColumn(QueryTableExpression table, string name,
            BuilderContext builderContext)
        {
            return
                (from queryColumn in builderContext.ExpressionQuery.Columns
                 where queryColumn.Table == table && queryColumn.Name == name
                 select queryColumn).SingleOrDefault();
        }

        /// <summary>
        /// Registers a column
        /// This method requires the table to be already registered
        /// </summary>
        /// <param name="table"></param>
        /// <param name="memberInfo"></param>
        /// <param name="name"></param>
        /// <param name="builderContext"></param>
        /// <returns></returns>
        public QueryColumnExpression RegisterColumn(QueryTableExpression table,
                                                    MemberInfo memberInfo, string name,
                                                    BuilderContext builderContext)
        {
            if (memberInfo == null)
                return null;
            var queryColumn = GetRegisteredColumn(table, name, builderContext);
            if (queryColumn == null)
            {
                queryColumn = new QueryColumnExpression(table, name, memberInfo.GetMemberType());
                builderContext.ExpressionQuery.Columns.Add(queryColumn);
            }
            if (builderContext.RequestColumns)
                queryColumn.Request = true;
            return queryColumn;
        }

        /// <summary>
        /// Registers a column with only a table and a MemberInfo (this is the preferred method overload)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="memberInfo"></param>
        /// <param name="builderContext"></param>
        /// <returns></returns>
        public QueryColumnExpression RegisterColumn(QueryTableExpression table, MemberInfo memberInfo,
                                    BuilderContext builderContext)
        {
            var dataMember = builderContext.QueryContext.DataContext.Mapping.GetTable(table.Type).RowType
                                            .GetDataMember(memberInfo);
            if (dataMember == null)
                return null;
            return RegisterColumn(table, memberInfo, dataMember.MappedName, builderContext);
        }
    }
}
