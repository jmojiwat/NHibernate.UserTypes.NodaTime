using System;
using System.Data.Common;
using NHibernate.Engine;
using NHibernate.Type;
using NodaTime;
using NodaTime.Text;

namespace NHibernate.UserTypes.NodaTime
{
    public class NodaTimeIntervalUserType : ICompositeUserType
    {
        public object GetPropertyValue(object component, int property)
        {
            var interval = (Interval)component;
            return property switch
            {
                0 => interval.HasStart ? interval.Start : (object)null,
                1 => interval.HasEnd ? interval.End : (object)null,
                _ => throw new ArgumentOutOfRangeException(nameof(property))
            };
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

            if (x is null || y is null)
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
            var startFormat = (string)NHibernateUtil.String.NullSafeGet(dr, names[0], session, owner);
            var endFormat = (string)NHibernateUtil.String.NullSafeGet(dr, names[1], session, owner);

            if (startFormat == null && endFormat == null)
            {
                return null;
            }

            Instant? startInstant = null;
            if (!string.IsNullOrEmpty(startFormat))
            {
                var startParsed = InstantPattern.General.Parse(startFormat);
                if (startParsed.Success)
                {
                    startInstant = startParsed.Value;
                }
            }

            Instant? endInstant = null;
            if (!string.IsNullOrEmpty(endFormat))
            {
                var endParsed = InstantPattern.General.Parse(endFormat);
                if (endParsed.Success)
                {
                    endInstant = endParsed.Value;
                }
            }

            return new Interval(startInstant, endInstant);
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            if (value is Interval interval)
            {
                var startFormat = interval.HasStart ? InstantPattern.General.Format(interval.Start) : string.Empty;
                var endFormat = interval.HasEnd ? InstantPattern.General.Format(interval.End) : string.Empty;

                if (settable[0])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, startFormat, index, session);
                }
                if (settable[1])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, endFormat, index + 1, session);
                }
            }
            else
            {
                if (settable[0])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, null, index, session);
                }
                if (settable[1])
                {
                    NHibernateUtil.String.NullSafeSet(cmd, null, index + 1, session);
                }
            }
        }

        public object DeepCopy(object value) => value;

        public object Disassemble(object value, ISessionImplementor session) => value;

        public object Assemble(object cached, ISessionImplementor session, object owner) => cached;

        public object Replace(object original, object target, ISessionImplementor session, object owner) => original;

        public string[] PropertyNames => new[] { "Start", "End" };
        public IType[] PropertyTypes => new IType[] { NHibernateUtil.String, NHibernateUtil.String };
        public System.Type ReturnedClass => typeof(Interval);
        public bool IsMutable => false;
    }
}
