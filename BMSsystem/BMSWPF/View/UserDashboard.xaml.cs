using BMSWPF.ViewModel;
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

namespace BMSWPF.View
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void View_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new RedViewModel();
        }

        private void Update_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new BlueViewModel();
        }

        private void ApplyLoan_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplyLoanViewModel();
        }

        private void ViewLoan_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new OrangeViewModel();
        }
    }
}
