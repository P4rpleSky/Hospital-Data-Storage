using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Kosta_Task.Attributes
{
	public class DateAttribute : ValidationAttribute
	{
		public DateTime Minimum { get; }
		public DateTime Maximum { get; }

		public DateAttribute(string? minimum = null, string? maximum = null, string? format = null)
		{
			format = format ?? @"yyyy-MM-dd'T'HH:mm:ss.FFFK";

			Minimum = minimum == null ? DateTime.MinValue : DateTime.ParseExact(minimum, new[] { format }, CultureInfo.InvariantCulture, DateTimeStyles.None);
			Maximum = maximum == null ? DateTime.MaxValue : DateTime.ParseExact(maximum, new[] { format }, CultureInfo.InvariantCulture, DateTimeStyles.None);

			if (Minimum > Maximum)
				throw new InvalidOperationException($"Specified max-date '{maximum}' is less than the specified min-date '{minimum}'");
		}

		public override bool IsValid(object? value)
		{
			if (value == null)
				return true;

			var s = value as string;
			if (s != null && string.IsNullOrEmpty(s))
				return true;

			var min = (IComparable)Minimum;
			var max = (IComparable)Maximum;
			return min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0;
		}

		public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Minimum, Maximum);
	}
}
