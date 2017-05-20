using System;
using System.ComponentModel;
using System.Globalization;
using MazeLib;

namespace MazeMVVM.ViewLib.Converter
{
    class PositionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string positionStr = value as string;
            if (positionStr != null)
            {
                int index = positionStr.IndexOf(",", StringComparison.Ordinal);
                int row = int.Parse(positionStr.Substring(1, index - 1));
                int col = int.Parse(positionStr.Substring(index + 1, positionStr.Length - index - 2));
                return new Position(row, col);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
            CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}