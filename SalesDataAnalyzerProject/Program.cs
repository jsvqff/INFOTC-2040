using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            if (args.Length != 2)
            {
                Console.WriteLine("SalesDataAnalyzer <sales_data_file_path> <report_file_path>");
                return;
            }

            string salesFilePath = args[0];
            string reportFilePath = args[1];

            // Read sales data
            var salesData = ReadSalesData(salesFilePath);

            // Generate and write report
            GenerateAndWriteReport(salesData, reportFilePath);

            Console.WriteLine("Report generated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static List<SalesRecord> ReadSalesData(string filePath)
    {
        List<SalesRecord> salesData = new List<SalesRecord>();

        try
        {
            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                int lineNumber = 1;
                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length != SalesRecord.NumItemsInRow)
                    {
                        throw new Exception($"Row {lineNumber} contains {values.Length} values. It should contain {SalesRecord.NumItemsInRow}.");
                    }

                    var record = new SalesRecord
                    {
                        InvoiceID = values[0],
                        Branch = values[1],
                        City = values[2],
                        CustomerType = values[3],
                        Gender = values[4],
                        ProductLine = values[5],
                        UnitPrice = decimal.Parse(values[6], CultureInfo.InvariantCulture),
                        Quantity = int.Parse(values[7]),
                        Date = DateTime.Parse(values[8]),
                        Payment = values[9],
                        Rating = decimal.Parse(values[10], CultureInfo.InvariantCulture)
                    };

                    salesData.Add(record);
                }
            }

            return salesData;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading lines from sales data file: {ex.Message}");
        }
    }

    static void GenerateAndWriteReport(List<SalesRecord> salesData, string reportFilePath)
    {
        try
        {
            using (var writer = new StreamWriter(reportFilePath))
            {
                // Question 1: Calculate the total sales in the data set.
                writer.WriteLine("*************************************");
                writer.WriteLine("Total Sales in Dataset");
                writer.WriteLine("*************************************");
                decimal totalSales = salesData.Sum(s => s.Quantity * s.UnitPrice);
                writer.WriteLine($"Total Sales: {totalSales:C}");
                writer.WriteLine("");

                // Question 2: Show the unique product lines in the data set.
                var uniqueProductLines = salesData.Select(s => s.ProductLine).Distinct().ToList();
                writer.WriteLine("*************************************");
                writer.WriteLine("Unique Productlines");
                writer.WriteLine("*************************************");
                foreach (var productLine in uniqueProductLines)
                {
                    writer.WriteLine(productLine);
                }
                writer.WriteLine("");

                // Question 3: Calculate the total sales for each product line.
                var totalSalesPerProductLine = salesData
                    .GroupBy(s => s.ProductLine)
                    .Select(group => new
                    {
                        ProductLine = group.Key,
                        TotalSales = group.Sum(s => s.Quantity * s.UnitPrice)
                    })
                    .OrderBy(item => item.ProductLine)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Total Sales per Product Line");
                writer.WriteLine("*************************************");
                foreach (var item in totalSalesPerProductLine)
                {
                    writer.WriteLine($"{item.ProductLine}: {item.TotalSales:C}");
                }
                writer.WriteLine("");

                // Question 4: Calculate the total Sales per city.
                var totalSalesPerCity = salesData
                    .GroupBy(s => s.City)
                    .Select(group => new
                    {
                        City = group.Key,
                        TotalSales = group.Sum(s => s.Quantity * s.UnitPrice)
                    })
                    .OrderBy(item => item.City)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Total Sales per City");
                writer.WriteLine("*************************************");
                foreach (var item in totalSalesPerCity)
                {
                    writer.WriteLine($"{item.City}: {item.TotalSales:C}");
                }
                writer.WriteLine("");

                // Question 5: Which product lines have the sale with the highest unit price?
                var highestUnitPrices = salesData
                    .OrderByDescending(s => s.UnitPrice)
                    .Take(2)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Product lines with Highest Unit Price");
                writer.WriteLine("*************************************");
                foreach (var sale in highestUnitPrices)
                {
                    writer.WriteLine($"{sale.ProductLine}: {sale.UnitPrice:C}");
                }
                writer.WriteLine("");

                // Question 6: Calculate the total sales per month in the data set.
                var totalSalesPerMonth = salesData
                    .GroupBy(s => s.Date.ToString("MMMM"))
                    .OrderBy(group => DateTime.ParseExact(group.Key, "MMMM", CultureInfo.InvariantCulture))
                    .Select(group => new
                    {
                        Month = group.Key,
                        TotalSales = group.Sum(s => s.Quantity * s.UnitPrice)
                    })
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Total Sales per Month");
                writer.WriteLine("*************************************");
                foreach (var item in totalSalesPerMonth)
                {
                    writer.WriteLine($"{item.Month}: {item.TotalSales:C}");
                }
                writer.WriteLine("");

                // Question 7: Calculate the total sales per payment type.
                var totalSalesPerPaymentType = salesData
                    .GroupBy(s => s.Payment)
                    .Select(group => new
                    {
                        PaymentType = group.Key,
                        TotalSales = group.Sum(s => s.Quantity * s.UnitPrice)
                    })
                    .OrderBy(item => item.PaymentType)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Total Sales by Payment Type");
                writer.WriteLine("*************************************");
                foreach (var item in totalSalesPerPaymentType)
                {
                    writer.WriteLine($"{item.PaymentType}: {item.TotalSales:C}");
                }
                writer.WriteLine("");

                // Question 8: Calculate the number of sales transactions per member type.
                var salesTransactionsPerMemberType = salesData
                    .GroupBy(s => s.CustomerType)
                    .Select(group => new
                    {
                        MemberType = group.Key,
                        NumberOfTransactions = group.Count()
                    })
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Total Transactions by Member Type");
                writer.WriteLine("*************************************");
                foreach (var item in salesTransactionsPerMemberType)
                {
                    writer.WriteLine($"{item.MemberType}: {item.NumberOfTransactions}");
                }
                writer.WriteLine("");

                // Question 9: Calculate the average rating per product line.
                var averageRatingPerProductLine = salesData
                    .GroupBy(s => s.ProductLine)
                    .Select(group => new
                    {
                        ProductLine = group.Key,
                        AverageRating = group.Average(s => s.Rating)
                    })
                    .OrderBy(item => item.ProductLine)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Average Rating per Product Line");
                writer.WriteLine("*************************************");
                foreach (var item in averageRatingPerProductLine)
                {
                    writer.WriteLine($"{item.ProductLine}: {item.AverageRating:F2}");
                }
                writer.WriteLine("");

                // Question 10: Calculate the total number of transactions per payment type.
                var totalTransactionsPerPaymentType = salesData
                    .GroupBy(s => s.Payment)
                    .Select(group => new
                    {
                        PaymentType = group.Key,
                        NumberOfTransactions = group.Count()
                    })
                    .OrderBy(item => item.PaymentType)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Total Transactions by Payment Type");
                writer.WriteLine("*************************************");
                foreach (var item in totalTransactionsPerPaymentType)
                {
                    writer.WriteLine($"{item.PaymentType}: {item.NumberOfTransactions}");
                }
                writer.WriteLine("");

                // Question 11: Calculate the total quantity of products sold per city.
                var totalQuantityPerCity = salesData
                    .GroupBy(s => s.City)
                    .Select(group => new
                    {
                        City = group.Key,
                        TotalQuantity = group.Sum(s => s.Quantity)
                    })
                    .OrderBy(item => item.City)
                    .ToList();

                writer.WriteLine("*************************************");
                writer.WriteLine("Number of Products Sold per City");
                writer.WriteLine("*************************************");
                foreach (var item in totalQuantityPerCity)
                {
                    writer.WriteLine($"{item.City}: {item.TotalQuantity}");
                }
                writer.WriteLine("");

                // Question 12: Using a 5% sales tax, Calculate the tax for each sales transaction in each product line.
                var salesTaxPerTransaction = salesData
                    .GroupBy(s => s.ProductLine)
                    .OrderBy(group => group.Key)
                    .ToList();

                writer.WriteLine("******************************************");
                writer.WriteLine("Tax per Sale per Product Line");
                writer.WriteLine("******************************************");
                writer.WriteLine("");

                foreach (var group in salesTaxPerTransaction)
                {
                    writer.WriteLine($"**********{group.Key.ToUpper()}**********");

                    var transactions = group
                        .Select(s => new
                        {
                            InvoiceNumber = s.InvoiceID,
                            TotalSales = s.Quantity * s.UnitPrice,
                            TaxAmount = (s.Quantity * s.UnitPrice) * 0.05M
                        })
                        .ToList();

                    foreach (var item in transactions)
                    {
                        writer.WriteLine($"Invoice: {item.InvoiceNumber} - Total: {item.TotalSales:C} - Tax: {item.TaxAmount:C}");
                    }
                    writer.WriteLine("");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error writing to report file: {ex.Message}");
        }
    }
}

class SalesRecord
{
    public const int NumItemsInRow = 11;

    public string InvoiceID { get; set; }
    public string Branch { get; set; }
    public string City { get; set; }
    public string CustomerType { get; set; }
    public string Gender { get; set; }
    public string ProductLine { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public string Payment { get; set; }
    public decimal Rating { get; set; }
}
