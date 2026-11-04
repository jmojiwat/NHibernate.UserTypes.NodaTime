using System;
using System.Linq.Expressions;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using static NHibernate.UserTypes.NodaTime.ExpressionExtensions;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeClassMapping<TEntity> : ClassMapping<TEntity> where TEntity : class
    {
        // AnnualDate
        public void AnnualDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeAnnualDateUserType>());

        public void AnnualDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeAnnualDateUserType>()) + mapping);

        public void AnnualDateProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) => 
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeAnnualDateUserType>()) + mapping);

        // DateInterval
        public void DateIntervalProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Start"),
                    cm => cm.Name($"{propertyName}_End"));
                m.Type<NodaTimeDateIntervalUserType>();
            });
        }

        public void DateIntervalProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Start"),
                    cm => cm.Name($"{propertyName}_End"));
                m.Type<NodaTimeDateIntervalUserType>();
            }) + mapping);
        }

        public void DateIntervalProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Start"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_End"));
                m.Type<NodaTimeDateIntervalUserType>();
            }) + mapping);
        }

        // Duration
        public void DurationProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeDurationUserType>());

        public void DurationProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeDurationUserType>()) + mapping);

        public void DurationProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeDurationUserType>()) + mapping);

        // Instant
        public void InstantProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeInstantUserType>());

        public void InstantProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeInstantUserType>()) + mapping);

        public void InstantProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeInstantUserType>()) + mapping);

        // Interval
        public void IntervalProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Start"),
                    cm => cm.Name($"{propertyName}_End"));
                m.Type<NodaTimeIntervalUserType>();
            });
        }

        public void IntervalProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Start"),
                    cm => cm.Name($"{propertyName}_End"));
                m.Type<NodaTimeIntervalUserType>();
            }) + mapping);
        }

        public void IntervalProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Start"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_End"));
                m.Type<NodaTimeIntervalUserType>();
            }) + mapping);
        }

        // LocalDate
        public void LocalDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeLocalDateUserType>());

        public void LocalDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeLocalDateUserType>()) + mapping);

        public void LocalDateProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeLocalDateUserType>()) + mapping);

        // LocalDateTime
        public void LocalDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeLocalDateTimeUserType>());

        public void LocalDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeLocalDateTimeUserType>()) + mapping);

        public void LocalDateTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeLocalDateTimeUserType>()) + mapping);

        // LocalTime
        public void LocalTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeLocalTimeUserType>());

        public void LocalTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeLocalTimeUserType>()) + mapping);

        public void LocalTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeLocalTimeUserType>()) + mapping);

        // OffsetDate
        public void OffsetDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeOffsetDateUserType>());

        public void OffsetDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeOffsetDateUserType>()) + mapping);

        public void OffsetDateProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeOffsetDateUserType>()) + mapping);

        // OffsetDateTime
        public void OffsetDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeOffsetDateTimeUserType>());

        public void OffsetDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeOffsetDateTimeUserType>()) + mapping);

        public void OffsetDateTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeOffsetDateTimeUserType>()) + mapping);

        // Offset
        public void OffsetProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeOffsetUserType>());

        public void OffsetProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeOffsetUserType>()) + mapping);

        public void OffsetProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeOffsetUserType>()) + mapping);

        // OffsetTime
        public void OffsetTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeOffsetTimeUserType>());

        public void OffsetTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeOffsetTimeUserType>()) + mapping);

        public void OffsetTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeOffsetTimeUserType>()) + mapping);

        // Period
        public void PeriodProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimePeriodUserType>());

        public void PeriodProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimePeriodUserType>()) + mapping);

        public void PeriodProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimePeriodUserType>()) + mapping);

        // YearMonth
        public void YearMonthProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeYearMonthUserType>());

        public void YearMonthProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeYearMonthUserType>()) + mapping);

        public void YearMonthProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeYearMonthUserType>()) + mapping);

        // ZonedDateTime
        public void ZonedDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeZonedDateTimeUserType>());

        public void ZonedDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeZonedDateTimeUserType>()) + mapping);

        public void ZonedDateTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeZonedDateTimeUserType>()) + mapping);

        // DateTimeZone
        public void DateTimeZoneProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) =>
            Property(property, m => m.Type<NodaTimeDateTimeZoneUserType>());

        public void DateTimeZoneProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeDateTimeZoneUserType>()) + mapping);

        public void DateTimeZoneProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) =>
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeDateTimeZoneUserType>()) + mapping);
    }
}
