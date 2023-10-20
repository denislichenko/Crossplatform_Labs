using System;
using System.IO;

namespace Lab3
{
    class Program
    {
        static int n, m;
        static char[][] board;
        static bool[][] visited;
        static char[] dirs = { 'R', 'L', 'U', 'D' };
        static int[][] d = { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 1, 0 } };

        static bool IsValid(int x, int y)
        {
            return x >= 0 && x < n && y >= 0 && y < m;
        }

        static void MinimizeCycles(int x, int y)
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

        static void MaximizeCycles(int x, int y)
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

        static void Main()
        {
            string inputFile = "INPUT.TXT";
            string outputFile = "OUTPUT.TXT";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Input file not found: " + inputFile);
                return;
            }

            using (StreamReader sr = new StreamReader(inputFile))
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                string[] nm = sr.ReadLine().Split();
                if (nm.Length != 2 || !int.TryParse(nm[0], out n) || !int.TryParse(nm[1], out m))
                {
                    Console.WriteLine("Invalid format in the first line of the input file.");
                    return;
                }

                board = new char[n][];
                visited = new bool[n][];

                for (int i = 0; i < n; i++)
                {
                    string line = sr.ReadLine();
                    if (line.Length != m)
                    {
                        Console.WriteLine("Invalid format in the input file. Each row should have exactly " + m + " characters.");
                        return;
                    }
                    board[i] = line.ToCharArray();
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
                    sw.WriteLine(board[i]);
                }

                sw.WriteLine();

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

                for (int i = 0; i < n; i++)
                {
                    sw.WriteLine(board[i]);
                }
            }
        }
    }
}
