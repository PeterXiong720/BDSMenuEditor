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
using System.Windows.Shapes;

namespace MenuEditor.Views
{
    /// <summary>
    /// AddButtonCommandDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddButtonCommandDialog : UserControl
    {
        public AddButtonCommandDialog(ref Command vmodel)
        {
            InitializeComponent();
            DataContext = vmodel;
        }
    }
}
