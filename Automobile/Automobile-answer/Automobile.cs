using System;

namespace AutoDemo{
    class Automobile{
        private string make, model, vin, color;
        private int year;
        private AutoType type;
        //create a constructor for the Automobile class
        public Automobile(string carMake, string model, int year, string vin, string color, AutoType type){
            this.make = carMake;
            this.model = model;
            this.year = year;
            this.vin = vin;
            this.color = color;
            this.type = type;
        }

        public AutoType getType(){
            return this.type;
        }

        public string getMake(){
            return this.make;
        }

        public string getModel(){
            return this.model;
        }

        public int getYear(){
            return this.year;
        }

        public string getVin(){
            return this.vin;
        }

        public string getColor(){
            return this.color;
        }

        public void setColor(string newColor){
            this.color = newColor;
        }

        public int getAutoAge(){
            DateTime currentDate = DateTime.Now;
            int currentYear = currentDate.Year;
            return currentYear - this.year;
        }


    }
}