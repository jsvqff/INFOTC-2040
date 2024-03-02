class Associate : Employee
{
    private string department;
    private float sales;

    public Associate(string firstName, string lastName, string id, string department, float sales) : base(firstName, lastName, id, EmployeeType.Associate)
    {
        this.department = department;
        this.sales = sales;
    }

    public void updateSales(float additionalSales)
    {
        sales += additionalSales;
    }

    public float getSales()
    {
        return sales;
    }

    public SalesLevel GetSalesLevel()
    {
        if (sales < 10000)
            return SalesLevel.Bronze;
        else if (sales < 20000)
            return SalesLevel.Silver;
        else if (sales < 30000)
            return SalesLevel.Gold;
        else if (sales < 40000)
            return SalesLevel.Diamond;
        else
            return SalesLevel.Platinum;
    }
}
