using System;

namespace MatrixRotation
{
    class Program
    {

        static void TestCase(int Length)
        {

            // Test cases                      
            Console.WriteLine("Hello World - Matrix Rotate - Length " +  Length);
            Matrix m = new Matrix(Length);
            Console.WriteLine("Original Matrix");
            m.PrintMe();
            Console.WriteLine("Rotating Clockwise");
            m.RotateClockwise();
            m.PrintMe();
        }

        static void Main(string[] args)
        {
            // Test cases                      
            TestCase(1);
            TestCase(2);
            TestCase(3);
            TestCase(4);
            TestCase(5);
            TestCase(8);
            TestCase(9);
        }
    }


    public class Matrix
    {
        // 2D Array representing the Square Matrix
        int[,] Values;

        // Length (X or Y) of Square Matrix
        int Length;

        public Matrix(int Length)
        {
            this.Length = Length;

            // Temp Hack: Assign some random values to the matrix. 
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

        // Given a point i,j in the matrix, find the next point 
        // if the matrix is rotated clockwise by 1 step
        public void GetNextIJ(ref int i, ref int j)
        {

            //  Example Matrix  
            //    . . . . . . . .            
            //    . B B B B B C .          
            //    . A . . . . C .
            //    . A . . . . C .
            //    . A . . . . C .
            //    . A . . . . C .
            //    . A D D D D D .
            //    . . . . . . . .             


            // Check for Region A, in that case next point will be directly above it      
            if ((i <= Length/2 - 1) && (j >= i) && ((i + j) < (Length - 1))) { 
                j++; 
                return; 
            }

            // Check for Region C, in that case next point will be directly below it      
            if ((i > Length / 2 - 1) && (j <= i) && ((i + j) >= Length)) { 
                j--; 
                return; 
            }

            // Check for Region B in that case next point will be directly to the right of it      
            // Note: This check leverages teh fact that the point does not lie in A, C above
            if ((j <= (Length - 1) / 2) && (i > j)) { 
                i--; 
                return; 
            }

            // Check for Region D in that case next point will be directly to the left of it      
            // Note: This check leverages teh fact that the point does not lie in A, C above
            if ((j > (Length - 1) / 2) && (i < j)) { 
                i++; 
                return; 
            }

            // I should never reach here. 
            Console.WriteLine("BAD I J GetNextIJ LOGIC ERROR i,j,Length" + i + "," + j + "," + Length);

        }

        public void RotateClockwise()
        {
            // A loop to rotate each spiral loop independently
            for(int spiral=0; spiral <= (Length/2 - 1); spiral++)
            {
                //This is each Spriral. We will start with spiral,spiral for each spiral and rotate that spiral
                int starti = spiral;
                int startj = spiral;
                int curri, currj;

                int lastIJValueBuffer = Values[starti, startj];

                curri = starti;
                currj = startj;
                GetNextIJ(ref curri, ref currj);

                while (curri != starti || currj != startj)
                {
                    // Save off the value of current point, and 
                    // put the value of the last point in the current point
                    int tmp = Values[curri, currj];
                    Values[curri, currj] = lastIJValueBuffer;
                    lastIJValueBuffer = tmp;

                    GetNextIJ(ref curri, ref currj);
                }

                // Since we broke of the loop before getting a chance to update
                // the last point, do that now. 
                Values[curri, currj] = lastIJValueBuffer;
            }
        }
    }

}
