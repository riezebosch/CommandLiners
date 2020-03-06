using System;
using System.Linq.Expressions;

namespace CommandLiners
{
    public static class PropertySelector
    {
        public static string Do(LambdaExpression map)
        {
            var member = map.Body as MemberExpression ?? throw new ArgumentException(nameof(map));
            return $"{Do(member.Expression)}{member.Member.Name}";
        }

        private static string Do(Expression expression) =>
            expression switch
            {
                MemberExpression member => $"{Do(member.Expression)}{member.Member.Name}:",
                ParameterExpression _ => string.Empty,
                _ => throw new ArgumentException(nameof(expression))
            };
    }
}