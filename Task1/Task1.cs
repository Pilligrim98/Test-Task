using System.Text;

namespace Task1;

class Task1
{
    static void Main(string[] args)
    {
        if (args.Length < 4 ||
            string.IsNullOrWhiteSpace(args[0]) ||
            string.IsNullOrWhiteSpace(args[1]) ||
            string.IsNullOrWhiteSpace(args[2]) ||
            string.IsNullOrWhiteSpace(args[3]))
        {
            throw new ArgumentException("Не заполнены аргументы командной строки >:(");
        }
        var maxValue2 = int.Parse(args[2]);
        var interval2 = int.Parse(args[3]);
        var maxValue = int.Parse(args[0]);
        var interval = int.Parse(args[1]);

        var path1 = GetCircularPath(maxValue, interval);
        var path2 = GetCircularPath(maxValue2, interval2);

        Console.Write(path1 + path2);
        Console.ReadLine();
    }

    static string GetCircularPath(int maxValue, int interval)
    {
        var myArray = new int[maxValue];
        for (var i = 0; i < maxValue; i++)
        {
            myArray[i] = i + 1;
        }
        
        var resultIntegers = new List<int>();
        var index = 0;
        do
        {
            resultIntegers.Add(myArray[index]);
            index += (interval - 1);
            index = index % myArray.Length;
        } while (myArray[index] != myArray[0]);
        
        var result = new StringBuilder();
        foreach (var integerValue in resultIntegers)
        {
            result.Append(integerValue);
        }
        return result.ToString();
    }
}