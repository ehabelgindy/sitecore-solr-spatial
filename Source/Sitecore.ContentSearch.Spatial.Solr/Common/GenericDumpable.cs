using System.IO;
using Sitecore.ContentSearch.Linq.Common;

namespace Sitecore.ContentSearch.Spatial.Solr.Common
{
    public class GenericDumpable : IDumpable
    {
        protected object Value { get; set; }

        public GenericDumpable(object value)
        {
            this.Value = value;
        }

        public virtual void WriteTo(TextWriter writer)
        {
            IDumpable dumpable = this.Value as IDumpable;
            if (dumpable != null)
                dumpable.WriteTo(writer);
            else
                writer.WriteLine(this.Value);
        }
    }
}