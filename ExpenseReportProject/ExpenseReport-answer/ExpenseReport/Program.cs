namespace ExpenseReport;
class Program
{
    static void Main(string[] args)
    {
        string expenseReport = "*****************\nExpense Report\n*****************\n\n";

        Dictionary<string, List<float>> expenses = new Dictionary<string, List<float>>();
        //open and read file into a dictionary of lists. Category keys and cost values
        try{
            StreamReader cc_reader = new StreamReader("credit_card.csv");
            expenses = loadExpenseDictionary(cc_reader);
            cc_reader.Close();
        }catch{
            Console.WriteLine("ERROR: could not open file. Check the path and try again");
        }
        
        //Calculate total purchases
        float totalPurchases = calculateTotalPurchases(expenses);
        expenseReport += $"Total Cost of Purchases: ${totalPurchases:N2}\n";

        //Calculate Number of Purchases
        int numberOfPurchases = getNumberOfPurchases(expenses);
        expenseReport += $"Number of Purchases: {numberOfPurchases}\n";

        //Calculate Average purchase
        float avgPurchase = totalPurchases / numberOfPurchases;
        expenseReport += $"Average Purchase: ${avgPurchase:N2}\n";

        //calculate the cost of purchases in each category
        expenseReport += "\nCost of Purchases by Category\n-------------------------\n";
        foreach(string category in expenses.Keys){
            expenseReport += $"{category}: ${expenses[category].Sum()}\n";
        }

        //calculate the number of purchases in each category
        expenseReport += "\nNumber of Purchases by Category\n-------------------------\n";
        foreach(string category in expenses.Keys){
            expenseReport += $"{category} Purchases: {expenses[category].Count}\n";
        }

        //calculate the max expense and category
        float maxPurchase = 0;
        string maxPurchaseCategory = "";
        expenseReport += "\nMost Expensive purchase\n-------------------------\n";
        foreach(string category in expenses.Keys){
            if(expenses[category].Max() >= maxPurchase){
                maxPurchase = expenses[category].Max();
                maxPurchaseCategory += " " + category;
            }
        }

        expenseReport += $"{maxPurchaseCategory}: ${maxPurchase}\n";

        //calculate the max expense and category
        float minPurchase = 1000000;
        string minPurchaseCategory = "";
        expenseReport += "\nLeast Expensive purchase\n-------------------------\n";
        foreach(string category in expenses.Keys){
            if(expenses[category].Min() <= minPurchase){
                minPurchase = expenses[category].Min();
                minPurchaseCategory += " " + category;
            }
        }

        expenseReport += $"{minPurchaseCategory}: ${minPurchase}\n";

        StreamWriter reportWriter = new StreamWriter("report.txt");
        reportWriter.Write(expenseReport);
        reportWriter.Close();
    
    }

    static int getNumberOfPurchases(Dictionary<string, List<float>> expenseDict){
        int numberOfPurchases = 0;

        foreach(string category in expenseDict.Keys){
            numberOfPurchases += expenseDict[category].Count;
        }

        return numberOfPurchases;
    }

    static float calculateTotalPurchases(Dictionary<string, List<float>> expenseDict){
        float total = 0;

        foreach(string category in expenseDict.Keys){
            foreach(float expense in expenseDict[category]){
                total += expense;
            }
        }

        return total;
    }

    static Dictionary<string, List<float>> loadExpenseDictionary(StreamReader fileReader){
        
        //create a dictionary of lists
        Dictionary<string, List<float>> expenseDictionary = new Dictionary<string, List<float>>();
        string category = "";
        float expense = 0;
        
        while(!fileReader.EndOfStream){
            
            //read file line by line
            string lineOfData = fileReader.ReadLine()!;

            //split line of data
            string[] expenseData = lineOfData.Split(",");

            //get key and value
            try{
                category = expenseData[0];
                expense = float.Parse(expenseData[1]);
            }catch{
                continue;
            }

            //place data in dictionary. check if key exists
            //if key does not exist. Add key to the dictionary with a new list
            //if key does exist. Add expense to the list of that key
            if(!expenseDictionary.ContainsKey(category)){
                expenseDictionary.Add(category, new List<float>(){expense});
            }else{
                expenseDictionary[category].Add(expense);
            }
            
        }

        return expenseDictionary;
    }
}
