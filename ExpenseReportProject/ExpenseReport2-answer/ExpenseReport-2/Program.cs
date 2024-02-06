namespace ExpenseReport;
class Program
{
    static void Main(string[] args)
    {
        //Declare variables
        float totalSales = 0, avgPurchase = 0, minPurchase = 1000000, maxPurchase = 0;
        float numberOfPurchases = 0;
        List<string> maxPurchaseCategories = new List<string>();
        List<string> minPurchaseCategories = new List<string>();

        //create a streamreader object
        StreamReader cc_reader = null!;
        try{
            //create a file handler for the credit_card.csv file
            cc_reader = new StreamReader("credit_card.csv");
        }catch{
            Console.WriteLine("ERROR: could not open file. Check the path and try again");
            Environment.Exit(0);
        }
        
        //create a dictionary of lists
        Dictionary<string, float> salesDictionary = new Dictionary<string, float>();
        Dictionary<string, float> purchaseDictionary = new Dictionary<string, float>();
        string category = "";
        float sale = 0;
        
        //read the credit_card.csv line by line
        while(!cc_reader.EndOfStream!){
            
            //read the next line in the file
            string lineOfData = cc_reader.ReadLine()!;

            //split line of data
            string[] expenseData = lineOfData.Split(",");

            //get key and value
            try{
                category = expenseData[0];
                sale = float.Parse(expenseData[1]);
            }catch{
                continue;
            }

            //place data in dictionary. check if key exists
            //if key does not exist add keys and initial values to dictionaries. salesDictionary[category] = sale; purchaseDictionary[category] = 1
            //if key does exist. Add the sale to sales dictionary and add 1 to the purchase dictionary for that category
            if(!salesDictionary.ContainsKey(category)){
                salesDictionary.Add(category, sale);
                purchaseDictionary.Add(category, 1);
            }else{
                salesDictionary[category] += sale;
                purchaseDictionary[category] += 1;
            }

            //determine max purchase and category. If current sale > maxPurchase, set maxPurchase to the current sale
            //add the current category to the maxPurchaseCategories if it is not already in the list
            if(sale == maxPurchase){
                if(!maxPurchaseCategories.Contains(category)){
                    maxPurchaseCategories.Add(category);
                }
            }else if(sale > maxPurchase){
                 maxPurchase = sale;
                 maxPurchaseCategories.Clear();
                 maxPurchaseCategories.Add(category);
            }

            //determine min purchase and category. If current sale < minPurchase, set minPurchase to the current sale
            //add the current category to the minPurchaseCategories if it is not already in the list
            if(sale == minPurchase){
                if(!minPurchaseCategories.Contains(category)){
                    minPurchaseCategories.Add(category);
                }
            }else if(sale < minPurchase){
                 minPurchase = sale;
                 minPurchaseCategories.Clear();
                 minPurchaseCategories.Add(category);
            }
            
        }

        //Calculate total purchases
        totalSales = sumDictionaryCategories(salesDictionary);

        //Calculate Number of Purchases
        numberOfPurchases = sumDictionaryCategories(purchaseDictionary);

        //Calculate Average purchase
        avgPurchase = totalSales / numberOfPurchases;

        // write report
        writeExpenseReport(purchaseDictionary, salesDictionary, maxPurchaseCategories, minPurchaseCategories, 
        totalSales, avgPurchase, numberOfPurchases, maxPurchase, minPurchase);
    }

    /*
    Function to write the expense report
    Input: purchase dictionary, sales dictionary, maxPurchaseCategories, minPurchaseCategories, total sales, num purchases, avd purchases
    Output: NONE
    */
    static void writeExpenseReport(Dictionary<string, float> purchaseDictionary, Dictionary<string, float> salesDictionary, 
    List<string> maxCategories, List<string> minCategories, float totalSales, float averageSales, float numberOfPurchases,
    float maxPurchase, float minPurchase){

        StreamWriter reportWriter = new StreamWriter("report.txt");
        reportWriter.Write("*****************\nExpense Report\n*****************\n\n");
        reportWriter.Write($"Total Cost of Purchases: ${totalSales:N2}\n");
        reportWriter.Write($"Number of Purchases: {numberOfPurchases:N0}\n");
        reportWriter.Write($"Average Purchase: ${averageSales:N2}\n");
        
        //write sales by category section. Loop through the salesDictionary and write the categories and values to the file
        reportWriter.Write("\nCost of Purchases by Category\n-------------------------\n");
        foreach(string category in salesDictionary.Keys){
           reportWriter.Write($"{category}: ${salesDictionary[category]}\n");
        }

        //write number of Purchases by category section. Loop through the purchaseDictionary and write the categories and values to the file
        reportWriter.Write("\nNumber of Purchases by Category\n-------------------------\n");
        foreach(string category in purchaseDictionary.Keys){
           reportWriter.Write($"{category}: ${purchaseDictionary[category]:N0}\n");
        }

        //create max category string and min category string
        string maxCategoryString = "", minCategoryString = "";
        foreach(string category in maxCategories){
            maxCategoryString += category + " ";
        }

        foreach(string category in minCategories){
            minCategoryString += category + " ";
        }

        reportWriter.Write($"{maxCategoryString}: ${maxPurchase}\n");
        reportWriter.Write($"{minCategoryString}: ${minPurchase}\n");
        
        reportWriter.Close();

    }
    
    /*
    Function to calculate the sum of all values in a dictionary
    Input: a dictionary of float values
    Output: the total
    */
    static float sumDictionaryCategories(Dictionary<string, float> theDictionary){
        float total = 0;
        //loop though dictionary keys and add values to total
        foreach(string category in theDictionary.Keys){
            total += theDictionary[category];
        }

        return total;
    }
}
