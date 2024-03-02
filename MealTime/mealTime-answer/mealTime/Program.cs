namespace mealTime;
class Program
{
    static void Main(string[] args)
    {
        //prompt user what time is it
        Console.WriteLine("What time is it? ");
        //get user response
        string userResponse = Console.ReadLine()!;
        //send response to function
        
        float timeNumber = convertTime(userResponse);
        
        //use if statement to determine what time it is
        if(timeNumber >= 7 && timeNumber <= 8){
            Console.WriteLine("Breakfast Time!");
        }else if(timeNumber >= 12 && timeNumber <= 13){
            Console.WriteLine("Lunch Time!");
        }else if(timeNumber >= 18 && timeNumber <= 19){
            Console.WriteLine("Dinner Time!");
        }else{
            Console.WriteLine("Not time to eat!");
        }
    }

    public static float convertTime(string time){
        //in function, use split to get the hours and minutes
        string []timeParts = time.Split(":");
        //get hours and minutes
        float hours = float.Parse(timeParts[0]);
        float minutes = float.Parse(timeParts[1]);

        //convert minutes to a decimal
        minutes = minutes/60;
        
        return hours + minutes;
    }
}

    

