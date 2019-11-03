using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeOffsetDateTimeMap : NodaTimeClassMapping<NodaTimeOffsetDateTime>
    {
        public NodaTimeOffsetDateTimeMap()
        {
            Table("NodaTimeOffsetDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            OffsetDateTimeProperty(p => p.OffsetDateTime);
            OffsetDateTimeProperty(p => p.NullableOffsetDateTime);
            OffsetDateTimeProperty(p => p.NullableOffsetDateTimeWithNull);
           }
    }
}