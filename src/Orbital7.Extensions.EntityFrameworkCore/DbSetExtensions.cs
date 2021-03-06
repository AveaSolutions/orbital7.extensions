﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> CreateContainsIdsQuery<T>(this DbSet<T> dbSet, IList ids, string whereAndClause = "",
            string queryIdColumnName = "Id", string tableNameOverride = null) where T : class
        {
            if (ids.Count > 0)
            {
                var values = new StringBuilder();
                values.AppendFormat("'{0}'", ids[0]);
                for (int i = 1; i < ids.Count; i++)
                    values.AppendFormat(", '{0}'", ids[i]);

                // TODO: Refactor this to use SQL parameterization; need to ensure that additional 'where' 
                // clauses can be passed in to this method.
                var sql = String.Format("SELECT * FROM {0} WHERE {1} IN ({2})",
                    tableNameOverride ?? typeof(T).Name.Pluralize(),
                    (whereAndClause + " " + queryIdColumnName).Trim(), values.ToString());
                return dbSet.FromSql(sql);
            }
            else
            {
                return Enumerable.Empty<T>().AsAsyncQueryable();
            }
        }

        public static async Task<List<T>> GatherAsync<T>(this DbSet<T> dbSet, IList ids, bool asNoTracking,
            string whereAndClause = "", string queryIdColumnName = "Id", string tableNameOverride = null) where T : class
        {
            return await CreateContainsIdsQuery(dbSet, ids, whereAndClause, queryIdColumnName,
                tableNameOverride).ToListAsync(asNoTracking);
        }
    }
}
