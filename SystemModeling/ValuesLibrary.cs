using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemModeling
{
    class ValuesLibrary
    {
        public static double H { get; set; } //Высота слоя
        public static double Tm0 { get; set; } //Начальная температура материала
        public static double Tg0 { get; set; } //Начальная температура воздуха(газа)
        public static double Wg { get; set; } //Скорость воздуха(газа) на свободное сечение шахты
        public static double Cg { get; set; } //Теплоемкость воздуха(газа)
        public static double Rate { get; set; } //Рассход окатышей
        public static double Cok { get; set; } //Теплоемкость окатышей
        public static double AlfaV { get; set; } //Объемный коэффициент теплоотдачи
        public static double D { get; set; } //Диаметр аппарата

        public static double M { get; set; } //Отношение теплоемкостей потоков
        public static double Y0 { get; set; } //Полная относительная высота слоя
        public static double E1 { get; set; } //1-m*exp((m-1)*y0/m)
        public static int Switch { get; set; }

        public static int Razm;
        public static double[] Y;
        public static double[] E2;
        public static double[] E3;
        public static double[] Teta1;
        public static double[] Teta2;
        public static double[] T1;
        public static double[] T2;
        public static double[] T;
        /// <summary>
        /// Метод для рассчета отношения теплоемкостей потоков
        /// </summary>
        /// <param name="cok">Теплоемкость окатышей</param>
        /// <param name="rate">Рассход окатышей</param>
        /// <param name="cg">Теплоемкость воздуха(газа)</param>
        /// <param name="wg">Скорость воздуха(газа) на свободное сечение шахты</param>
        /// <param name="d">Диаметр аппарата</param>
        /// <returns></returns>
        public static double FindM(double cok, double rate, double cg, double wg, double d)
        {
            return Math.Round((cok * rate) / (wg * (Math.PI * Math.Pow(d / 2, 2) * cg)), 3);
        }
        /// <summary>
        /// Метод для рассчета полно1 относительно1 высоты слоя
        /// </summary>
        /// <param name="alfav">Объемный коэффициент теплоотдачи</param>
        /// <param name="h">Высота слоя</param>
        /// <param name="wg">Скорость воздуха(газа) на свободное сечение шахты</param>
        /// <param name="cg">Теплоемкость воздуха(газа)</param>
        /// <returns></returns>
        public static double FindY0(double alfav, double h, double wg, double cg)
        {
            return Math.Round((alfav * h) / (wg * cg * 1000), 3);
        }
        /// <summary>
        /// Метод для рассчета 1-m*exp((m-1)*y0/m)
        /// </summary>
        /// <param name="m">Отношение теплоемкостей потоков</param>
        /// <param name="y0">Полная относительная высота слоя</param>
        /// <returns></returns>
        public static double FindE1(double m, double y0)
        {
            return Math.Round((1 - m * Math.Exp(((m - 1) * y0) / m)), 3);
        }
        /// <summary>
        /// Метод для рассчета Y
        /// </summary>
        /// <param name="alfav">Объемный коэффициент теплоотдачи</param>
        /// <param name="x">Координата по оси x</param>
        /// <param name="wg">Скорость воздуха(газа) на свободное сечение шахты</param>
        /// <param name="cg">Теплоемкость воздуха(газа)</param>
        /// <returns></returns>
        public static double FindY(double alfav, double x, double wg, double cg)
        {
            return Math.Round((alfav * x / 1000) / (wg * cg), 3);
        }
        /// <summary>
        /// Метод для рассчета 1-exp((m-1)*y/m)
        /// </summary>
        /// <param name="m">Отношение теплоемкостей потоков</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        public static double FindE2(double m, double y)
        {
            return Math.Round(1 - Math.Exp((m - 1) * y / m), 3);
        }
        /// <summary>
        /// Метод для рассчета 1-m*exp((m-1)*y/m)
        /// </summary>
        /// <param name="m">Отношение теплоемкостей потоков</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        public static double FindE3(double m, double y)
        {
            return Math.Round(1 - m * Math.Exp(((m - 1) * y) / m), 3);
        }
        /// <summary>
        /// Метод для рассчета тета1
        /// </summary>
        /// <param name="e2">1-exp((m-1)*y/m)</param>
        /// <param name="e1">1-m*exp((m-1)*y0/m)</param>
        /// <returns></returns>
        public static double FindTeta1(double e2, double e1)
        {
            return Math.Round(e2 / e1, 3);
        }
        /// <summary>
        /// Метод для рассчета тета2
        /// </summary>
        /// <param name="e3">1-m*exp((m-1)*y/m)</param>
        /// <param name="e1">1-m*exp((m-1)*y0/m)</param>
        /// <returns></returns>
        public static double FindTeta2(double e3, double e1)
        {
            return Math.Round(e3 / e1, 3);
        }
        /// <summary>
        /// Метод для рассчета температуры
        /// </summary>
        /// <param name="tm0">Начальная температура материала</param>
        /// <param name="tg0">Начальная температура воздуха(газа)</param>
        /// <param name="teta">тета</param>
        /// <returns></returns>
        public static double FindT(double tm0, double tg0, double teta)
        {
            return Math.Round(tm0 + (tg0 - tm0) * teta, 3);
        }
        public static double ToDouble(string s)
        {
            return Convert.ToDouble(s, System.Globalization.CultureInfo.CurrentCulture);
        }
        public static string ToStr(double d)
        {
            return Convert.ToString(d, System.Globalization.CultureInfo.CurrentCulture);
        }
        public static void FindXY() //Рассчет значений для таблицы
        {
            double x = 0;
            Razm = Convert.ToInt32(H * 2 + 1);
            Y = new double [Razm];
            E2 = new double[Razm];
            E3 = new double[Razm];
            Teta1 = new double[Razm];
            Teta2 = new double[Razm];
            T1 = new double[Razm];
            T2 = new double[Razm];
            T = new double[Razm];

            for (int i = 0; i < Razm; i++)
            {
                Y[i] = FindY(AlfaV, x, Wg, Cg);
                E2[i] = FindE2(M, Y[i]);
                E3[i] = FindE3(M, Y[i]);
                Teta1[i] = FindTeta1(E2[i], E1);
                Teta2[i] = FindTeta2(E3[i], E1);
                T1[i] = FindT(Tm0, Tg0, Teta1[i]);
                T2[i] = FindT(Tm0, Tg0, Teta2[i]);
                T[i] = T1[i] - T2[i];
                x += 0.5;
            }
        }
    }
    class MyTable //Заполнение тыблицы
    {
        public MyTable(double x, double y, double e2, double e3, double teta1, double teta2, double t1, double t2, double t)
        {
            X = x;
            Y = y;
            E2 = e2;
            E3 = e3;
            Teta1 = teta1;
            Teta2 = teta2;
            T1 = t1;
            T2 = t2;
            T = t;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public double E2 { get; set; }
        public double E3 { get; set; }
        public double Teta1 { get; set; }
        public double Teta2 { get; set; }
        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T { get; set; }
    }
}
