using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetDateTimeMap : ClassMapping<NodaTimeOffsetDateTime>
    {
        public NodaTimeOffsetDateTimeMap()
        {
            Table("NodaTimeOffsetDateTime");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.OffsetDateTime, m => m.Type<NodaTimeOffsetDateTimeUserType>());

            Property(p => p.NullableOffsetDateTime, m => m.Type<NodaTimeOffsetDateTimeUserType>());

            Property(p => p.NullableOffsetDateTimeWithNull, m => m.Type<NodaTimeOffsetDateTimeUserType>());
        }
    }
}
