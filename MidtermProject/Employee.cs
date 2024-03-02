using System;

public enum EmployeeType { Associate, Manager, Production }
public enum SalesLevel { Bronze, Silver, Gold, Diamond, Platinum }

public class Employee
{
    private string firstName;
    private string lastName;
    private string id;
    private EmployeeType empType;

    public Employee(string firstName, string lastName, string id, EmployeeType empType)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
        this.empType = empType;
    }

    public string getFirstName()
    {
        return firstName;
    }

    public void setFirstName(string firstName)
    {
        this.firstName = firstName;
    }

    public string getLastName()
    {
        return lastName;
    }

    public void setLastName(string lastName)
    {
        this.lastName = lastName;
    }

    public EmployeeType getEmpType()
    {
        return empType;
    }

    public void setEmpType(EmployeeType empType)
    {
        this.empType = empType;
    }

    public string getId()
    {
        return id;
    }

    public void getEmployeeInfo()
    {
        Console.WriteLine($"Name: {firstName} {lastName}");
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"Type: {empType}");
    }
}
