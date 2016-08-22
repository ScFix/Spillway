using System.Windows;

namespace Spillway.Utilities.AttachedProperties
{
	/// <summary>
	/// This is an attached property that you derive a class from this given the base class the type and use that as the attached property,
	/// <see cref="http://stackoverflow.com/questions/22011005/bind-a-property-to-datatemplateselector"/>
	/// </summary>
	public class DataTemplateParameters<T> : DependencyObject
	{
		public static double GetValueToCompare(DependencyObject obj)
		{
			return (double)obj.GetValue(ValueToCompareProperty);
		}

		public static void SetValueToCompare(DependencyObject obj, T value)
		{
			obj.SetValue(ValueToCompareProperty, value);
		}

		public static readonly DependencyProperty ValueToCompareProperty = DependencyProperty.RegisterAttached("ValueToCompare", typeof(T), typeof(DataTemplateParameters<T>));
	}
}