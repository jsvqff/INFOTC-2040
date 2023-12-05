using System;
using System.IO;

namespace Document
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true){
                //Display “Document” followed by a blank line.
                Console.WriteLine("Document\n----------\n");
                
                //Prompt the user for the name of the document.
                string docName = getDocumentName();

                //Prompt the user for the content that is to be in the document.
                Console.WriteLine("\nEnter the content to be written to the document");
                string docText = Console.ReadLine();
                           
                //Write the content to a file in the current directory and print appropriate message to user
                writeContentToDocument(docName, docText);

                //ask user if the would like to write another file
                Console.WriteLine("To create another file type 'y'. Type any other key to exit");
                string loopAgain = Console.ReadLine();

                if (!loopAgain.Equals("y")){
                    break;
                }               
            }
        }

        /*
        Function to get the word count of the document
        Parameters: docment name
        Returns: length of the document 
        */
        static int getWordCount(string document){
            //split the document content at the spaces " " to get the words
            string[] words = document.Split(" ");
            return words.Length;
        }

        /*
        Function to get the document name. Function also calls the 
        Parameters: none
        Returns: the document name ending with ".txt"
        */
        static string getDocumentName(){
            string docName;
            while(true){
                Console.WriteLine("Enter the name of the document");
                docName = Console.ReadLine();
                
                if(docName == ""){
                    //throw new Exception("Error: Document name is null.\n You must enter a document name.\n");
                    Console.WriteLine("Error: You must enter a document name.\n");
                    continue;
                }else{
                    break;
                }
            }
            return checkDocumentName(docName);
        }

        /*
        Function to check the ending of the document name. Appends ".txt" to document name if .txt not present
        Parameters: string document name
        Returns: the document name ending with ".txt"
        */
        static string checkDocumentName(string documnetName){
            if(documnetName.EndsWith(".txt")){
                return documnetName;
            }else{
                return documnetName + ".txt";
            }
        }

        /*
        Function to write content to the file and print results to the user
        Parameters: string document name, string document text
        Returns: nothing
        */
        static void writeContentToDocument(string documnetName, string documentText){

            //get the number of words in document
            int numberOfWords = getWordCount(documentText);

            //create stream writer object to open file to write to 
            StreamWriter sw = new StreamWriter(documnetName);
            try{
                sw.WriteLine(documentText);
                sw.Close();
            }catch(Exception err){
                //If an exception occurs, output the exception message and exit.
                Console.WriteLine("Exception: " + err.Message);
            }finally{
                if (sw != null){
                    sw.Close();
                }
                //If an exception does not occur, output “[filename] was successfully saved.
                Console.WriteLine($"\n{documnetName} was successfully saved. The document contains {numberOfWords} words.");
            }
        }
    }
}