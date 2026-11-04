# NHibernate.UserTypes.NodaTime

[![CI](https://github.com/jmojiwat/NHibernate.UserTypes.NodaTime/actions/workflows/ci.yml/badge.svg)](https://github.com/jmojiwat/NHibernate.UserTypes.NodaTime/actions/workflows/ci.yml)
[![NuGet Version](https://img.shields.io/nuget/v/NHibernate.UserTypes.NodaTime.svg)](https://www.nuget.org/packages/NHibernate.UserTypes.NodaTime)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE.txt)

A comprehensive collection of [NHibernate](https://nhibernate.info/) custom user types (`IUserType`) and mapping helpers for [NodaTime](https://nodatime.org) types.

## Supported NodaTime Types

- `AnnualDate` (Month, Day)
- `DateInterval` (Start, End)
- `DateTimeZone` (IANA zone ID string)
- `Duration` (Roundtrip ISO-8601)
- `Instant` (Extended ISO-8601 with nanosecond precision)
- `Interval` (Start, End)
- `LocalDate` (ISO-8601 date)
- `LocalDateTime` (Full roundtrip ISO-8601)
- `LocalTime` (Extended ISO-8601 time with nanosecond precision)
- `Offset` (General contracted)
- `OffsetDate` (General ISO-8601)
- `OffsetDateTime` (Full roundtrip ISO-8601)
- `OffsetTime` (Extended ISO-8601 with nanosecond precision)
- `Period` (Roundtrip ISO-8601 period)
- `YearMonth` (ISO-8601 year and month)
- `ZonedDateTime` (Extended ISO-8601 with IANA zone)

---

## Installation

Install via the .NET CLI:

```bash
dotnet add package NHibernate.UserTypes.NodaTime
```

Or via the Package Manager Console:

```powershell
Install-Package NHibernate.UserTypes.NodaTime
```

### Target Frameworks & Compatibility
- **Library Target**: `.NET Standard 2.1` (compatible with .NET 5, 6, 7, 8, 9, 10+)
- **Minimum Dependencies**:
  - `NHibernate` >= 5.4.7
  - `NodaTime` >= 3.1.8

---

## Usage

You can map NodaTime types in your NHibernate code-first mappings in one of two ways:
1. **Using Mapping Helpers (`NodaTimeClassMapping<TEntity>`)** *(Recommended)*: Deriving your entity mappings from `NodaTimeClassMapping<TEntity>` gives you clean, single-line helper methods for all NodaTime types with sensible column naming conventions.
2. **Standard NHibernate Mapping (`ClassMapping<TEntity>`)**: Using standard `Property(p => p.Prop, m => m.Type<NodaTime...UserType>())`.

---

### Mapping Examples

#### 1. AnnualDate
Stores month and day as two `Int32` columns.

```csharp
// Helper method (defaults to PropertyName_Month and PropertyName_Day columns)
AnnualDateProperty(p => p.AnnualDate);

// Standard mapping
Property(p => p.AnnualDate, m =>
{
    m.Columns(
        cm => cm.Name("AnnualDate_Month"),
        cm => cm.Name("AnnualDate_Day"));
    m.Type<NodaTimeAnnualDateUserType>();
});
```

#### 2. DateInterval
Stores start and end dates as two ISO-8601 `LocalDate` strings.

```csharp
// Helper method (defaults to PropertyName_Start and PropertyName_End columns)
DateIntervalProperty(p => p.DateInterval);

// Standard mapping
Property(p => p.DateInterval, m =>
{
    m.Columns(
        cm => cm.Name("DateInterval_Start"),
        cm => cm.Name("DateInterval_End"));
    m.Type<NodaTimeDateIntervalUserType>();
});
```

#### 3. DateTimeZone
Stores the canonical IANA zone ID string (e.g. `"Europe/London"` or `"America/New_York"`), resolved via `DateTimeZoneProviders.Tzdb`.

```csharp
// Helper method
DateTimeZoneProperty(p => p.TimeZone);

// Standard mapping
Property(p => p.TimeZone, m => m.Type<NodaTimeDateTimeZoneUserType>());
```

#### 4. Duration
Stores lossless roundtrip ISO-8601 duration string.

```csharp
// Helper method
DurationProperty(p => p.Duration);

// Standard mapping
Property(p => p.Duration, m => m.Type<NodaTimeDurationUserType>());
```

#### 5. Instant
Stores ISO-8601 instant with up to 9 digits of nanosecond precision (`ExtendedIso`).

```csharp
// Helper method
InstantProperty(p => p.Instant);

// Standard mapping
Property(p => p.Instant, m => m.Type<NodaTimeInstantUserType>());
```

#### 6. Interval
Stores start and end instants as two ISO-8601 strings.

```csharp
// Helper method (defaults to PropertyName_Start and PropertyName_End columns)
IntervalProperty(p => p.Interval);

// Standard mapping
Property(p => p.Interval, m =>
{
    m.Columns(
        cm => cm.Name("Interval_Start"),
        cm => cm.Name("Interval_End"));
    m.Type<NodaTimeIntervalUserType>();
});
```

#### 7. LocalDate
Stores ISO-8601 date string (`yyyy-MM-dd`).

```csharp
// Helper method
LocalDateProperty(p => p.LocalDate);

// Standard mapping
Property(p => p.LocalDate, m => m.Type<NodaTimeLocalDateUserType>());
```

#### 8. LocalDateTime
Stores ISO-8601 date and time string without timezone offset.

```csharp
// Helper method
LocalDateTimeProperty(p => p.LocalDateTime);

// Standard mapping
Property(p => p.LocalDateTime, m => m.Type<NodaTimeLocalDateTimeUserType>());
```

#### 9. LocalTime
Stores ISO-8601 time string with up to nanosecond precision (`ExtendedIso`).

```csharp
// Helper method
LocalTimeProperty(p => p.LocalTime);

// Standard mapping
Property(p => p.LocalTime, m => m.Type<NodaTimeLocalTimeUserType>());
```

#### 10. Offset
Stores timezone offset string (e.g., `+05`, `+05:30`).

```csharp
// Helper method
OffsetProperty(p => p.Offset);

// Standard mapping
Property(p => p.Offset, m => m.Type<NodaTimeOffsetUserType>());
```

#### 11. OffsetDate
Stores ISO-8601 date with offset.

```csharp
// Helper method
OffsetDateProperty(p => p.OffsetDate);

// Standard mapping
Property(p => p.OffsetDate, m => m.Type<NodaTimeOffsetDateUserType>());
```

#### 12. OffsetDateTime
Stores full roundtrip ISO-8601 date, time, and offset.

```csharp
// Helper method
OffsetDateTimeProperty(p => p.OffsetDateTime);

// Standard mapping
Property(p => p.OffsetDateTime, m => m.Type<NodaTimeOffsetDateTimeUserType>());
```

#### 13. OffsetTime
Stores ISO-8601 time with offset with nanosecond precision.

```csharp
// Helper method
OffsetTimeProperty(p => p.OffsetTime);

// Standard mapping
Property(p => p.OffsetTime, m => m.Type<NodaTimeOffsetTimeUserType>());
```

#### 14. Period
Stores ISO-8601 period string (e.g., `P1Y2M3W4DT5H6M7S`).

```csharp
// Helper method
PeriodProperty(p => p.Period);

// Standard mapping
Property(p => p.Period, m => m.Type<NodaTimePeriodUserType>());
```

#### 15. YearMonth
Stores ISO-8601 year and month string (`yyyy-MM`).

```csharp
// Helper method
YearMonthProperty(p => p.YearMonth);

// Standard mapping
Property(p => p.YearMonth, m => m.Type<NodaTimeYearMonthUserType>());
```

#### 16. ZonedDateTime
Stores ISO-8601 extended date/time along with IANA timezone ID (e.g. `2024-11-04T15:30:45.123456789+00[Europe/London]`).

```csharp
// Helper method
ZonedDateTimeProperty(p => p.ZonedDateTime);

// Standard mapping
Property(p => p.ZonedDateTime, m => m.Type<NodaTimeZonedDateTimeUserType>());
```

---

## Customizing Columns with Helper Methods

All helper methods accept an optional `Action<IPropertyMapper>` callback to customize column names or other NHibernate mappings:

```csharp
public class OrderMap : NodaTimeClassMapping<Order>
{
    public OrderMap()
    {
        Id(x => x.Id, m => m.Generator(Generators.Identity));

        InstantProperty(x => x.CreatedAt, m =>
        {
            m.Column("created_at_utc");
            m.NotNullable(true);
        });

        DateTimeZoneProperty(x => x.CustomerTimeZone, m =>
        {
            m.Column("customer_tz");
            m.Length(64);
        });
    }
}
```

---

## License

This project is licensed under the [MIT License](LICENSE.txt).
