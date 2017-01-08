﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
namespace SqlSugar
{
    public class MemberExpressionResolve : BaseResolve
    {
        public MemberExpressionResolve(ExpressionParameter parameter) : base(parameter)
        {
            var expression = base.Expression as MemberExpression;
            var isLeft = parameter.IsLeft;
            var isSingle = parameter.Context.IsSingle;
            string fieldName = string.Empty;
            fieldName = isSingle ? expression.Member.Name : expression.Member.ToString();
            if (parameter.BaseParameter.BinaryExpressionInfoList != null)
                parameter.BaseParameter.BinaryExpressionInfoList.Add(new KeyValuePair<string, BinaryExpressionInfo>(ExpressionConst.BinaryExpressionInfoListKey, new BinaryExpressionInfo()
                {
                    IsLeft = Convert.ToBoolean(isLeft),
                    Value = fieldName,
                    ExpressionType = expression.GetType()
                }));
            if (isLeft == null && base.Context.SqlWhere == null)
            {
                base.Context.SqlWhere = new StringBuilder();
                base.Context.SqlWhere.Append(fieldName);
            }
        }
    }
}
