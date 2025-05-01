using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BrokenApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new AppService();
            app.DoWork();

            // Потенциальная утечка: логгирование чувствительных данных
            Console.WriteLine("User password is: 123456");

            string unusedVariable = "This is never used";

            // Потенциальная уязвимость: считывание файла без валидации пути
            string path = Console.ReadLine();
            string content = File.ReadAllText(path);
            Console.WriteLine(content);
        }
    }

    public class AppService
    {
        private List<string> _items = new List<string>();

        public void DoWork()
        {
            for (int i = 0; i <= 10; i++)
            {
                _items.Add("Item " + i);
            }

            // Потенциальная ошибка: выход за пределы массива
            Console.WriteLine(_items[15]);

            try
            {
                DangerousOperation(null);
            }
            catch (Exception ex)
            {
                // Плохая практика: глушим исключение
            }

            Task.Run(() => LongRunningProcess());
        }

        private void DangerousOperation(string input)
        {
            Console.WriteLine(input.ToUpper()); // возможен NullReferenceException
        }

        private async Task LongRunningProcess()
        {
            // Имитация утечки ресурсов
            while (true)
            {
                Thread.Sleep(1000); // не await, блокировка потока
            }
        }
    }

    class UselessClass
    {
        public void DoNothing()
        {
            int a = 5;
            int b = 10;
            int result = a * b; // переменная не используется
        }
    }

    // Устаревший стиль, неиспользуемый интерфейс
    interface IUnused
    {
        void NotImplemented();
    }
}

