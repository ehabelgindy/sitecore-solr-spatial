using System.Linq;
using Sitecore.ContentSearch.Abstractions;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Linq.Common;
using Sitecore.ContentSearch.Pipelines.QueryGlobalFilters;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch.Security;
using Sitecore.ContentSearch.SolrProvider;
using Sitecore.ContentSearch.Spatial.Solr.Indexing;
using Sitecore.ContentSearch.Utilities;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace Sitecore.ContentSearch.Spatial.Solr.Provider
{
    public class SolrSearchWithSpatialContext : SolrSearchContext, IProviderSearchContext
    {

        private readonly SolrSearchIndex index;
        private readonly SearchSecurityOptions securityOptions;
        private readonly IContentSearchConfigurationSettings contentSearchSettings;
        private ISettings settings;


        public SolrSearchWithSpatialContext(SolrSearchIndex index, SearchSecurityOptions options = SearchSecurityOptions.EnableSecurityCheck)
            : base(index, options)
        {
            Assert.ArgumentNotNull((object)index, "index");
            Assert.ArgumentNotNull((object)options, "options");
            this.index = index;
            this.contentSearchSettings = this.index.Locator.GetInstance<IContentSearchConfigurationSettings>();
            this.settings = this.index.Locator.GetInstance<ISettings>();
            this.securityOptions = options;
        }

        public new IQueryable<TItem> GetQueryable<TItem>()
        {
            return this.GetQueryable<TItem>(new IExecutionContext[0]);
        }

        public new IQueryable<TItem> GetQueryable<TItem>(IExecutionContext executionContext)
        {
            return this.GetQueryable<TItem>(new IExecutionContext[1]
              {
                executionContext
              });
        }

        public new IQueryable<TItem> GetQueryable<TItem>(params IExecutionContext[] executionContexts)
        {
            var linqToSolrIndex = new LinqToSolrIndexWithSpatial<TItem>(this, executionContexts);
            if (this.contentSearchSettings.EnableSearchDebug())
                ((IHasTraceWriter)linqToSolrIndex).TraceWriter = new LoggingTraceWriter(SearchLog.Log);

            var queryable = linqToSolrIndex.GetQueryable();
            if (typeof(TItem).IsAssignableFrom(typeof(SearchResultItem)))
            {
                var globalFiltersArgs = new QueryGlobalFiltersArgs(linqToSolrIndex.GetQueryable(), typeof(TItem), executionContexts.ToList());
                this.Index.Locator.GetInstance<ICorePipeline>().Run("contentSearch.getGlobalLinqFilters", globalFiltersArgs);
                queryable = (IQueryable<TItem>)globalFiltersArgs.Query;
            }
            return queryable;
        }

    }
}
