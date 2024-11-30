using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace ProductsList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _products { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _products = new ObservableCollection<Product>();
            ProductsList.ItemsSource = _products;
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;
            double price;
            int quantity;

            Product newProduct;

            if (!double.TryParse(PriceInput.Text, out price) || price <= 0)
            {
                MessageBox.Show("Значение цены некорректно.");
                return;
            }

            if (QuantityInput.Text == "")
            {
                newProduct = new Product(name, price);
                _products.Add(newProduct);

                NameInput.Clear();
                PriceInput.Clear();
                QuantityInput.Clear();

                return;

            }
            else if (int.TryParse(QuantityInput.Text, out quantity) && quantity > 0)
            {
                
                newProduct = new Product(name, price, quantity);
                _products.Add(newProduct);

                NameInput.Clear();
                PriceInput.Clear();
                QuantityInput.Clear();

                return;
            }
            else
            {
                MessageBox.Show("Количество товара некорректно.");
            }
        }

        private void ShowProductInfo(object sender, RoutedEventArgs e)
        {
           if (ProductsList.SelectedItem is Product selectedItem)
            {
                selectedItem.Deconstruct(out string name, out double price, out int quantity);
                MessageBox.Show($"Название: {name} \nЦена: {price} \nКоличество: {quantity} шт.", "Информация о товаре");
            }
            
        }
    }
}
