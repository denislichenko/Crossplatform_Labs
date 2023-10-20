using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input;
            try
            {
                input = File.ReadAllLines("INPUT.TXT");
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

            if (input.Length < 1)
            {
                Console.WriteLine("Input file is empty.");
                return;
            }

            string[] firstLine = input[0].Split();

            if (firstLine.Length != 3)
            {
                Console.WriteLine("The first line of input should contain three integers separated by spaces (N, K, and M).");
                return;
            }

            int N = 0, K = 0, M = 0;

            if (!int.TryParse(firstLine[0], out N) || !int.TryParse(firstLine[1], out K) || !int.TryParse(firstLine[2], out M))
            {
                Console.WriteLine("Invalid integer format in the first line of input.");
                return;
            }

            if (N <= 0 || K <= 0 || M <= 0)
            {
                Console.WriteLine("N, K, and M should be positive integers.");
                return;
            }

            if (input.Length < 1 + M)
            {
                Console.WriteLine("Not enough lines in the input for the specified number of edges (M).");
                return;
            }

            List<int>[] adjacencyList = new List<int>[N + 1];
            for (int i = 1; i <= N; i++)
            {
                adjacencyList[i] = new List<int>();
            }

            for (int i = 1; i <= M; i++)
            {
                string[] line = input[i].Split();
                int u = int.Parse(line[0]);
                int v = int.Parse(line[1]);
                adjacencyList[u].Add(v);
                adjacencyList[v].Add(u);
            }

            List<int> firstTeam = new List<int>();
            List<int> secondTeam = new List<int>();
            List<int> remainingParticipants = Enumerable.Range(1, N).ToList();

            for (int i = 0; i < K; i++)
            {
                int bestParticipant = -1;
                int bestScore = -1;

                foreach (int participant in remainingParticipants)
                {
                    int score = 0;
                    foreach (int friend in adjacencyList[participant])
                    {
                        if (firstTeam.Contains(friend))
                        {
                            score++;
                        }
                    }

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestParticipant = participant;
                    }
                }

                if (bestParticipant != -1)
                {
                    firstTeam.Add(bestParticipant);
                    remainingParticipants.Remove(bestParticipant);
                }
            }

            secondTeam.AddRange(remainingParticipants);

            string result = string.Join(" ", firstTeam);
            File.WriteAllText("OUTPUT.TXT", result);
        }
    }
}
