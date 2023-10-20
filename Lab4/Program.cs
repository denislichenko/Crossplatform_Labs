using LabRunner;
using System;

namespace Lab4
{
    /*
        TODO: 
        Calculate path using Environment Variable 
     */
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введіть команду (version, run lab1/lab2/lab3, set-path -p/--path <шлях>):");
                string input = Console.ReadLine();
                string[] inputTokens = input.Split(' ');

                if (inputTokens.Length == 0)
                {
                    Console.WriteLine("Невірна команда. Введіть команду ще раз.");
                    continue;
                }

                string command = inputTokens[0].ToLower();

                if (command == "version")
                {
                    Console.WriteLine("Автор: Denys Lichenko");
                    Console.WriteLine("Версія: 1.0");
                }
                else if (command == "run" && inputTokens.Length > 1)
                {

                    string subCommand = inputTokens[1].ToLower();
                    string inputFilePath = null;
                    string outputFilePath = null;

                    for (int i = 2; i < inputTokens.Length; i++)
                    {
                        if (inputTokens[i] == "-I" || inputTokens[i] == "--input")
                        {
                            if (i + 1 < inputTokens.Length)
                            {
                                inputFilePath = inputTokens[i + 1];
                                i++;
                            }
                        }
                        else if (inputTokens[i] == "-o" || inputTokens[i] == "--output")
                        {
                            if (i + 1 < inputTokens.Length)
                            {
                                outputFilePath = inputTokens[i + 1];
                                i++;
                            }
                        }
                    }

                    if (subCommand == "lab1" || subCommand == "lab2" || subCommand == "lab3")
                    {
                        Console.WriteLine($"Виконується команда 'run {subCommand}' з вхідним файлом '{inputFilePath}' та вихідним файлом '{outputFilePath}'");
                        
                        if (!string.IsNullOrEmpty(inputFilePath) && !string.IsNullOrEmpty(outputFilePath))
                        {
                            switch(subCommand)
                            {
                                case "lab1": 
                                    Lab1.Execute(inputFilePath, outputFilePath);
                                    break;
                                case "lab2":
                                    Lab2.Execute(inputFilePath, outputFilePath);
                                    break;
                                case "lab3":
                                    Lab3.Execute(inputFilePath, outputFilePath);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Невірна підкоманда для 'run'. Введіть команду ще раз.");
                    }
                }
                else if (command == "set-path" && inputTokens.Length > 2)
                {
                    if ((inputTokens[1] == "-p" || inputTokens[1] == "--path") && !string.IsNullOrEmpty(inputTokens[2]))
                    {
                        string labPath = inputTokens[2];
                        Environment.SetEnvironmentVariable("LAB_PATH", labPath);
                        Console.WriteLine($"Шлях до папки з інпут та аутпут файлами встановлено на '{labPath}'");
                    }
                    else
                    {
                        Console.WriteLine("Невірна команда 'set-path'. Введіть команду ще раз.");
                    }
                }
                else
                {
                    Console.WriteLine("Невірна команда. Введіть команду ще раз.");
                }
            }
        }
    }
}
