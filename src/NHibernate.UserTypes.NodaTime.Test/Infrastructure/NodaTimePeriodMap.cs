using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHibernate.UserTypes.NodaTime.Test.Infrastructure
{
    public class NodaTimePeriodMap : ClassMapping<NodaTimePeriod>
    {
        public NodaTimePeriodMap()
        {
            Table("NodaTimePeriod");

            Id(p => p.Id, m => m.Generator(Generators.HighLow));

            Property(p => p.Period, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("PeriodDays"),
                        cm => cm.Name("PeriodHours"),
                        cm => cm.Name("PeriodMilliseconds"),
                        cm => cm.Name("PeriodMinutes"),
                        cm => cm.Name("PeriodMonths"),
                        cm => cm.Name("PeriodNanoseconds"),
                        cm => cm.Name("PeriodSeconds"),
                        cm => cm.Name("PeriodTicks"),
                        cm => cm.Name("PeriodWeeks"),
                        cm => cm.Name("PeriodYears"));
                    m.Type<NodaTimePeriodUserType>();
                });

            Property(p => p.NullablePeriod, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullablePeriodDays"),
                        cm => cm.Name("NullablePeriodHours"),
                        cm => cm.Name("NullablePeriodMilliseconds"),
                        cm => cm.Name("NullablePeriodMinutes"),
                        cm => cm.Name("NullablePeriodMonths"),
                        cm => cm.Name("NullablePeriodNanoseconds"),
                        cm => cm.Name("NullablePeriodSeconds"),
                        cm => cm.Name("NullablePeriodTicks"),
                        cm => cm.Name("NullablePeriodWeeks"),
                        cm => cm.Name("NullablePeriodYears"));
                    m.Type<NodaTimePeriodUserType>();
                });

            Property(p => p.NullablePeriodWithNull, 
                m =>
                {
                    m.Columns(
                        cm => cm.Name("NullablePeriodDaysWithNull"),
                        cm => cm.Name("NullablePeriodHoursWithNull"),
                        cm => cm.Name("NullablePeriodMillisecondsWithNull"),
                        cm => cm.Name("NullablePeriodMinutesWithNull"),
                        cm => cm.Name("NullablePeriodMonthsWithNull"),
                        cm => cm.Name("NullablePeriodNanosecondsWithNull"),
                        cm => cm.Name("NullablePeriodSecondsWithNull"),
                        cm => cm.Name("NullablePeriodTicksWithNull"),
                        cm => cm.Name("NullablePeriodWeeksWithNull"),
                        cm => cm.Name("NullablePeriodYearsWithNull"));
                    m.Type<NodaTimePeriodUserType>();
                });
       }
    }
}