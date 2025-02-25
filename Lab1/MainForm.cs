using ProjectVector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace Lab2
{
    public partial class MainForm : Form
    {
        private List<int> operationsWithoutCapacity = new List<int>();
        private List<int> operationsWithCapacity = new List<int>();
        private List<int> operationsLinkedList = new List<int>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void PlotGraph(List<int> withoutCapacity, List<int> withCapacity, List<int> linkedList, int initialCapacity, int elementsCount)
        {
            // Очищаем старые данные и легенду
            chart1.Series.Clear();
            chart1.Legends.Clear();

            // Добавляем легенду с текстом
            var legend = new Legend("MainLegend") { Docking = Docking.Bottom };
            chart1.Legends.Add(legend);

            // Серия "Без ёмкости"
            Series series1 = new Series($"Без ёмкости\nКол-во операций: {withoutCapacity.Last()}")
            {
                ChartType = SeriesChartType.Line, // Тип графика - линия
                Color = Color.Red,               // Цвет линии
                MarkerStyle = MarkerStyle.Circle // Стиль маркера
            };

            // Точки графика "Без ёмкости"
            for (int i = 0; i < withoutCapacity.Count; i++)
            {
                series1.Points.AddXY(i + 1, withoutCapacity[i]); // X = количество элементов, Y = количество операций
            }

            // Серия "С ёмкостью"
            Series series2 = new Series($"С ёмкостью\nКол-во операций: {withCapacity.Last()}")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Blue,
                MarkerStyle = MarkerStyle.Square
            };

            // Точки графика "С ёмкостью"
            for (int i = 0; i < withCapacity.Count; i++)
            {
                series2.Points.AddXY(i + 1, withCapacity[i]);
            }

            // Серия "Односвязаный список"
            Series series3 = new Series($"Односвязный список\nКол-во операций: {linkedList.Last()}")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Green,
                MarkerStyle = MarkerStyle.Triangle
            };

            // Точки графика "MyLinkedList"
            for (int i = 0; i < linkedList.Count; i++)
            {
                series3.Points.AddXY(i + 1, linkedList[i]);
            }

            // Вывод серий на график
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);
            chart1.Series.Add(series3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int elementsCount))
            {
                MessageBox.Show("Кол-во элементов не может быть пустым");
                return;
            }
            if (!int.TryParse(textBox2.Text, out int initialCapacity))
            {
                MessageBox.Show("Ёмкость не может быть пустой");
                return;
            }

            // Список на основе MyList без начальной ёмкости
            MyList<int> listWithoutCapacity = new MyList<int>();
            operationsWithoutCapacity.Clear();  // Очищаем старые данные

            // Вставляем элементы в список
            for (int i = 0; i < elementsCount; i++)
            {
                listWithoutCapacity.Insert(listWithoutCapacity.Count, i); // Вставка
                operationsWithoutCapacity.Add(listWithoutCapacity.OperatoinCount); // Счётчик операций
            }

            // Список на основе MyList с начальной ёмкостью
            MyList<int> listWithCapacity = new MyList<int>(initialCapacity);
            operationsWithCapacity.Clear();  // Очищаем старые данные

            for (int i = 0; i < elementsCount; i++)
            {
                listWithCapacity.Insert(listWithCapacity.Count, i);
                operationsWithCapacity.Add(listWithCapacity.OperatoinCount);
            }

            // Список на основе MyLinkedList
            MyLinkedList<int> linkedList = new MyLinkedList<int>();
            operationsLinkedList.Clear();  // Очищаем старые данные

            for (int i = 0; i < elementsCount; i++)
            {
                linkedList.Insert(0, i);
                operationsLinkedList.Add(linkedList.CountOperations);
            }

            // Строим график с полученными данными
            PlotGraph(operationsWithoutCapacity, operationsWithCapacity, operationsLinkedList, initialCapacity, elementsCount);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("graph_data.csv"))
            {
                // Находим наибольший размер для определения количества строк
                int maxCount = Math.Max(Math.Max(operationsWithoutCapacity.Count, operationsWithCapacity.Count), operationsLinkedList.Count);

                // Записываем данные по каждой точке
                for (int i = 0; i < maxCount; i++)
                {
                    // Получаем значения для каждой серии (если они существуют)
                    string without = (i < operationsWithoutCapacity.Count) ? operationsWithoutCapacity[i].ToString() : "0";
                    string with = (i < operationsWithCapacity.Count) ? operationsWithCapacity[i].ToString() : "0";
                    string linked = (i < operationsLinkedList.Count) ? operationsLinkedList[i].ToString() : "0";

                    // Записываем строку в CSV файл
                    writer.WriteLine($"{i + 1};{without};{with};{linked}");
                }
            }

            MessageBox.Show("Данные сохранены в CSV файл!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
