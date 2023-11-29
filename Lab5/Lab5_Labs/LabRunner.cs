using System.ComponentModel;
using System.Text;

namespace Lab5_Labs
{
    public class LabRunner
    {
        public static List<int>? ExecuteLab1(string input)
        {
            string[] lines = input.Split(Environment.NewLine);

            if (lines.Length < 1)
            {
                Console.WriteLine("Input file is empty.");
                return null;
            }

            string[] firstLine = lines[0].Split();

            if (firstLine.Length != 3)
            {
                Console.WriteLine("The first line of input should contain three integers separated by spaces (N, K, and M).");
                return null;
            }

            int N = 0, K = 0, M = 0;

            if (!int.TryParse(firstLine[0], out N) || !int.TryParse(firstLine[1], out K) || !int.TryParse(firstLine[2], out M))
            {
                Console.WriteLine("Invalid integer format in the first line of input.");
                return null;
            }

            if (N <= 0 || K <= 0 || M <= 0)
            {
                Console.WriteLine("N, K, and M should be positive integers.");
                return null;
            }

            if (lines.Length < 1 + M)
            {
                Console.WriteLine("Not enough lines in the input for the specified number of edges (M).");
                return null;
            }

            List<int>[] adjacencyList = new List<int>[N + 1];
            for (int i = 1; i <= N; i++)
            {
                adjacencyList[i] = new List<int>();
            }

            for (int i = 1; i <= M; i++)
            {
                string[] line = lines[i].Split();
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

            return firstTeam;
        }


        public static long ExecuteLab2(string input)
        {
            if (!int.TryParse(input.Trim(), out int N) || N <= 0)
            {
                Console.WriteLine("Invalid input. The input should be a valid positive integer.");
                return 0;
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
            return answer;
        }

        public static string ExecuteLab3(string inputText)
        {
            int n, m;
            char[][] board;
            bool[][] visited;
            char[] dirs = { 'R', 'L', 'U', 'D' };
            int[][] d = { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };

            StringBuilder output = new StringBuilder();

            string[] lines = inputText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string[] nm = lines[0].Split();
            if (nm.Length != 2 || !int.TryParse(nm[0], out n) || !int.TryParse(nm[1], out m))
            {
                output.AppendLine("Invalid format in the first line of the input.");
                return output.ToString();
            }

            board = new char[n][];
            visited = new bool[n][];

            for (int i = 0; i < n; i++)
            {
                if (lines[i + 1].Length != m)
                {
                    output.AppendLine("Invalid format in the input. Each row should have exactly " + m + " characters.");
                    return output.ToString();
                }
                board[i] = lines[i + 1].ToCharArray();
                visited[i] = new bool[m];
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (board[i][j] == '?' && !visited[i][j])
                    {
                        MinimizeCycles(i, j);
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    visited[i][j] = false;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (board[i][j] == '?' && !visited[i][j])
                    {
                        MaximizeCycles(i, j);
                    }
                }
            }

            // Writing the board to the output string
            for (int i = 0; i < n; i++)
            {
                output.AppendLine(new string(board[i]));
            }

            return output.ToString();

            bool IsValid(int x, int y)
            {
                return x >= 0 && x < n && y >= 0 && y < m;
            }

            void MinimizeCycles(int x, int y)
            {
                visited[x][y] = true;

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + d[i][0];
                    int ny = y + d[i][1];

                    if (IsValid(nx, ny) && board[nx][ny] == '?' && !visited[nx][ny])
                    {
                        board[x][y] = dirs[i];
                        board[nx][ny] = dirs[(i + 2) % 4];
                        MinimizeCycles(nx, ny);
                        return;
                    }
                }
            }

            void MaximizeCycles(int x, int y)
            {
                visited[x][y] = true;

                for (int i = 3; i >= 0; i--)
                {
                    int nx = x + d[i][0];
                    int ny = y + d[i][1];

                    if (IsValid(nx, ny) && board[nx][ny] == '?' && !visited[nx][ny])
                    {
                        board[x][y] = dirs[i];
                        board[nx][ny] = dirs[(i + 2) % 4];
                        MaximizeCycles(nx, ny);
                        return;
                    }
                }
            }
        }

    }
}
