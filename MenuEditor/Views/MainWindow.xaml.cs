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
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand NewCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            var viewmodel = new MainWindowViewModel(new JsonDataService());
            DataContext = viewmodel;
            TopMenu.DataContext = viewmodel.TopMenu;
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            NewCommand.InputGestures.Add(new KeyGesture(Key.K, ModifierKeys.Control));
        }

        private void onLbMenuLostFocus(object sender, RoutedEventArgs e)
        {
            lbMenu.SelectedItem = null;
        }

        private void onLbModalLostFocus(object sender, RoutedEventArgs e)
        {
            lbModal.SelectedItem = null;
        }

        private void onSaveData(object sender, ExecutedRoutedEventArgs e)
        {
            var model = new MainWindowViewModel(new JsonDataService());
            model.onSaveData();
        }

        private void onNewProj(object sender, ExecutedRoutedEventArgs e)
        {
            var model = new MainWindowViewModel(new JsonDataService());
            model.onNewProj();
        }
    }
}
