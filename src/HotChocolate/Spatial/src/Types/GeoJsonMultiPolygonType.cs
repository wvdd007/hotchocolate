﻿using NetTopologySuite.Geometries;
using static HotChocolate.Types.Spatial.Properties.Resources;

namespace HotChocolate.Types.Spatial
{
    public sealed class GeoJsonMultiPolygonType : ObjectType<MultiPolygon>
    {
        protected override void Configure(IObjectTypeDescriptor<MultiPolygon> descriptor)
        {
            descriptor.GeoJsonName(nameof(GeoJsonMultiPolygonType));

            descriptor.BindFieldsExplicitly();

            descriptor.Implements<GeoJsonInterface>();

            descriptor
                .Field(x => x.Coordinates)
                .Description(GeoJson_Field_Coordinates_Description_MultiPolygon);

            descriptor
                .Field<GeoJsonResolvers>(x => x.GetType(default!))
                .Description(GeoJson_Field_Type_Description);

            descriptor
                .Field<GeoJsonResolvers>(x => x.GetBbox(default!))
                .Description(GeoJson_Field_Bbox_Description);

            descriptor
                .Field<GeoJsonResolvers>(x => x.GetCrs(default!))
                .Description(GeoJson_Field_Crs_Description);
        }
    }
}