using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace EnACT
{
    class TimestampTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            if (value == null)
                return new Timestamp();

            if (value is String)
            {
                String vstring = value as String;
                return new Timestamp(vstring);
            }
            else
                return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if ((destinationType == typeof(string)) |
                (destinationType == typeof(InstanceDescriptor)))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value != null && !(value is Timestamp))
                throw new Exception("Value is of wrong type");

            if (destinationType == typeof(String))
            {
                Timestamp t = value as Timestamp;
                return t.AsString;
            }

            if (destinationType == typeof(InstanceDescriptor))
            {
                if (value == null)
                    return null;

                Timestamp t = value as Timestamp;

                MemberInfo memberInfo = typeof(Timestamp).GetConstructor(new Type[] { typeof(String) });
                object[] args = new object[] { t.AsString };

                if (memberInfo != null)
                    return new InstanceDescriptor(memberInfo, args);
                else
                    return null;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
