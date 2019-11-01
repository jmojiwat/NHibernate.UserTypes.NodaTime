using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeIntervalUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var interval = (Interval)component;
            switch (property)
            {
                case 0:
                    return true;
                case 1:
                    return interval.HasStart ? interval.Start : (object) null;
                case 2:
                    return interval.HasEnd ? interval.End : (object) null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(property));
            }
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new InvalidOperationException($"{nameof(Interval)} is an immutable object. SetPropertyValue is not supported");
        }

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            var lhs = (Interval)x;
            var rhs = (Interval)y;

            return lhs.Equals(rhs);
        }

        public int GetHashCode(object x) => ((Interval)x).GetHashCode();

        public object NullSafeGet(DbDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            var interval = (bool)NHibernateUtil.Boolean.NullSafeGet(dr, names[0], session, owner);
            var startTicks = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[1], session, owner);
            var endTicks = (long?)NHibernateUtil.Int64.NullSafeGet(dr, names[2], session, owner);

            var start = startTicks.HasValue ? Instant.FromUnixTimeTicks(startTicks.Value) : (Instant?) null;
            var end = endTicks.HasValue ? Instant.FromUnixTimeTicks(endTicks.Value) : (Instant?) null;
            
            if(interval)
            {
                return new Interval(start, end);
            }
            else
            {
                return null;
            }
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            var interval = (Interval?)value;
            var start = interval.HasValue && (bool) interval?.HasStart ? interval?.Start.ToUnixTimeTicks() : null;
            var end = interval.HasValue && (bool) interval?.HasEnd ? interval?.End.ToUnixTimeTicks() : null;

            if (settable[0])
            {
                NHibernateUtil.Boolean.NullSafeSet(cmd, interval.HasValue, index++, session);
            }
            if (settable[1])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, start, index++, session);
            }

            if (settable[2])
            {
                NHibernateUtil.Int64.NullSafeSet(cmd, end, index, session);
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] {"Interval", "Start", "End"};
        public IType[] PropertyTypes => new IType[] {NHibernateUtil.Boolean, NHibernateUtil.Int64, NHibernateUtil.Int64};
        public System.Type ReturnedClass => typeof(Interval);
        public bool IsMutable => false;
    }
}