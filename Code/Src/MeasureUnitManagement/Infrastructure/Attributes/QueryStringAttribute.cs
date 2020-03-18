using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeasureUnitManagement.Infrastructure.Attributes
{
    public class QueryStringAttribute : Attribute, IActionConstraint
    {
        public List<string> Keys { get; private set; }
        public QueryStringAttribute(params string[] keys)
        {
            this.Keys = keys.ToList();
        }

        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var query = context.RouteContext.HttpContext.Request.Query;
            return Keys.Where(key => !key.EndsWith("?")).All(key => query.ContainsKey(key));
        }
    }
}
