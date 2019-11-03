# NHibernate.UserTypes.NodaTime

This libarary is a collection of [NHibernate](https://nhibernate.info/) custom value types for [NodaTime](https://nodatime.org). Current custom types inclue:
- AnnualDate
- DateInterval
- Duration
- Instant
- Interval
- LocalDate
- LocalDateTime
- LocalTime
- Offset
- OffsetDate
- OffsetDateTime
- OffsetTime
- Period
- ZonedDateTime

# Usage

You can either use the custom value types in your ```csharp ClassMapping<>``` by declaring the column(s) and property type, or use the helper methods.

To use the helper methods, change your mapping class to derive from ```csharp NodaTimeClassMapping<>``` instead of ```csharp ClassMapping<>```.

The Property method's ```csharp Action<IPropertyMapper> mapping``` parameter you pass into the helper methods will take precedence.

## AnnualDate

```csharp
Property(p => p.AnnualDate, 
	m =>
	{
		m.Columns(
			cm => cm.Name("AnnualDate_Month"),
			cm => cm.Name("AnnualDate_Day"));
		m.Type<NodaTimeAnnualDateUserType>();
	});
```

or

```csharp
AnnualDateProperty(p => p.AnnualDate);
```

## DateInterval

```csharp
Property(p => p.DateInterval, 
	m =>
	{
		m.Columns(
			cm => cm.Name("DateInterval_Start"),
			cm => cm.Name("DateInterval_End"),
			cm => cm.Name("DateInterval_Calendar"));
		m.Type<NodaTimeDateIntervalUserType>();
	});
```

or

```csharp
DateIntervalProperty(p => p.DateInterval);
```

## Duration

```csharp
Property(p => p.Duration, m => m.Type<NodaTimeDurationUserType>());
```

or

```csharp
DurationProperty(p => p.Duration);
```

## Instant

```csharp
Property(p => p.Instant, m => m.Type<NodaTimeInstantUserType>());
```

or 

```csharp
InstantProperty(p => p.Instant);
```

## Interval

```csharp
Property(p => p.Interval, 
	m =>
	{
		m.Columns(
			cm => cm.Name("Interval_Interval"),
			cm => cm.Name("Interval_Start"),
			cm => cm.Name("Interval_End"));
		m.Type<NodaTimeIntervalUserType>();
	});
```

or 

```csharp
IntervalProperty(p => p.Interval);
```

## LocalDate

```csharp
Property(p => p.LocalDate, 
	m =>
	{
		m.Columns(
			cm => cm.Name("LocalDate_Date"),
			cm => cm.Name("LocalDate_Calendar"));
		m.Type<NodaTimeLocalDateUserType>();
	});
```

or 

```csharp
LocalDateProperty(p => p.LocalDate);
```

## LocalDateTime

```csharp
Property(p => p.LocalDateTime, 
	m =>
	{
		m.Columns(
			cm => cm.Name("LocalDateTime_DateTime"),
			cm => cm.Name("LocalDateTime_Calendar"));
		m.Type<NodaTimeLocalDateUserType>();
	});
```

or 

```csharp
LocalDateTimeProperty(p => p.LocalDateTime);
```

## LocalTime

```csharp
Property(p => p.LocalTime, m => m.Type<NodaTimeLocalTimeUserType>());
```

or 

```csharp
LocalTimeProperty(p => p.LocalTime);
```

## Offset

```csharp
Property(p => p.Offset, m => m.Type<NodaTimeOffsetUserType>());
```

or 

```csharp
OffsetProperty(p => p.Offset);
```

## OffsetDate

```csharp
Property(p => p.OffsetDate, 
	m =>
	{
		m.Columns(
			cm => cm.Name("OffsetDate_Date"),
			cm => cm.Name("OffsetDate_Offset"),
			cm => cm.Name("OffsetDate_Calendar"));
		m.Type<NodaTimeOffsetDateUserType>();
	});
```

or 

```csharp
OffsetDateProperty(p => p.OffsetDate);
```

## OffsetDateTime

```csharp
Property(p => p.OffsetDateTime, 
	m =>
	{
		m.Columns(
			cm => cm.Name("OffsetDateTime_DateTime"),
			cm => cm.Name("OffsetDateTime_Offset"),
			cm => cm.Name("OffsetDateTime_Calendar"));
		m.Type<NodaTimeOffsetDateTimeUserType>();
	});
```

or 

```csharp
OffsetDateTimeProperty(p => p.OffsetDateTime);
```

## OffsetTime

```csharp
Property(p => p.OffsetTime, 
	m =>
	{
		m.Columns(
			cm => cm.Name("OffsetTime_Time"),
			cm => cm.Name("OffsetTime_Offset"));
		m.Type<NodaTimeOffsetTimeUserType>();
	});
```

or 

```csharp
OffsetTimeProperty(p => p.OffsetTime);
```

## Period

```csharp
Property(p => p.Period, 
	m =>
	{
		m.Columns(
			cm => cm.Name("Period_Days"),
			cm => cm.Name("Period_Hours"),
			cm => cm.Name("Period_Milliseconds"),
			cm => cm.Name("Period_Minutes"),
			cm => cm.Name("Period_Months"),
			cm => cm.Name("Period_Nanoseconds"),
			cm => cm.Name("Period_Seconds"),
			cm => cm.Name("Period_Ticks"),
			cm => cm.Name("Period_Weeks"),
			cm => cm.Name("Period_Years"));
		m.Type<NodaTimePeriodUserType>();
	});
```

or 

```csharp
PeriodProperty(p => p.Period);
```

## ZonedDateTime

```csharp
Property(p => p.ZonedDateTime, 
	m =>
	{
		m.Columns(
			cm => cm.Name("ZonedDateTime_DateTime"),
			cm => cm.Name("ZonedDateTime_Zone"),
			cm => cm.Name("ZonedDateTime_Calendar"));
		m.Type<NodaTimeZonedDateTimeUserType>();
	});
```

or 

```csharp
ZonedDateTimeProperty(p => p.ZonedDateTime);
```


