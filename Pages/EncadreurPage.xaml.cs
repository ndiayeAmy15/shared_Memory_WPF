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
using WPFModernVerticalMenu.Services;
namespace WPFModernVerticalMenu.Pages
{
    /// <summary>
    /// Logique d'interaction pour EncadreurPage.xaml
    /// </summary>
    public partial class EncadreurPage : Page
    {
        EncadreurService serv = new EncadreurService();

        public EncadreurPage()
        {
            InitializeComponent();
            dgEncadreur.ItemsSource = serv.servGetListEncadreur();
        }

        private void dgEncadreur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgEncadreur.ItemsSource = serv.servGetListEncadreur();
        }
    }
}
