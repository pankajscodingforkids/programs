using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Snake snake = new Snake(10, 10, 20, 20);
            snake.PrintAsGrid();

            //snake.Grow(Direction.Right);
            //snake.PrintAsGrid();

            //snake.Move(Direction.Right);
            //snake.PrintAsGrid();

            //snake.Move(Direction.Left);
            //snake.PrintAsGrid();

            
            for (int i = 0; i <= 100; i++)
            {
                Console.Clear();
                Random r = new Random();
                Direction d = (Direction)r.Next(0, 4);
                if (i%4 != 0)
                {
                    Console.Write("Move: " + d + " - " + (int)d);
                    snake.Move(d);
                } 
                else
                {
                    Console.Write("Grow: " + d + " - " + (int)d);
                    snake.Grow(d);
                }
                Console.WriteLine();
                snake.PrintAsGrid();
                Thread.Sleep(2000);
            }

        }
    }


    // Define an extension method in a non-nested static class.
    //public static class Extensions
    //{
    //    public static Direction GetRandomDirection()
    //    {
    //        Array values = Enum.GetValues(typeof(Direction));
    //        Random random = new Random();
    //        Direction d = (Direction)values.GetValue(random.Next(values.Length));
    //        return d;
    //    }
    //}


    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


    class Point : ICloneable
    {
        int CoordinateX;
        int CoordinateY;
        int GridSizeX;
        int GridSizeY;

        public Point(int CoordinateX, int CoordinateY, int GridSizeX, int GridSizeY)
        {
            this.CoordinateX = CoordinateX;
            this.CoordinateY = CoordinateY;
            this.GridSizeX = GridSizeX;
            this.GridSizeY = GridSizeY;

            if (!IsValid())
            {
                // Constructors cannot fail. We can throw an excpetion here.
                // but for now just creating a log. 

            }

        }

        public bool IsSame(Point P)
        {
            if (CoordinateX != P.CoordinateX) { return false; }
            if (CoordinateY != P.CoordinateY) { return false; }
            if (GridSizeX   != P.GridSizeX)   { return false; }
            if (GridSizeY   != P.GridSizeY)   { return false; }
            return true;
        }

        public bool IsSame(int X, int Y)
        {
            if (CoordinateX != X) { return false; }
            if (CoordinateY != Y) { return false; }
            return true;
        }

        bool IsValid(int X, int Y)
        {

            if (GridSizeX <= 0 ||
                 GridSizeY <= 0)
            {
                // Bad Grid Size
                return false;
            }
            if (X < 0 ||
                 Y < 0 ||
                 X >= GridSizeX ||
                 Y >= GridSizeY)
            {
                // Point is Off Grid
                return false;
            }
            return true;
        }


        bool IsValid()
        {
            return IsValid(CoordinateX, CoordinateY);
        }

        public bool Move(Direction Dir)
        {
            int x = CoordinateX;
            int y = CoordinateY;

            switch (Dir)
            {
                case Direction.Up:
                    y++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                default:
                    x = -1;
                    y = -1;
                    break;
            }

            if (!IsValid(x, y))
            {
                return false;
            }
            CoordinateX = x;
            CoordinateY = y;

            return true;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }


    class Snake
    {
        List<Point> Points;

        int GridSizeX;
        int GridSizeY;

        // Constructor for Snake of length 1
        public Snake(int InitialX, int InitialY, int GridSizeX, int GridSizeY)
        {
            Points = new List<Point>();
            this.GridSizeX = GridSizeX;
            this.GridSizeY = GridSizeY;

            Point point = new Point(InitialX, InitialY, GridSizeX, GridSizeY);
            Points.Add(point);
        }

        bool IsNull()
        {
            // If the snake is Null then move operation must fail 
            if (Points == null)
            {
                return true;
            }

            if (Points.Count == 0)
            {
                return true;
            }

            return false;
        }


        bool IsColliding(Point TestPoint)
        {
            foreach (Point p in Points)
            {
                if (p.IsSame(TestPoint)) { return true; }
            }
            return false;
        }

        bool IsColliding(int X, int Y)
        {
            foreach (Point p in Points)
            {
                if (p.IsSame(X, Y)) { return true; }
            }
            return false;
        }


        //NOTE: After I impleted Grow, i can just use Grow in my Move()
        //But for now i keep them separate. 
        public bool Move(Direction Dir)
        {

            if (Grow(Dir) == false)
            {
                return false;
            }

            Points.RemoveAt(Points.Count - 1);
            
            return true;

        }

        public bool Grow(Direction Dir)
        {

            // If the snake is Null then move operation must fail 
            if (IsNull())
            {
                Console.WriteLine("Failed due to NLL");
                return false;
            }

            Point newHead = (Point) Points[0].Clone();

            if (newHead.Move(Dir) == false)
            {
                Console.Write("Failed");
                return false;
            }

            if (IsColliding(newHead))
            {
                Console.Write("Collision");
                return false;
            }

            Points.Insert(0, newHead);
            return true;

        }

        public void PrintMe()
        {
            Console.WriteLine("Head-->");
            foreach (Point p in Points)
            {
                //Console.WriteLine(")
            }
        }

        public void PrintAsGrid()
        {
            Console.Write('|');
            for (int x = 0; x < GridSizeX; x++)
            {
                Console.Write('-');
            }
            Console.WriteLine('|');
            char c = 'X';
            for (int y = GridSizeY - 1; y >= 0; y--)
            {
                Console.Write('|');
                for (int x = 0; x < GridSizeX; x++)
                {
                    if (Points[0].IsSame(x,y)) { Console.Write('X'); }
                    else if (IsColliding(x,y)) { Console.Write ('.');  }
                    else                       { Console.Write (' ');   }
                }
                Console.WriteLine('|');
            }
            Console.Write('|');
            for (int x = 0; x < GridSizeX; x++)
            {
                Console.Write('-');
            }
            Console.WriteLine('|');
            Console.WriteLine();

        }
    }




}
