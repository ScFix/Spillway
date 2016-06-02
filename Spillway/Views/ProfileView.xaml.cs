using Spillway.TemplateSelectors;
using Spillway.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spillway.Views
{
	/// <summary>
	/// Interaction logic for ProfileView.xaml
	/// </summary>
	public partial class ProfileView : UserControl
	{

		#region ViewState
		public static readonly DependencyProperty ViewStateProperty = DependencyProperty.Register("ViewState", typeof(ProfileViewState), typeof(UserControl), new PropertyMetadata(ViewStatePropertyChanged));
		public ProfileViewState ViewState
		{
			get
			{
				return (ProfileViewState)GetValue(ViewStateProperty);
			}
			set
			{
				if (value != ViewState)
				{
					SetValue(ViewStateProperty, value);
				}
			}
		}
		private static void ViewStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ProfileView pv = d as ProfileView;
			pv.UpdateContent();

		}

		#endregion //ViewState

		public ProfileView()
		{
			InitializeComponent();
		}

		public void UpdateContent()
		{
			// HACK(Matthe); This is a hack till I figure out a better way to sort out the data template selector and updating the view properly
			Dispatcher.Invoke(() =>
			{
				content.Content = null;
				content.Content = DataContext;
			});



		}
	}
}
