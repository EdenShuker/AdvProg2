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

namespace MazeMVVM.ViewLib.Controls
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : UserControl
    {


        public int Prop
        {
            get { return (int) GetValue(PropProperty); }
            set { SetValue(PropProperty, value); }
        }

        // Using a DependencyProperty as the backing store for prop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PropProperty =
            DependencyProperty.Register("Prop", typeof(int), typeof(Test), new PropertyMetadata(0));



        public Test()
        {
            InitializeComponent();
        }
    }
}
