using System;
using System.Collections.Generic;

namespace GradeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------GRADE CONVERTER PROGRAM----------");
            string firstName, lastName;
            Console.Write("Enter your first name: ");
            firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            lastName = Console.ReadLine();
            Console.WriteLine($"Hello, {firstName} {lastName}!");

            while (true)
            {
                List<double> grades = GetGrades();
                ConvertAndPrintGrades(grades);
                PrintGradeStatistics(grades);

                Console.Write("Do you have more grades to convert? (yes/no): ");
                string response = Console.ReadLine().ToLower();
                if (response != "yes")
                    break;
            }

            Console.WriteLine("Goodbye!");
        }

        static List<double> GetGrades()
        {
            List<double> grades = new List<double>();
            Console.Write("Enter the number of grades you need to convert: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfGrades))
            {
                for (int i = 1; i <= numberOfGrades; i++)
                {
                    double grade;
                    bool validInput = false;
                    do
                    {
                        Console.Write($"Enter grade {i}: ");
                        string input = Console.ReadLine();
                        if (double.TryParse(input, out grade))
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid numeric grade.");
                        }
                    } while (!validInput);

                    grades.Add(grade);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            return grades;
        }

        static char ConvertToLetterGrade(double grade)
        {
            if (grade >= 90)
                return 'A';
            else if (grade >= 80)
                return 'B';
            else if (grade >= 70)
                return 'C';
            else if (grade >= 60)
                return 'D';
            else
                return 'F';
        }

        static void ConvertAndPrintGrades(List<double> grades)
        {
            Console.WriteLine("Grade Conversion:");
            Console.WriteLine("--------------------------");
            foreach (var grade in grades)
            {
                char letterGrade = ConvertToLetterGrade(grade);
                Console.WriteLine($"A score of {grade} is a {letterGrade} grade");
            }
        }

        static void PrintGradeStatistics(List<double> grades)
        {
            Console.WriteLine("\nGrade Statistics");
            Console.WriteLine("--------------------------");
            Console.WriteLine($"Number of grades: {grades.Count}");
            double average = CalculateAverageGrade(grades);
            Console.WriteLine($"Average Grade: {average} ==> {ConvertToLetterGrade(average)}");
        }

        static double CalculateAverageGrade(List<double> grades)
        {
            if (grades.Count == 0)
                return 0;

            double total = 0;
            foreach (var grade in grades)
            {
                total += grade;
            }
            return total / grades.Count;
        }
    }
}
