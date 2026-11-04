using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetTimeMap : ClassMapping<NodaTimeOffsetTime>
    {
        public NodaTimeOffsetTimeMap()
        {
            Table("NodaTimeOffsetTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.OffsetTime, m => m.Type<NodaTimeOffsetTimeUserType>());

            Property(p => p.NullableOffsetTime, m => m.Type<NodaTimeOffsetTimeUserType>());

            Property(p => p.NullableOffsetTimeWithNull, m => m.Type<NodaTimeOffsetTimeUserType>());
        }
    }
}
