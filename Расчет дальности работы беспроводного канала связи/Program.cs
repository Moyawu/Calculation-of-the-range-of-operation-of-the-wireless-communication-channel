using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите, что вы хотите рассчитать:");
            Console.WriteLine("1 - Потери в свободном пространстве (FSL)");
            Console.WriteLine("2 - Искомое расстояние (D)");
            Console.WriteLine("3 - Суммарное усиление системы передачи (YдБ)");
            Console.WriteLine("0 - Выход");
            Console.Write("Введите номер выбора: ");

            if (!int.TryParse(Console.ReadLine(), out int calculationChoice))
            {
                Console.WriteLine("Ошибка: Неправильный ввод. Пожалуйста, введите число 1, 2, 3 или 0.");
                continue; // Возврат к началу цикла
            }

            if (calculationChoice == 0)
            {
                break; // Выход из программы
            }

            switch (calculationChoice)
            {
                case 1:
                    CalculateFSL();
                    break;
                case 2:
                    CalculateDistance();
                    break;
                case 3:
                    CalculateYdB();
                    break;
                default:
                    Console.WriteLine("Неправильный выбор.");
                    break;
            }
        }

        // Ожидание ввода перед закрытием программы
        Console.WriteLine("Нажмите Enter, чтобы выйти...");
        Console.ReadLine();
    }

    static void CalculateFSL()
    {
        Console.WriteLine("Выберите метод расчета потерь в свободном пространстве:");
        Console.WriteLine("1 - По формуле FSL = YдБ - SOM");
        Console.WriteLine("2 - По формуле FSL = 33 + 20 * (log10(frequency) + log10(distance))");
        Console.Write("Введите номер выбора: ");

        if (!int.TryParse(Console.ReadLine(), out int methodChoice))
        {
            Console.WriteLine("Ошибка: Неправильный ввод.");
            return;
        }

        if (methodChoice == 1)
        {
            Console.Write("Введите суммарное усиление системы передачи (YдБ): ");
            if (!double.TryParse(Console.ReadLine(), out double YdB))
            {
                Console.WriteLine("Ошибка: Неправильный ввод для YдБ.");
                return;
            }

            double SOM = 10; // Задаем SOM равным 10 дБ

            // Вычисление FSL
            double FSL = YdB - SOM;

            // Вывод результата
            Console.WriteLine("Результат:");
            Console.WriteLine($"Потери в свободном пространстве (FSL) по формуле (3): {FSL:F2} дБ");
        }
        else if (methodChoice == 2)
        {
            double frequency = GetFrequency();
            double distance = GetDistance();

            double fsl = CalculateFreeSpaceLoss(frequency, distance);

            // Вывод результата
            Console.WriteLine("Результат:");
            Console.WriteLine($"Частота: {frequency / 1000000} МГц");
            Console.WriteLine($"Расстояние: {distance} км");
            Console.WriteLine($"Потери в свободном пространстве (FSL) по формуле (1): {fsl:F2} дБ");
        }
        else
        {
            Console.WriteLine("Неправильный выбор.");
        }
    }

    static void CalculateDistance()
    {
        double frequency = GetFrequency();

        Console.Write("Введите потери в свободном пространстве (FSL) в дБ: ");
        if (!double.TryParse(Console.ReadLine(), out double fsl))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для FSL.");
            return;
        }

        double distance = CalculateDistance(frequency, fsl);

        // Вывод результата
        Console.WriteLine("Результат:");
        Console.WriteLine($"Частота: {frequency / 1000000} МГц");
        Console.WriteLine($"Потери в свободном пространстве (FSL): {fsl:F2} дБ");
        Console.WriteLine($"Искомое расстояние (D): {distance:F2} км");
    }

    static void CalculateYdB()
    {
        Console.Write("Введите мощность передатчика (Pt,дБм): ");
        if (!double.TryParse(Console.ReadLine(), out double Pt))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для Pt.");
            return;
        }

        Console.Write("Введите коэффициент усиления передающей антенны (Gt,дБи): ");
        if (!double.TryParse(Console.ReadLine(), out double Gt))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для Gt.");
            return;
        }

        Console.Write("Введите коэффициент усиления приемной антенны (Gr,дБи): ");
        if (!double.TryParse(Console.ReadLine(), out double Gr))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для Gr.");
            return;
        }

        Console.Write("Введите чувствительность приемника на данной скорости (Pmin,дБм): ");
        if (!double.TryParse(Console.ReadLine(), out double Pmin))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для Pmin.");
            return;
        }

        Console.Write("Введите потери сигнала в коаксиальном кабеле и разъемах передающего тракта (Lt,дБ): ");
        if (!double.TryParse(Console.ReadLine(), out double Lt))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для Lt.");
            return;
        }

        Console.Write("Введите потери сигнала в коаксиальном кабеле и разъемах приемного тракта (Lr,дБ): ");
        if (!double.TryParse(Console.ReadLine(), out double Lr))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для Lr.");
            return;
        }

        // Вычисление YдБ
        double YdB = Pt + Gt + Gr - Pmin - Lt - Lr;

        // Вывод результата
        Console.WriteLine("Результат:");
        Console.WriteLine($"Суммарное усиление системы передачи (YдБ): {YdB:F2} дБ");
    }

    static double GetFrequency()
    {
        Console.WriteLine("Выберите единицы измерения для частоты:");
        Console.WriteLine("1 - Гц");
        Console.WriteLine("2 - кГц");
        Console.WriteLine("3 - МГц");
        Console.WriteLine("4 - ГГц");
        Console.WriteLine("5 - Т Гц");
        Console.Write("Введите номер выбора: ");

        if (!int.TryParse(Console.ReadLine(), out int frequencyUnit))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для единиц измерения частоты.");
            return 0;
        }

        Console.Write("Введите центральную частоту канала: ");
        if (!double.TryParse(Console.ReadLine(), out double frequencyValue))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для частоты.");
            return 0;
        }

        switch (frequencyUnit)
        {
            case 1:
                return frequencyValue; // Гц
            case 2:
                return frequencyValue * 1000; // кГц -> Гц
            case 3:
                return frequencyValue * 1000000; // МГц -> Гц
            case 4:
                return frequencyValue * 1000000000; // ГГц -> Гц
            case 5:
                return frequencyValue * 1000000000000; // ТГц -> Гц
            default:
                throw new ArgumentException("Неправильный выбор единиц измерения для частоты");
        }
    }

    static double GetDistance()
    {
        Console.Write("Введите расстояние в километрах: ");
        if (!double.TryParse(Console.ReadLine(), out double distance))
        {
            Console.WriteLine("Ошибка: Неправильный ввод для расстояния.");
            return 0;
        }

        return distance;
    }

    static double CalculateFreeSpaceLoss(double frequency, double distance)
    {
        return 33 + 20 * (Math.Log10(frequency) + Math.Log10(distance));
    }

    static double CalculateDistance(double frequency, double fsl)
    {
        return Math.Pow(10, (fsl - 33) / 20) / (4 * Math.PI * frequency);
    }
}