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
using static SystemModeling.ValuesLibrary;

namespace SystemModeling
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                H = ToDouble(TB_H.Text);
                Tm0 = ToDouble(TB_Tm0.Text);
                Tg0 = ToDouble(TB_Tg0.Text);
                Wg = ToDouble(TB_Wg.Text);
                Cg = ToDouble(TB_Cg.Text);
                Rate = ToDouble(TB_Rate.Text);
                Cok = ToDouble(TB_Cok.Text);
                AlfaV = ToDouble(TB_AlfaV.Text);
                D = ToDouble(TB_D.Text);

                M = FindM(Cok, Rate, Cg, Wg, D);
                Y0 = FindY0(AlfaV, H, Wg, Cg);
                E1 = FindE1(M, Y0);

                TB_M.Text = ToStr(M);
                TB_Y0.Text = ToStr(Y0);
                TB_E1.Text = ToStr(E1);

                Grid_Loaded(sender, e);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (H != 0)
            {
                grid.Visibility = Visibility.Visible;
                double x = 0;
                FindXY();

                List<MyTable> result = new List<MyTable>(Razm);
                for (int i = 0; i < Razm; i++)
                {
                    result.Add(new MyTable(x, Y[i], E2[i], E3[i], Teta1[i], Teta2[i], T1[i], T2[i], T[i]));
                    x += 0.5;
                }
                grid.ItemsSource = result;
            }
            else
            {
                grid.Visibility = Visibility.Hidden;
            }
        }

        private void Graph_Click(object sender, RoutedEventArgs e)
        {
            Switch = 0;
            Graph graph = new Graph();
            graph.Show();
        }

        private void TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789,".IndexOf(e.Text, StringComparison.CurrentCulture) < 0;
        }

        private void Graph1_Click(object sender, RoutedEventArgs e)
        {
            Switch = 1;
            Graph graph = new Graph();
            graph.Show();
        }
    }
}
