using System;

namespace MidtermProject{
    class Associate : Employee{

        private string department;
        private float sales;

        public Associate(string firstName, string lastName, string id, string department, int sales) : base(firstName, lastName, id, EmployeeType.Associate){
            this.department = department;
            this.sales = sales;

        }

        public string getDepartment(){
            return this.department;
        }

        public void setDepartment(string newDepartment){
            this.department = newDepartment;
        }

        public float getSales(){
            return this.sales;
        }

        public void updateSales(float newSales){
            this.sales += newSales;
        }

        public SalesLevel GetSalesLevel(){
            if (this.sales < 10000){
                return SalesLevel.Bronze;
            } else if(this.sales < 20000){
                return SalesLevel.Silver;
            }else if(this.sales < 30000){
                return SalesLevel.Gold;
            }else if(this.sales < 40000){
                return SalesLevel.Gold;
            }else{
                return SalesLevel.Platinum;
            }
        }

    }
}