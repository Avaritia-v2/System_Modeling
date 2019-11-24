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
using static SystemModeling.ValuesLibrary;

namespace SystemModeling
{
    /// <summary>
    /// Логика взаимодействия для Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        public Graph()
        {
            InitializeComponent();

            Color c = new Color() { ScA = 1, ScR = 0, ScG = 1, ScB = 0 }; //Цвет графика
            Color c2 = new Color() { ScA = 1, ScR = 1, ScG = 0, ScB = 0 }; //Цвет второго графика
            Color s = new Color() { ScA = 1, ScR = 0, ScG = 0, ScB = 0 }; //Цвет разметки


            Line line = new Line() { X1 = 0, Y1 = 0, X2 = 0, Y2 = 2000, Stroke = new SolidColorBrush(s), StrokeThickness = 2.0 };
            canvas.Children.Add(line);
            line = new Line() { X1 = 0, Y1 = 0, X2 = 2000, Y2 = 0, Stroke = new SolidColorBrush(s), StrokeThickness = 2.0 };
            canvas.Children.Add(line);
            double[] x = new double[Razm*2];
            double stepx = Width / Razm;
            int indent = 75;
            int a = -1;

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i * stepx;
            }
            for (int i = 1; i < Razm*2; i++) // Ось X
            {
                line = new Line() { X1 = x[1] * i, Y1 = 0, X2 = x[1] * i, Y2 = 2000, Stroke = new SolidColorBrush(s), StrokeThickness = 0.5 };
                canvas.Children.Add(line);
                var txt = new TextBlock() { Text = ToStr(i * 0.5) };
                Canvas.SetLeft(txt, x[1] * i);
                if (a > 0) Canvas.SetTop(txt, Height - indent); else Canvas.SetTop(txt, 0);
                canvas.Children.Add(txt);
            }

            if (Switch == 0)
            {
                if (H != 0)
                {
                    GraphName.Text = "Разность температур окатышей и газа";
                    if (T[0] > 0) //Разница положительна
                    {
                        a = 1;

                        for (int i = 0; i < Razm - 1; i++)
                        {
                            line = new Line() { X1 = x[i], Y1 = Height - indent - (a * T[i] * 6), X2 = x[i + 1], Y2 = Height - indent - (a * T[i + 1] * 6), Stroke = new SolidColorBrush(c), StrokeThickness = 4.0 };
                            canvas.Children.Add(line);
                        }
                    }
                    else //Разница отрицательна
                    {
                        for (int i = 0; i < Razm - 1; i++)
                        {
                            line = new Line() { X1 = x[i], Y1 = a * T[i] * 6, X2 = x[i + 1], Y2 = a * T[i + 1] * 6, Stroke = new SolidColorBrush(c), StrokeThickness = 4.0 };
                            canvas.Children.Add(line);
                        }
                    }

                    for (int i = 0; i < 100; i++) // Ось Y
                    {
                        line = new Line() { X1 = 0, Y1 = i * 60, X2 = 2000, Y2 = i * 60, Stroke = new SolidColorBrush(s), StrokeThickness = 0.5 };
                        canvas.Children.Add(line);
                        var txt = new TextBlock() { Text = ToStr(i * a * 10) };
                        Canvas.SetLeft(txt, 0);
                        if (a > 0) Canvas.SetTop(txt, Height - indent - i * 60); else Canvas.SetTop(txt, i * 60);
                        canvas.Children.Add(txt);
                    }
                }
            }
            else
            {
                if (H != 0)
                {
                    GraphName.Text = "Изменение температуры окатышей и газа по высоте слоя";
                    if (Tm0<Tg0) //Температура материала меньше температуры газа
                    {
                        a = 1;

                        for (int i = 0; i < Razm - 1; i++)
                        {
                            line = new Line() { X1 = x[i], Y1 = Height - indent - (a * T1[i]), X2 = x[i + 1], Y2 = Height - indent - (a * T1[i + 1]), Stroke = new SolidColorBrush(c), StrokeThickness = 4.0 };
                            canvas.Children.Add(line);

                            line = new Line() { X1 = x[i], Y1 = Height - indent -(a * T2[i]), X2 = x[i + 1], Y2 = Height - indent - (a * T2[i + 1]), Stroke = new SolidColorBrush(c2), StrokeThickness = 4.0 };
                            canvas.Children.Add(line);
                        }
                    }
                    else //Температура газа меньше температуры материала
                    {
                        for (int i = 0; i < Razm - 1; i++)
                        {
                            line = new Line() { X1 = x[i], Y1 = a * T1[i], X2 = x[i + 1], Y2 = a * T1[i + 1], Stroke = new SolidColorBrush(c), StrokeThickness = 4.0 };
                            canvas.Children.Add(line);

                            line = new Line() { X1 = x[i], Y1 = a * T2[i], X2 = x[i + 1], Y2 = a * T2[i + 1], Stroke = new SolidColorBrush(c2), StrokeThickness = 4.0 };
                            canvas.Children.Add(line);
                        }
                    }

                    for (int i = 0; i < 100; i++) // Ось Y
                    {
                        line = new Line() { X1 = 0, Y1 = i * 100 - indent, X2 = 2000, Y2 = i * 100 - indent, Stroke = new SolidColorBrush(s), StrokeThickness = 0.5 };
                        canvas.Children.Add(line);
                        var txt = new TextBlock() { Text = ToStr(i * a * 100) };
                        Canvas.SetLeft(txt, 0);
                        if (a > 0) Canvas.SetTop(txt, Height - indent - i * 100); else Canvas.SetTop(txt, i * 100);
                        canvas.Children.Add(txt);
                    }
                }
            }
        }
    }
}
