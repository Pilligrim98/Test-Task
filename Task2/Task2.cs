using System.Numerics;

namespace Task4;

class Task2
{
    static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Неверно указаны аргументы >:(");
            return 1;
        }

        var circleCoordinatesFilePath = args[0];
        var pointsCoordinatesFilePath = args[1];

        var circle = GetCircle(circleCoordinatesFilePath);
        var points = GetPoints(pointsCoordinatesFilePath);

        GetPointPosition(circle, points);

        return 0;
    }

    private static Circle GetCircle(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        var splitted = lines[0].Split(' ');
        var circleCenterX = BigRational.Parse(splitted[0]);
        var circleCenterY = BigRational.Parse(splitted[1]);
        var splittedRadius = lines[1].Split(' ');
        var radiusX = BigRational.Parse(splittedRadius[0]);
        var radiusY = BigRational.Parse(splittedRadius[1]);

        var circleCenter = new BigRationalPoint(circleCenterX, circleCenterY);
        var circle = new Circle(circleCenter, radiusX, radiusY);
        return circle;
    }

    private static BigRationalPoint[] GetPoints(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var convertedPoints = new BigRationalPoint[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var splitted = lines[i].Split(' ');
            var pointX = BigRational.Parse(splitted[0]);
            var pointY = BigRational.Parse(splitted[1]);

            var point = new BigRationalPoint(pointX, pointY);
            convertedPoints[i] = point;
        }

        return convertedPoints;
    }

    static void GetPointPosition(Circle circle, BigRationalPoint[] convertedPoints)
    {
        foreach (var point in convertedPoints)
        {
            var x = point.X - circle.Center.X;
            var y = point.Y - circle.Center.Y;


            var ellipseValue = (x * x) / (circle.RadiusX * circle.RadiusX) +
                               (y * y) / (circle.RadiusY * circle.RadiusY);

            if (ellipseValue == 1)
            {
                Console.WriteLine(0);
            }
            else if (ellipseValue < 1)
            {
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(2);
            }
        }
    }

    class BigRationalPoint
    {
        private BigRational _x;
        private BigRational _y;

        public BigRationalPoint(BigRational x, BigRational y)
        {
            _x = x;
            _y = y;
        }


        public BigRational X
        {
            get { return _x; }
        }

        public BigRational Y
        {
            get { return _y; }
        }
    }

    class Circle
    {
        private BigRationalPoint _center;
        private BigRational _radiusX;
        private BigRational _radiusY;

        public Circle(BigRationalPoint center, BigRational radiusX, BigRational radiusY)
        {
            _center = center;
            _radiusX = radiusX;
            _radiusY = radiusY;
        }
    public BigRationalPoint Center
    {
        get { return _center; }
    }

    public BigRational RadiusX
    {
        get { return _radiusX; }
    }

    public BigRational RadiusY
    {
        get { return _radiusY; }
    }
    }
}