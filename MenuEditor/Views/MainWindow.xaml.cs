using MenuEditor.Services;
using MenuEditor.ViewModels;
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

namespace MenuEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewmodel = new MainWindowViewModel(new JsonDataService());
            this.DataContext = viewmodel;
            this.TopMenu.DataContext = viewmodel.TopMenu;
        }

        private void onLbMenuLostFocus(object sender, RoutedEventArgs e)
        {
            lbMenu.SelectedItem = null;
        }

        private void onLbModalLostFocus(object sender, RoutedEventArgs e)
        {
            lbModal.SelectedItem = null;
        }
    }
}
