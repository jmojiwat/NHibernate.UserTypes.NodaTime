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

You can either use the custom value types in your ```ClassMapping<>``` by declaring the column(s) and property type, or use the helper methods.

To use the helper methods, change your mapping class to derive from ```NodaTimeClassMapping<>``` instead of ```ClassMapping<>```.

The Property method's ```Action<IPropertyMapper> mapping``` parameter you pass into the helper methods will take precedence.

## AnnualDate

```csharp
Property(p => p.AnnualDate, 
	m =>
	{
		m.Columns(
			cm => cm.Name("AnnualDate_Month"),	// Int32
			cm => cm.Name("AnnualDate_Day"));	// Int32
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
			cm => cm.Name("DateInterval_Start"),		// Int64
			cm => cm.Name("DateInterval_End"),			// Int64
			cm => cm.Name("DateInterval_Calendar"));	// String
		m.Type<NodaTimeDateIntervalUserType>();
	});
```

or

```csharp
DateIntervalProperty(p => p.DateInterval);
```

## Duration

```csharp
Property(p => p.Duration, m => m.Type<NodaTimeDurationUserType>());	// Int64
```

or

```csharp
DurationProperty(p => p.Duration);
```

## Instant

```csharp
Property(p => p.Instant, m => m.Type<NodaTimeInstantUserType>()); // Int64
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
			cm => cm.Name("Interval_Interval"),	// Boolean
			cm => cm.Name("Interval_Start"),	// Int64
			cm => cm.Name("Interval_End"));		// Int64
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
			cm => cm.Name("LocalDate_Date"),		// Int64
			cm => cm.Name("LocalDate_Calendar"));	// String
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
			cm => cm.Name("LocalDateTime_DateTime"),	// Int64
			cm => cm.Name("LocalDateTime_Calendar"));	// String
		m.Type<NodaTimeLocalDateUserType>();
	});
```

or 

```csharp
LocalDateTimeProperty(p => p.LocalDateTime);
```

## LocalTime

```csharp
Property(p => p.LocalTime, m => m.Type<NodaTimeLocalTimeUserType>());	// Int64
```

or 

```csharp
LocalTimeProperty(p => p.LocalTime);
```

## Offset

```csharp
Property(p => p.Offset, m => m.Type<NodaTimeOffsetUserType>());		// Int64
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
			cm => cm.Name("OffsetDate_Date"),		// Int64
			cm => cm.Name("OffsetDate_Offset"),		// Int64
			cm => cm.Name("OffsetDate_Calendar"));	// String
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
			cm => cm.Name("OffsetDateTime_DateTime"),	// Int64
			cm => cm.Name("OffsetDateTime_Offset"),		// Int64
			cm => cm.Name("OffsetDateTime_Calendar"));	// String
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
			cm => cm.Name("OffsetTime_Time"),		// Int64
			cm => cm.Name("OffsetTime_Offset"));	// Int64
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
			cm => cm.Name("Period_Days"),			// Int32
			cm => cm.Name("Period_Hours"),			// Int64
			cm => cm.Name("Period_Milliseconds"),	// Int64
			cm => cm.Name("Period_Minutes"),		// Int64
			cm => cm.Name("Period_Months"),			// Int32
			cm => cm.Name("Period_Nanoseconds"),	// Int64
			cm => cm.Name("Period_Seconds"),		// Int64
			cm => cm.Name("Period_Ticks"),			// Int64
			cm => cm.Name("Period_Weeks"),			// Int32
			cm => cm.Name("Period_Years"));			// Int32
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
			cm => cm.Name("ZonedDateTime_DateTime"),	// Int64
			cm => cm.Name("ZonedDateTime_Zone"),		// String
			cm => cm.Name("ZonedDateTime_Calendar"));	// String
		m.Type<NodaTimeZonedDateTimeUserType>();
	});
```

or 

```csharp
ZonedDateTimeProperty(p => p.ZonedDateTime);
```


