using System;
using System.IO;

namespace Documnet_Merger
{
    class Program
    {
        static void Main(string[] args)
        {
            string file1, file2, mergedName, moreMerges;
            bool fileWritten;
            int charsInFile = 0;
            //Display Document Merger
            Console.WriteLine("Document Merger \n");

            //while loop allow the user to run the program multiple times
            while(true){
                //Get File name call to function
                file1 = GetFileName("Enter name of the first text file"); 

                //Prompt the user for the name of the second document.
                file2 = GetFileName("Enter name of the second text file"); 
                
                //Fuction Call to Merge the two file names and append .txt to the end 
                mergedName = mergeFileNames(file1, file2);
                
                //Read and merge the text of the two files.
                //Save the content to a file in the current directory.
                var result = readAndMergeFiles(file1, file2, mergedName);
                fileWritten = result.Item1;
                charsInFile = result.Item2;

                //If an exception occurs, output the exception message and exit.
                //If an exception does not occur, output “[filename] was successfully saved. 
                //The document contains [count] characters.” and exit.
                if(fileWritten){
                    Console.WriteLine($"{mergedName} was successfully saved. The document contains {charsInFile} characters");
                }
                //ask the user if they would like to merge two more files. 
                Console.WriteLine("Would you like to merge two more files (y/n)");
                moreMerges = Console.ReadLine();
                if (moreMerges != "y"){
                    Console.WriteLine("Thank you and Goodbye!");
                    break;
                }
            }
        }

        //Read and merge the two files
        static Tuple <bool, int> readAndMergeFiles(string name1, string name2, string mergedName){

            StreamReader sr1 = null;
            StreamReader sr2 = null;
            StreamWriter mergedDoc = null;
            string docText = "";
            bool success = false;
            int charCount = 0;

            try{
                //Open both files to be read
                sr1 = new StreamReader(name1);
                sr2 = new StreamReader(name2);

                //Open the file to be merged in write mode
                mergedDoc = new StreamWriter(mergedName);

                //Read from file1 and write to the merged file
                while((docText = sr1.ReadLine()) != null){
                    mergedDoc.WriteLine(docText);
                    charCount += docText.Length;
                }

                //Read from file2 and write to the merged file
                while((docText = sr2.ReadLine()) != null){
                    mergedDoc.WriteLine(docText);
                    charCount += docText.Length;
                }

                //Close all 3 files
                sr1.Close();
                sr2.Close();
                mergedDoc.Close();

                //Set Success to true
                success = true;

            }catch(Exception e){
                //If an exception occurs, output the exception message and exit.
                Console.WriteLine("Exception: " + e.Message);
                success = false;
                charCount = 0;

            }finally{
                if (sr1 != null){
                    sr1.Close();
                }

                if (sr2 != null){
                    sr2.Close();
                }

                if (mergedDoc != null){
                    mergedDoc.Close();
                }

            }
        
            return Tuple.Create(success, charCount);

        }

        //Get file name function
        static string GetFileName(string userPrompt){
            string userFileName;
            //In while loop
            while (true){
                //Prompt the user for the name of the first text file.
                Console.WriteLine(userPrompt);
                userFileName = Console.ReadLine();

                //Verify filename is in the correct format
                //Check if user enters empty string
                if(userFileName == ""){
                    Console.WriteLine("ERROR: Must enter a file name");
                    continue;
                //Append .txt to the end of the file name if the user did not
                }else if (!userFileName.EndsWith(".txt")){
                    userFileName += ".txt";
                }
                
                //Verify that the first file exists.
                //If not, give the user feedback and let them re-enter the first filename.
                if(!File.Exists(userFileName)){
                    Console.WriteLine($"ERROR: {userFileName} does not exist! ReEnter file name"); 
                    continue;
                }else{
                    //return filename
                    return userFileName;
                }
            }       
        }

        static string mergeFileNames(string name1, string name2){
            //Merge filename function
            //remove .txt from the first file name and return the concatenated file name .txt
            name1 = name1.Substring(0, name1.Length - 4); 

            return name1 + name2;
        }            
    }
}
