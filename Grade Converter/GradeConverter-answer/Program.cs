using System;
using System.Collections.Generic;

namespace GradeConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool doAgain = true;
            float score, totalScore = 0 , averageScore;
            string letterGrade, moreConversions;
            int numberOfGrades;

            //Ask the user to enter their first and last name
            Console.WriteLine("Please enter your first name and last name");
            string userName = Console.ReadLine();

            //Print a welcome message: "Hello [first name] [last name] Welcome to the Grade Converter!"
            Console.WriteLine($"\nHello {userName}! Welcome to the Grade Converter!");

            while(doAgain){
                List<float> scores = new List<float>();

                //Prompt the user to enter the number of grades they need to convert "Enter the number of grades you need to convert: "
                Console.WriteLine("\nEnter the number of grades you need to convert:");
                numberOfGrades = int.Parse(Console.ReadLine());

                //Prompt the user to enter the grades. The grades should be stored in a list and you should use the appropriate data type for the grades. 
                for(int counter = 0; counter < numberOfGrades; counter ++){
                    score = getScores();
                    scores.Add(score);
                }
                
                Console.WriteLine();
                foreach(float testScore in scores){
                    //Convert the number grades to letter grades (A = 90-100, B = 80-89, C = 70-79, D = 60 - 69, F lower than 60)
                    letterGrade = getLetterGrade(testScore);

                    //Print all the numerical grades and their corresponding letter grades to the console 
                    Console.WriteLine($"A score of {testScore} is a {letterGrade} grade"); 

                    totalScore += testScore; 
                }
                
                Console.WriteLine("\nGrade Statistics\n-------------------------\n");
                averageScore = totalScore / numberOfGrades; 
                Console.WriteLine($"Number of grades: {numberOfGrades}");
                Console.WriteLine($"Average Grade: {averageScore} which is a {getLetterGrade(averageScore)}");

                Console.WriteLine("Would you like to convert nore grades (y/n)?");
                moreConversions = Console.ReadLine();

                //reset total score
                totalScore = 0;

                if (moreConversions != "y" && moreConversions != "Y"){
                    doAgain = false;
                }
            }
        }

        static float getScores(){
            float grade;
            while(true){
                Console.WriteLine("\nEnter the score to be converted: ");
                try{
                    grade = float.Parse(Console.ReadLine());
                    break;  
                }catch(FormatException){
                    Console.WriteLine("Error: Only numbers allowed");
                }
            }  
            return grade;
        }

        static string getLetterGrade(float score){
            //(A = 90-100, B = 80-89, C = 70-79, D = 60 - 69, F lower than 60).
            if (score >= 90){
                return "A";
            }else if(score >= 80){
                return "B";
            }else if (score >= 70){
                return "C";
            }else if(score >= 60){
                return "D";
            }else{
                return "F";
            }
        }

    }
}
