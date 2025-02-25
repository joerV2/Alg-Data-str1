using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    internal static class Program
    {
        // Подключаем метод для открытия консоли
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            // Открываем консоль
 

            // Печатаем информацию в консоль сразу после её открытия
            Console.WriteLine("Консоль открыта и готова для вывода");

            // Запускаем Windows Forms приложение
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            // Печатаем информацию в консоль после открытия формы
            Console.WriteLine("Программа завершена");

            // Ожидаем ввода, чтобы консоль не закрылась сразу
            Console.ReadLine();
        }
    }
}
