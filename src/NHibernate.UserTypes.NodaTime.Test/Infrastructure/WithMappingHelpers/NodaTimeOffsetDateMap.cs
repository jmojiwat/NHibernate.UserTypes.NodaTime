using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeOffsetDateMap : NodaTimeClassMapping<NodaTimeOffsetDate>
    {
        public NodaTimeOffsetDateMap()
        {
            Table("NodaTimeOffsetDate");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            OffsetDateProperty(p => p.OffsetDate);
            OffsetDateProperty(p => p.NullableOffsetDate);
            OffsetDateProperty(p => p.NullableOffsetDateWithNull);
       }
    }
}