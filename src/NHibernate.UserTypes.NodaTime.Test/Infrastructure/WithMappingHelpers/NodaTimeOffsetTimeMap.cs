using NHibernate.Mapping.ByCode;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure.WithMappingHelpers
{
    public class NodaTimeOffsetTimeMap : NodaTimeClassMapping<NodaTimeOffsetTime>
    {
        public NodaTimeOffsetTimeMap()
        {
            Table("NodaTimeOffsetTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            OffsetTimeProperty(p => p.OffsetTime);
            OffsetTimeProperty(p => p.NullableOffsetTime);
            OffsetTimeProperty(p => p.NullableOffsetTimeWithNull);
       }
    }
}