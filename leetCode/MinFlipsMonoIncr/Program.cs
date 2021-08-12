using System;

namespace MinFlipsMonoIncr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Solution s = new Solution();
            int result;
            string inp;

            inp = "00110";
            result = s.MinFlipsMonoIncr(inp);
            Console.WriteLine(inp + ":" + result);

            inp = "00011000";
            result = s.MinFlipsMonoIncr(inp);
            Console.WriteLine(inp + ":" + result);

        }
    }

    public class Solution
    {


        int countZeros(string s)
        {
            int numzeros = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                {
                    numzeros++;
                }
            }
            return numzeros;

        }
        public int MinFlipsMonoIncr(string s)
        {
            int numzeroestoRight = countZeros(s);
            int BestAnswer = -1; //Update to max int
            int numFlips = 0;

            for (int bitIndex = 0; bitIndex < s.Length;  bitIndex++)
            {
                if (s[bitIndex] == '0')
                {
                    numzeroestoRight--;
                    continue;
                }


                //(s[bitIndex] is 1;

                //What if we flip all the bits to the right to 0. Would we get a better answer
                int possibleAnswer = numFlips + numzeroestoRight;
                if (BestAnswer == -1) { BestAnswer = possibleAnswer; }
                if (BestAnswer > possibleAnswer) { BestAnswer = possibleAnswer; }

                // Assume We flip it to zero
                numFlips++;

                if (numFlips > BestAnswer)
                {
                    //Already found the best answer. Trim your search
                    break;
                }

            }

            // This is the case where we converted the whole string to 0s - and that is better than previously found best answer
            if (numFlips < BestAnswer) { BestAnswer = numFlips; }

            if (BestAnswer == -1) { BestAnswer = 0; }

            return BestAnswer;

        }
    }
}
