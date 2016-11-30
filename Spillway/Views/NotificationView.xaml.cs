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
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : UserControl
    {
        public static readonly DependencyProperty DblClickCommandProperty = DependencyProperty.Register("DblClickCommand", typeof(ICommand), typeof(NotificationView), new PropertyMetadata(DblClickCommandChanged));
        public ICommand DblClickCommand
        {
            get { return (ICommand)GetValue(DblClickCommandProperty); }
            set { SetValue(DblClickCommandProperty, value); }
        }
        private static void DblClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as NotificationView;
        }


        public static readonly DependencyProperty DblClickParamProperty = DependencyProperty.Register("DblClickParam", typeof(object), typeof(NotificationView), new PropertyMetadata(DblClickParamChanged));
        public object DblClickParam
        {
            get { return (object)GetValue(DblClickParamProperty); }
            set { SetValue(DblClickParamProperty, value); }
        }
        private static void DblClickParamChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as NotificationView;
        }



        public NotificationView()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Note(Mattew): you could techinically validate this in order to make sure the webaddress is really valid but for right now this is fine. 
            DblClickCommand?.Execute(DblClickParam);
        }
    }
}
