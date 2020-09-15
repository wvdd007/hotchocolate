using System.Threading.Tasks;
using HotChocolate.Execution;
using HotChocolate.Types.Descriptors;
using NetTopologySuite.Geometries;
using Snapshooter.Xunit;
using Xunit;

namespace HotChocolate.Types.Spatial.Tests
{
    public class GeoJsonLineStringTypeTests
    {
        private readonly LineString _geom = new LineString(
            new[] {new Coordinate(30, 10), new Coordinate(10, 30), new Coordinate(40, 40)});

        [Fact]
        public async Task LineString_Execution_Output()
        {
            ISchema schema = SchemaBuilder.New()
                .AddConvention<INamingConventions, MockNamingConvention>()
                .BindClrType<Coordinate, GeoJsonPositionType>()
                .AddType<GeoJsonLineStringType>()
                .AddQueryType(
                    d => d
                        .Name("Query")
                        .Field("test")
                        .Resolver(_geom))
                .Create();

            IRequestExecutor executor = schema.MakeExecutable();

            // act
            IExecutionResult result = await executor.ExecuteAsync(
                "{ test { type coordinates bbox crs }}");
            // assert
            result.MatchSnapshot();
        }

        [Fact]
        public async Task LineString_Execution_With_Fragments()
        {
            ISchema schema = SchemaBuilder.New()
                .AddConvention<INamingConventions, MockNamingConvention>()
                .AddSpatialTypes()
                .AddQueryType(
                    d => d
                        .Name("Query")
                        .Field("test")
                        .Type<GeoJsonLineStringType>()
                        .Resolver(_geom))
                .Create();
            IRequestExecutor executor = schema.MakeExecutable();
            // act
            IExecutionResult result = await executor.ExecuteAsync(
                "{ test { ... on LineString { type coordinates bbox crs }}}");
            // assert
            result.MatchSnapshot();
        }

        [Fact]
        public void LineString_Execution_Tests()
        {
            ISchema schema = SchemaBuilder.New()
                .AddConvention<INamingConventions, MockNamingConvention>()
                .BindClrType<Coordinate, GeoJsonPositionType>()
                .AddType<GeoJsonLineStringType>()
                .AddQueryType(
                    d => d
                        .Name("Query")
                        .Field("test")
                        .Resolver(_geom))
                .Create();

            schema.ToString().MatchSnapshot();
        }

        [Fact]
        public async Task LineString_Execution_With_CRS()
        {
            ISchema schema = SchemaBuilder.New()
                .AddConvention<INamingConventions, MockNamingConvention>()
                .BindClrType<Coordinate, GeoJsonPositionType>()
                .AddType<GeoJsonLineStringType>()
                .AddQueryType(
                    d => d
                        .Name("Query")
                        .Field("test")
                        .Resolver(_geom))
                .Create();

            IRequestExecutor executor = schema.MakeExecutable();
            IExecutionResult result = await executor.ExecuteAsync(
                "{ test { crs }}");

            // assert
            result.MatchSnapshot();
        }
    }
}
