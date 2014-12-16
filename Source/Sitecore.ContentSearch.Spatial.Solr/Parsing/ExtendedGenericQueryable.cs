using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Linq.Indexing;
using Sitecore.ContentSearch.Linq.Parsing;
using Sitecore.ContentSearch.Spatial.Solr.Common;
using Sitecore.ContentSearch.Spatial.Solr.Indexing;

namespace Sitecore.ContentSearch.Spatial.Solr.Parsing
{
    public class ExtendedGenericQueryable<TElement, TQuery> : GenericQueryable<TElement, TQuery>
    {
        public ExtendedGenericQueryable(Index<TElement, TQuery> index, QueryMapper<TQuery> queryMapper, IQueryOptimizer queryOptimizer, FieldNameTranslator fieldNameTranslator) : 
            base(index, queryMapper, queryOptimizer, fieldNameTranslator)
        {
        }

        protected ExtendedGenericQueryable(Index<TQuery> index, QueryMapper<TQuery> queryMapper, IQueryOptimizer queryOptimizer, Expression expression, Type itemType, FieldNameTranslator fieldNameTranslator) : 
            base(index, queryMapper, queryOptimizer, expression, itemType, fieldNameTranslator)
        {
        }

        public override IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var genericQueryable = new ExtendedGenericQueryable<TElement, TQuery>(this.Index, this.QueryMapper, this.QueryOptimizer, expression, this.ItemType, this.FieldNameTranslator);
            ((IHasTraceWriter)genericQueryable).TraceWriter = ((IHasTraceWriter)this).TraceWriter;
            return genericQueryable;
        }
        
        protected override TQuery GetQuery(Expression expression)
        {
            

            this.Trace(expression, "Expression");
            IndexQuery indexQuery = new ExtendedExpressionParser(typeof(TElement), this.ItemType, this.FieldNameTranslator).Parse(expression);
            this.Trace(indexQuery, "Raw query:");
            IndexQuery optimizedQuery = this.QueryOptimizer.Optimize(indexQuery);
            this.Trace(optimizedQuery, "Optimized query:");
            TQuery nativeQuery = this.QueryMapper.MapQuery(optimizedQuery);
            this.Trace(new GenericDumpable((object)nativeQuery), "Native query:");
            return nativeQuery;
        }
    }
}