using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week6program
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput = Console.ReadLine();

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1 - Ohm's Law");
            Console.WriteLine("2 - Voltage Divider Rule");
            Console.WriteLine("3 - LED limiting resistor");

            if (userInput == "1")
            {

            }
            else if (userInput == "2")
            {

            }
            else if (userInput == "3")
            {

            }
            else if (userInput == "4")
            {

            }

            List<IComponent> myList = new List<IComponent> { new Resistor(), new LED() };
            foreach(IComponent item in myList)
            {
                PrintComponent(item);
            }
            Console.ReadLine();
        }

        public static void PrintComponent(IComponent component)
        {
            component.DisplayValues();
        }
        public static void PrintComponent(IComponent[] components)
        {
            foreach (IComponent component in components)
            {
                component.DisplayValues();
            }
        }
        //public static string ConvertToEngineeringNotation

    }

    public class Resistance
    {
        private double r;
        public double resistance { get; set; }
    }
    public class Voltage
    {
        private double v;
        public double voltage { get; set; }
    }
    public class Current
    {
        private double i;
        public double current { get; set; }
    }


    public class Resistor : IComponent
    {
        //public double voltage, power, current;
        double resistance;



        double voltage, current, power;

        public double Voltage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Current { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Power { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /*
        Resistor(Resistance r, Current i )
        {

        }
        Resistor(Voltage r, Current i)
        {

        }
        Resistor(Voltage r, Resistance i)
        {

        }*/

        //


        public void DisplayValues()
        {

            Console.WriteLine("resistor");
            //throw new NotImplementedException();
        }
    }

    public class LED : IComponent
    {
        private double voltage;
        public double Voltage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Current { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Power { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DisplayValues()
        {
            Console.WriteLine("LED");
            //throw new NotImplementedException();
        }
    }



    public interface IComponent
    {
        //void CalculateVoltage(double current,double resistance);

        double Voltage { get; set; }
        double Current { get; set; }
        double Value { get; set; }
        double Power { get; set; }





        void DisplayValues();



        //void PrintComponent(IComponent)

        //void PrintComponent(ICo)


    }




}
