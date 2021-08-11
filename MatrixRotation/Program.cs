using System;

namespace MatrixRotation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World - Matrix Rotate!");
            Matrix m = new Matrix(5);
            m.PrintMe();
            Console.WriteLine("Rotating Clockwise");
            m.RotateClockwise();
            m.PrintMe();
        }
    }

    public class Matrix
    {
        int[,] Values;
        int Length;

        public Matrix(int Length)
        {
            this.Length = Length;

            int count = 100;
            Values = new int[Length,Length];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Values[i, j] = count++;
                }
            }
            
        }

        public void PrintMe()
        {
            //Console.WriteLine(Values);
            for (int j = Length - 1; j >= 0; j--)
            {
                for (int i = 0; i < Length; i++)
                {
                    Console.Write(Values[i, j] + "   ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void GetNextIJ(ref int i, ref int j)
        {
            if ((i <= Length/2 - 1) && (j >= i) && ((i + j) < (Length - 1))) { j++; return; }
            if ((i > Length / 2 - 1) && (j <= i) && ((i + j) >= Length)) { j--; return; }

            if ((j <= (Length - 1) / 2) && (i > j)) { i--; return; }
            if ((j > (Length - 1) / 2) && (i < j)) { i++; return; }

            Console.WriteLine("BAD I J GetNextIJ LOGIC ERROR i,j,Length" + i + "," + j + "," + Length);

        }

        public void RotateClockwise()
        {
            // A loop to rotate each spiral loop independently
            for(int spiral=0; spiral <= (Length/2 - 1); spiral++)
            {
                //This is each Spriral. We will start with spiral,spiral for each spiral and roatate that spiral
                int starti = spiral;
                int startj = spiral;
                int curri, currj;

                int lastIJValueBuffer = Values[starti, startj];

                curri = starti;
                currj = startj;
                //Console.WriteLine("Curri, Currj, Length : " + curri + "," + currj + "," + Length);
                GetNextIJ(ref curri, ref currj);
                //Console.WriteLine("Nexti, Nextj, Length : " + curri + "," + currj + "," + Length);

                while (curri != starti || currj != startj)
                {
                    int tmp = Values[curri, currj];
                    Values[curri, currj] = lastIJValueBuffer;
                    lastIJValueBuffer = tmp;

                    //Console.WriteLine("Curri, Currj, Length : " + curri + "," + currj + "," + Length);
                    GetNextIJ(ref curri, ref currj);
                    //Console.WriteLine("Nexti, Nextj, Length : " + curri + "," + currj + "," + Length);
                }

                Values[curri, currj] = lastIJValueBuffer;
            }
        }
    }

}
