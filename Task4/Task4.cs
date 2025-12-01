namespace Task2; 

class Task4
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Укажите путь до файла!");
            return 1;
        }

        var numbersFilePath = args[0];
        var fileLines = File.ReadAllLines(numbersFilePath);

        var numbersArray = Array.ConvertAll(fileLines, int.Parse);

        var averageValue = (int)Math.Floor(numbersArray.Average());

        var totalMoves = 0;

        foreach (var number in numbersArray)
        {
            if (number > averageValue)
            {
                totalMoves += number - averageValue;
            }

            if (number < averageValue)
            {
                totalMoves += averageValue - number;
            }
        }

        if (totalMoves > 20)
        {
            Console.WriteLine("20 ходов недостаточно для приведения всех элементов массива к одному числу");
            return 1;
        }

        Console.WriteLine(totalMoves);
        return 0;
    }
}
