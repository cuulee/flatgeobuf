using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json.Linq;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using FlatGeobuf;

namespace FlatGeobuf.Tests
{
    [TestClass]
    public class GeometryRoundtripTests
    {
        string MakeFeatureCollection(string wkt) {
            return MakeFeatureCollection(new string[] { wkt });
        }

        string MakeFeatureCollection(string[] wkts) {
            var fc = new FeatureCollection();
            foreach (var wkt in wkts)
                fc.Add(MakeFeature(wkt));
            var writer = new GeoJsonWriter();
            var geojson = writer.Write(fc);
            return geojson;
        }

        NetTopologySuite.Features.Feature MakeFeature(string wkt) {
            var reader = new WKTReader();
            var geometry = reader.Read(wkt);
            var feature = new NetTopologySuite.Features.Feature(geometry, null);
            return feature;
        }

        [TestMethod]
        public void Point()
        {
            var expected = MakeFeatureCollection("POINT(1.2 -2.1)");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Points()
        {
            var expected = MakeFeatureCollection(new string[] { "POINT(1.2 -2.1)", "POINT(2.4 -4.8)" });
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiPoint()
        {
            var expected = MakeFeatureCollection("MULTIPOINT(10 40, 40 30, 20 20, 30 10)");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiPointAlternativeSyntax()
        {
            var expected = MakeFeatureCollection("MULTIPOINT((10 40), (40 30), (20 20), (30 10))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void LineString()
        {
            var expected = MakeFeatureCollection("LINESTRING(1.2 -2.1, 2.4 -4.8)");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiLineString()
        {
            var expected = MakeFeatureCollection("MULTILINESTRING((10 10, 20 20, 10 40), (40 40, 30 30, 40 20, 30 10), (50 50, 60 60, 50 90))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiLineStringSinglePart()
        {
            var expected = MakeFeatureCollection("MULTILINESTRING((1.2 -2.1, 2.4 -4.8))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void Polygon()
        {
            var expected = MakeFeatureCollection("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiPolygon()
        {
            var expected = MakeFeatureCollection("MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)), ((15 5, 40 10, 10 20, 5 10, 15 5)))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiPolygonWithHole()
        {
            var expected = MakeFeatureCollection("MULTIPOLYGON (((40 40, 20 45, 45 30, 40 40)), ((20 35, 10 30, 10 10, 30 5, 45 20, 20 35), (30 20, 20 15, 20 25, 30 20)))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiPolygonSinglePart()
        {
            var expected = MakeFeatureCollection("MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void MultiPolygonSinglePartWithHole()
        {
            var expected = MakeFeatureCollection("MULTIPOLYGON (((35 10, 45 45, 15 40, 10 20, 35 10), (20 30, 35 35, 30 20, 20 30)))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }

        [TestMethod]
        public void PolygonWithHole()
        {
            var expected = MakeFeatureCollection("POLYGON ((35 10, 45 45, 15 40, 10 20, 35 10), (20 30, 35 35, 30 20, 20 30))");
            var bytes = Api.FromGeoJson(expected);
            var result = Api.ToGeoJson(bytes);
            var equals = JToken.DeepEquals(expected, result);
            Assert.IsTrue(equals);
        }
    }
}