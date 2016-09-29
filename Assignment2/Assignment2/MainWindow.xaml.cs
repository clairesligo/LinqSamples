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

namespace Assignment2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ex1Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        group c by c.Country into g
                        orderby g.Count() descending
                        select new
                        {
                            Country = g.Key,
                            Count = g.Count()
                        };
            Ex1lbDisplay.ItemsSource = query.ToList();
        }
        private void Ex2Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Customers
                        where c.Country == "Italy"
                        select c;


            Ex2lbDisplay.ItemsSource = query.ToList();
        }
        private void Ex3Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Products
                        where (c.UnitsInStock - c.UnitsOnOrder) > 0
                        select new
                        {
                            Product = c.ProductName,
                            Quantity = c.UnitsInStock
                        };
            Ex3lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex4Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Order_Details
                        orderby c.Product.ProductName
                        where c.Discount > 0
                        select new
                        {
                            ProductName = c.Product.ProductName,
                            DiscountReceived = c.Discount,
                            OrderID = c.OrderID
                        };
            Ex4lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex5Button_Click(object sender, RoutedEventArgs e)
        {
            var query = (from c in db.Orders
                        select c.Freight).Sum();
            Ex5TblkDisplay.Text = string.Format("The total value of freight of all orders is {0:C}", query);

        }

        private void Ex6Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from c in db.Categories
                        join p in db.Products on c.CategoryID equals p.CategoryID
                        orderby c.CategoryName, p.UnitPrice descending
                        select new
                        {
                            CategoryID = c.CategoryID,
                            CategoryName = c.CategoryName,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice
                        };
            Ex6lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex7Button_Click(object sender, RoutedEventArgs e)
        {
            var query = (from p in db.Customers
                        orderby p.Orders.Count descending
                        select new 
                        {
                          CustomerID =  p.CustomerID,
                          NumberOfOrders = p.Orders.Count
                        }).Take(10);
            Ex7lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex8Button_Click(object sender, RoutedEventArgs e)
        {
            var query = (from p in db.Customers
                         orderby p.Orders.Count descending
                         select new
                         {
                             CustomerID = p.CustomerID,
                             CompanyName = p.CompanyName,
                             NumberOfOrders = p.Orders.Count
                         }).Take(10);
            Ex8lbDisplay.ItemsSource = query.ToList();
        }

        private void Ex9Button_Click(object sender, RoutedEventArgs e)
        {
            var query = from p in db.Customers
                        where p.Orders.Count == 0
                        select new
                        {
                            CompanyName = p.CompanyName,
                            NumberOfOrders = p.Orders.Count
                        };
            Ex9lbDisplay.ItemsSource = query.ToList();
        }
    }
}
