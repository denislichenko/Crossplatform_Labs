using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LabRunner
{
    class Lab2
    {
        public static void Execute(string inputPath, string outputPath)
        {
            string input;
            try
            {
                input = File.ReadAllText(inputPath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Input file not found.");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while reading the input file: " + e.Message);
                return;
            }

            if (!int.TryParse(input.Trim(), out int N) || N <= 0)
            {
                Console.WriteLine("Invalid input. The input should be a valid positive integer.");
                return;
            }

            long[] dp = new long[N + 1];

            dp[1] = 1;

            for (int i = 2; i <= N; i++)
            {
                for (int j = 1; j <= 3 && i - j >= 1; j++)
                {
                    dp[i] += dp[i - j];
                }
            }

            long answer = dp[N];

            File.WriteAllText(outputPath, answer.ToString());
        }
    }
}
