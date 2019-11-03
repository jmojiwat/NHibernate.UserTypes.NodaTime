using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeOffsetMap : NodaTimeClassMapping<NodaTimeOffset>
    {
        public NodaTimeOffsetMap()
        {
            Table("NodaTimeOffset");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            OffsetProperty(p => p.Offset);
            OffsetProperty(p => p.NullableOffset);
            OffsetProperty(p => p.NullableOffsetWithNull);
       }
    }
}