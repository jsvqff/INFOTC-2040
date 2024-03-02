using System;

namespace MidtermProject{
    
    class Employee{
        private string firstName, lastName, id;
        EmployeeType empType;

        public Employee(string firstName, string lastName, string id, EmployeeType empType){
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = id;
            this.empType = empType;
        }

        public string getFirstName(){
            return this.firstName;
        }

        public void setFirstName(string newFirstName){
            this.firstName = newFirstName;
        }

        public string getlastName(){
            return this.lastName;
        }

        public void setLastName(string newLastName){
            this.lastName = newLastName;
        }

        public string getID(){
            return this.id;
        }

        public void setEmployeeType(EmployeeType newEmpType){
            this.empType = newEmpType;
        }

        public EmployeeType getEmployeeType(){
            return this.empType;
        }

        public void getEmployeeInfo(){
            Console.WriteLine($"Name: {this.firstName} {this.lastName}");
            Console.WriteLine($"ID: {this.id}");
            Console.WriteLine($"Type: {this.empType}");
        }
    }

}