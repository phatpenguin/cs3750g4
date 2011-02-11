using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Controls.Behaviors
{
    public class VisibilityBehavior {
        public static object defaultObj = new object();
        public static DependencyProperty VisibleIfTrueProperty = DependencyProperty.RegisterAttached("VisibleIfTrue",
                    typeof(object),
                    typeof(VisibilityBehavior),
                    new FrameworkPropertyMetadata(defaultObj, new PropertyChangedCallback(VisibleIfTrueChanged)));

        public static DependencyProperty VisibleIfFalseProperty = DependencyProperty.RegisterAttached("VisibleIfFalse",
                    typeof(object),
                    typeof(VisibilityBehavior),
                    new FrameworkPropertyMetadata(defaultObj, new PropertyChangedCallback(VisibleIfFalseChanged)));

        public static DependencyProperty NonVisibleStateProperty = DependencyProperty.RegisterAttached("NonVisibleState",
                    typeof(Visibility),
                    typeof(VisibilityBehavior),
                    new FrameworkPropertyMetadata(Visibility.Collapsed, NonVisibleStateChanged));


        public static void SetVisibleIfTrue(DependencyObject target, object value) { }

        public static void SetVisibleIfFalse(DependencyObject target, object value) { }

        public static void SetNonVisibleState(DependencyObject target, object value) { }

        private static void VisibleIfTrueChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = target as FrameworkElement;
            if (element != null)
            {
                element.Visibility = Convert(element);
            }
        }

        private static void NonVisibleStateChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = target as FrameworkElement;
            if (element != null)
            {
                element.Visibility = Convert(element);
            }
        }



        private static void VisibleIfFalseChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = target as FrameworkElement;
            if (element != null)
            {
                element.Visibility = Convert(element);
            }
        }
        private static Visibility Convert(FrameworkElement element)
        {
            object value = element.GetValue(VisibleIfTrueProperty);
            bool? hideIfTrue = null;

            if (value == defaultObj)
            {
                value = element.GetValue(VisibleIfFalseProperty);
                hideIfTrue = true;
                if (value == defaultObj)
                {
                    return Visibility.Visible;
                }
            }


            bool flag = false;
            if (value is string)
            {
                flag = StringToBoolean((string)value);
            }
            else if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue ? nullable.Value : false;
            }
            else if (value is Visibility)
            {
                flag = (Visibility)value == Visibility.Visible;
            }
            else if (value != null)
            {
                flag = true;
            }

            if (hideIfTrue.HasValue && hideIfTrue.Value)
            {
                flag = !flag;
            }

            return (flag ? Visibility.Visible : (Visibility)element.GetValue(NonVisibleStateProperty));
        }

        private static bool StringToBoolean(string str)
        {
            if (str != null && !str.Equals("") && !str.ToUpper().Equals("FALSE"))
            {
                return true;
            }
            if (str != null && str.ToUpper().Equals("FALSE"))
            {
                return false;
            }
            return false;
        }
    }
}
