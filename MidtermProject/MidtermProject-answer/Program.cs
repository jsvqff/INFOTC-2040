using System;

namespace MidtermProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create instances of Employee, Associate, and Manager
            Employee employee1 = new Employee("Truman", "Tiger", "12345", EmployeeType.Associate);
            Associate associate1 = new Associate("Mickey", "Mouse", "23456", "Sporting Goods", 7500);
            Manager manager1 = new Manager("Elmer", "Fudd", "56789", "Electronics", "Midwest");

            Console.WriteLine("\n-------Employee 1-------------");
            employee1.getEmployeeInfo();

            Console.WriteLine("\n-------Sales Person 1-------------");
            associate1.getEmployeeInfo();
            Console.WriteLine($"Sales Level: {associate1.GetSalesLevel()} | Sales: ${associate1.getSales()}");
            associate1.updateSales(5250.70f);
            Console.WriteLine($"Updated Sales Level: {associate1.GetSalesLevel()} | Updated Sales: ${associate1.getSales()}");

            Console.WriteLine("\n-------Manager 1-------------");
            manager1.getEmployeeInfo();
            Console.WriteLine($"Dept: {manager1.getDepartment()} | Region: {manager1.getRegion()}");
            manager1.setFirstName("Wiley");
            manager1.setLastName("Coyote");
            manager1.setRegion("Southeast");
            manager1.setDepartment("Automotive");
            Console.WriteLine($"\nNew Name: {manager1.getFirstName()} {manager1.getlastName()}");
            Console.WriteLine($"New Dept: {manager1.getDepartment()} | New Region: {manager1.getRegion()}");
            
        }
    }
}
