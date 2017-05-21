using System.Windows;

namespace MazeMVVM.ViewLib
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        /// <summary>
        /// Message to display in the window.
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public MessageWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}