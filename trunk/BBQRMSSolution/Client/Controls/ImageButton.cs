using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
	public class ImageButton : Button
	{
		static ImageButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
		}

		public ImageSource ImageSource
		{
			get { return (ImageSource)GetValue(ImageSourceProperty); }
			set { SetValue(ImageSourceProperty, value); }
		}

		public static readonly DependencyProperty ImageSourceProperty =
				DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		public static readonly DependencyProperty OrientationProperty =
				DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ImageButton), new UIPropertyMetadata(Orientation.Vertical));
		
	}
}
