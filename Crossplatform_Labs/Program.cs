using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Crossplatform_Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("INPUT.TXT");
            int N = int.Parse(input[0].Split()[0]);
            int K = int.Parse(input[0].Split()[1]);
            int M = int.Parse(input[0].Split()[2]);

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
