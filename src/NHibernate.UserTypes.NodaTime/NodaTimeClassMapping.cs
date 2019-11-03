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
        public void AnnualDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Month"),
                    cm => cm.Name($"{propertyName}_Day"));
                m.Type<NodaTimeAnnualDateUserType>();
            });
        }

        public void AnnualDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Month"),
                    cm => cm.Name($"{propertyName}_Day"));
                m.Type<NodaTimeAnnualDateUserType>();
            }) + mapping);
        }

        public void AnnualDateProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Month"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Day"));
                m.Type<NodaTimeAnnualDateUserType>();
            }) + mapping);
        }

        // DateInterval
        public void DateIntervalProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Start"),
                    cm => cm.Name($"{propertyName}_End"),
                    cm => cm.Name($"{propertyName}_Calendar"));
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
                    cm => cm.Name($"{propertyName}_End"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeDateIntervalUserType>();
            }) + mapping);
        }

        public void DateIntervalProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Start"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_End"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Calendar"));
                m.Type<NodaTimeDateIntervalUserType>();
            }) + mapping);
        }

        // Duration
        public void DurationProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) => 
            Property(property, m => m.Type<NodaTimeDurationUserType>());

        public void DurationProperty<TProperty>(Expression<Func<TEntity, TProperty>> property, Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeDurationUserType>()) + mapping);

        public void DurationProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) => 
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeDurationUserType>()) + mapping);

        // Instant
        public void InstantProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) => 
            Property(property, m => m.Type<NodaTimeInstantUserType>());

        public void InstantProperty<TProperty>(Expression<Func<TEntity, TProperty>> property, Action<IPropertyMapper> mapping) =>
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
                    cm => cm.Name($"{propertyName}_Interval"),
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
                    cm => cm.Name($"{propertyName}_Interval"),
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
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Interval"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Start"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_End"));
                m.Type<NodaTimeIntervalUserType>();
            }) + mapping);
        }

        // LocalDate
        public void LocalDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Date"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeLocalDateUserType>();
            });
        }

        public void LocalDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Date"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeLocalDateUserType>();
            }) + mapping);
        }

        public void LocalDateProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Date"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Calendar"));
                m.Type<NodaTimeLocalDateUserType>();
            }) + mapping);
        }

        // LocalDateTime
        public void LocalDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_DateTime"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeLocalDateTimeUserType>();
            });
        }

        public void LocalDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);

            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_DateTime"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeLocalDateTimeUserType>();
            }) + mapping);
        }

        public void LocalDateTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_DateTime"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Calendar"));
                m.Type<NodaTimeLocalDateTimeUserType>();
            }) + mapping);
        }

        // LocalTime
        public void LocalTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) => 
            Property(property, m => m.Type<NodaTimeLocalTimeUserType>());

        public void LocalTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property, Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeLocalTimeUserType>()) + mapping);

        public void LocalTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) => 
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeLocalTimeUserType>()) + mapping);

        // OffsetDate
        public void OffsetDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Date"),
                    cm => cm.Name($"{propertyName}_Offset"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeOffsetDateUserType>();
            });
        }

        public void OffsetDateProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Date"),
                    cm => cm.Name($"{propertyName}_Offset"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeOffsetDateUserType>();
            }) + mapping);
        }

        public void OffsetDateProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Date"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Offset"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Calendar"));
                m.Type<NodaTimeOffsetDateUserType>();
            }) + mapping);
        }

        // OffsetDateTime
        public void OffsetDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_DateTime"),
                    cm => cm.Name($"{propertyName}_Offset"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeOffsetDateTimeUserType>();
            });
        }

        public void OffsetDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_DateTime"),
                    cm => cm.Name($"{propertyName}_Offset"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeOffsetDateTimeUserType>();
            }) + mapping);
        }

        public void OffsetDateTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_DateTime"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Offset"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Calendar"));
                m.Type<NodaTimeOffsetDateTimeUserType>();
            }) + mapping);
        }

        // Offset
        public void OffsetProperty<TProperty>(Expression<Func<TEntity, TProperty>> property) => 
            Property(property, m => m.Type<NodaTimeOffsetUserType>());

        public void OffsetProperty<TProperty>(Expression<Func<TEntity, TProperty>> property, Action<IPropertyMapper> mapping) =>
            Property(property, (m => m.Type<NodaTimeOffsetUserType>()) + mapping);

        public void OffsetProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping) => 
            Property(notVisiblePropertyOrFieldName, (m => m.Type<NodaTimeOffsetUserType>()) + mapping);

        // OffsetTime
        public void OffsetTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Time"),
                    cm => cm.Name($"{propertyName}_Offset"));
                m.Type<NodaTimeOffsetTimeUserType>();
            });
        }

        public void OffsetTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Time"),
                    cm => cm.Name($"{propertyName}_Offset"));
                m.Type<NodaTimeOffsetTimeUserType>();
            }) + mapping);
        }

        public void OffsetTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Time"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Offset"));
                m.Type<NodaTimeOffsetTimeUserType>();
            }) + mapping);
        }

        // Period
        public void PeriodProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Days"),
                    cm => cm.Name($"{propertyName}_Hours"),
                    cm => cm.Name($"{propertyName}_Milliseconds"),
                    cm => cm.Name($"{propertyName}_Minutes"),
                    cm => cm.Name($"{propertyName}_Months"),
                    cm => cm.Name($"{propertyName}_Nanoseconds"),
                    cm => cm.Name($"{propertyName}_Seconds"),
                    cm => cm.Name($"{propertyName}_Ticks"),
                    cm => cm.Name($"{propertyName}_Weeks"),
                    cm => cm.Name($"{propertyName}_Years"));
                m.Type<NodaTimePeriodUserType>();
            });
        }

        public void PeriodProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);
            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_Days"),
                    cm => cm.Name($"{propertyName}_Hours"),
                    cm => cm.Name($"{propertyName}_Milliseconds"),
                    cm => cm.Name($"{propertyName}_Minutes"),
                    cm => cm.Name($"{propertyName}_Months"),
                    cm => cm.Name($"{propertyName}_Nanoseconds"),
                    cm => cm.Name($"{propertyName}_Seconds"),
                    cm => cm.Name($"{propertyName}_Ticks"),
                    cm => cm.Name($"{propertyName}_Weeks"),
                    cm => cm.Name($"{propertyName}_Years"));
                m.Type<NodaTimePeriodUserType>();
            }) + mapping);
        }

        public void PeriodProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Days"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Hours"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Milliseconds"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Minutes"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Months"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Nanoseconds"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Seconds"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Ticks"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Weeks"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Years"));
                m.Type<NodaTimePeriodUserType>();
            }) + mapping);
        }

        // ZonedDateTime
        public void ZonedDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            var propertyName = GetPropertyName(property);
            Property(property, m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_DateTime"),
                    cm => cm.Name($"{propertyName}_Zone"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeZonedDateTimeUserType>();
            });
        }

        public void ZonedDateTimeProperty<TProperty>(Expression<Func<TEntity, TProperty>> property,
            Action<IPropertyMapper> mapping)
        {
            var propertyName = GetPropertyName(property);

            Property(property, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{propertyName}_DateTime"),
                    cm => cm.Name($"{propertyName}_Zone"),
                    cm => cm.Name($"{propertyName}_Calendar"));
                m.Type<NodaTimeZonedDateTimeUserType>();
            }) + mapping);
        }

        public void ZonedDateTimeProperty(string notVisiblePropertyOrFieldName, Action<IPropertyMapper> mapping)
        {
            Property(notVisiblePropertyOrFieldName, (m =>
            {
                m.Columns(
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_DateTime"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Zone"),
                    cm => cm.Name($"{notVisiblePropertyOrFieldName}_Calendar"));
                m.Type<NodaTimeZonedDateTimeUserType>();
            }) + mapping);
        }
    }
}