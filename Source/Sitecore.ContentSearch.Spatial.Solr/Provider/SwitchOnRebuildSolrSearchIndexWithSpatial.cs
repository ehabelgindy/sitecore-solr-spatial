using Sitecore.ContentSearch.Maintenance;
using Sitecore.ContentSearch.SolrProvider;

namespace Sitecore.ContentSearch.Spatial.Solr.Provider
{
    public class SwitchOnRebuildSolrSearchIndexWithSpatial : SwitchOnRebuildSolrSearchIndex
	{
        public SwitchOnRebuildSolrSearchIndexWithSpatial(string name, string core, string rebuildcore, IIndexPropertyStore propertyStore) : base(name, core, rebuildcore, propertyStore)
        {
        }

        public override IProviderSearchContext CreateSearchContext(Security.SearchSecurityOptions options = Security.SearchSecurityOptions.EnableSecurityCheck)
        {
            return new SolrSearchWithSpatialContext(this,options);
        }
    }
}
