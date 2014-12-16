Sitecore.ContentSearch.Spatial.Solr
==============================

This module is an extension for the Sitecore Solr Search Provider that allows users to run Solr spatial queries on Sitecore items.

The module contains the following components:

1- A custom data type in Sitecore (LatLon) that can store coordinates which can be added to any template. The LatLon data type provides a custom GUI based on Google map (via the Content Editor) to easily locate the places.

2- An extended Solr search index provider that must be used instead of the default one.

3- A set of IQueryable extension methods to implement the spatial search.

Supported Queries:

.WithinRadius() : This method returns locations within specific circle, results will be sorted based on the distance of the center of circle
.OrderByNearest : Will sort the results by distance nearest to the location specified in the WithinRadius function

Please feel free to contribute!
