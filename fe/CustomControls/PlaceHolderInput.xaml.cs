using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace fe.CustomControls
{
    /// <summary>
    /// Interaction logic for PlaceHolderInput.xaml
    /// </summary>
    public partial class PlaceHolderInput : UserControl
    {
        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.Register(nameof(TextContent), 
                typeof(string), 
                typeof(PlaceHolderInput),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private string placeHolder = string.Empty;

        public string TextContent
        {
            get => (string)GetValue(TextContentProperty);
            set => SetValue(TextContentProperty, value);
        }

        public string PlaceHolder
        {
            get => placeHolder;
            set => placeHolder = value;
        }

        public PlaceHolderInput()
        {
            InitializeComponent();
        }

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            PlaceHolderInp.Visibility = Visibility.Hidden;
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            PlaceHolderInp.Visibility = string.IsNullOrEmpty(TextContent) ? Visibility.Visible : Visibility.Hidden;
        }
    }

}
