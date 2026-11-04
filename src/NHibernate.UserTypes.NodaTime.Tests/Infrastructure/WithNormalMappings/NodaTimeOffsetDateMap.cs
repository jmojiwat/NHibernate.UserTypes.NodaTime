using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Tests.Infrastructure.WithNormalMappings
{
    public class NodaTimeOffsetDateMap : ClassMapping<NodaTimeOffsetDate>
    {
        public NodaTimeOffsetDateMap()
        {
            Table("NodaTimeOffsetDate");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.OffsetDate, m => m.Type<NodaTimeOffsetDateUserType>());

            Property(p => p.NullableOffsetDate, m => m.Type<NodaTimeOffsetDateUserType>());

            Property(p => p.NullableOffsetDateWithNull, m => m.Type<NodaTimeOffsetDateUserType>());
        }
    }
}
