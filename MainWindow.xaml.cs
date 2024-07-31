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
using WPFModernVerticalMenu.Pages;

namespace Social_Blade_Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CategoriesPage categoriesPage;
        private ProductsPage productsPage;
        private Dashboard dashbord;
        public MainWindow()
        {
            InitializeComponent();
            categoriesPage = new CategoriesPage();
            productsPage = new ProductsPage();
           // RenderPages.Content = dashbord;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dashboard dashboardPage = new Dashboard();
            RenderPages.Content = dashboardPage;
        }

        private void btn_Categorie_click(object sender, RoutedEventArgs e)
        {
                RenderPages.Content = categoriesPage;
            
        }
        private void btn_Produit_click(object sender, RoutedEventArgs e)
        {
                RenderPages.Content = productsPage;
            
        }




        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
