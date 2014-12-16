using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.FieldReaders;
using Sitecore.Data.Fields;

namespace Sitecore.ContentSearch.Spatial.DataTypes.FieldReaders
{
    public class SpatialPointFieldReader :FieldReader
    {
        public override object GetFieldValue(IIndexableDataField indexableField)
        {
            if (!(indexableField is SitecoreItemDataField))
                return indexableField.Value;
            var field = (Field)(indexableField as SitecoreItemDataField);
            if (!string.IsNullOrEmpty(field.Value))
                return new SpatialPoint(field.Value);
            
            return null;
        }
    }
}