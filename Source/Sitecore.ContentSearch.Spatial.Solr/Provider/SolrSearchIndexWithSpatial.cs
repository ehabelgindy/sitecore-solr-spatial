using Sitecore.ContentSearch.Maintenance;
using Sitecore.ContentSearch.SolrProvider;

namespace Sitecore.ContentSearch.Spatial.Solr.Provider
{
    public class SolrSearchIndexWithSpatial : SolrSearchIndex
    {
        public SolrSearchIndexWithSpatial(string name, string core, IIndexPropertyStore propertyStore, string @group) : base(name, core, propertyStore, @group)
        {
        }

        public SolrSearchIndexWithSpatial(string name, string core, IIndexPropertyStore propertyStore) : base(name, core, propertyStore)
        {
        }

        public override IProviderSearchContext CreateSearchContext(Security.SearchSecurityOptions options = Security.SearchSecurityOptions.EnableSecurityCheck)
        {
            return new SolrSearchWithSpatialContext(this,options);
        }
    }
}
