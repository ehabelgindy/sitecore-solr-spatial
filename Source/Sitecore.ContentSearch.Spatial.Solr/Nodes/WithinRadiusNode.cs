using System.Collections.Generic;
using Sitecore.ContentSearch.Linq.Nodes;

namespace Sitecore.ContentSearch.Spatial.Solr.Nodes
{
    public class WithinRadiusNode : QueryNode
    {
        public WithinRadiusNode(QueryNode sourceNode, string field, double lat, double lon, int radius)
        {
            this.SourceNode = sourceNode;
            this.Field = field;
            this.Lat = lat;
            this.Lon = lon;
            this.Radius = radius;
        }

        public QueryNode SourceNode { get; protected set; }
        public string Field { get; protected set; }
        public double Lat { get; protected set; }
        public double Lon { get; protected set; }
        public int Radius { get; protected set; }

        public override QueryNodeType NodeType
        {
            get
            {
                return QueryNodeType.Custom;
            }
        }

        public override IEnumerable<QueryNode> SubNodes
        {
            get
            {
                yield return this.SourceNode;
            }
        }

    }
}