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
            for (; ; )
            {
                

                Console.WriteLine("Choose an option:");
                Console.WriteLine("1 - Ohm's Law");
                Console.WriteLine("2 - Voltage Divider Rule");
                Console.WriteLine("3 - LED limiting resistor");
                Console.WriteLine("4 - Exit Application");

                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    //Resistance resistance = new Resistance() { resistance = 5 };
                    //double test;
                    //Console.WriteLine(test);
                    Resistor OhmsLaw = new Resistor();
                    while (OhmsLaw.ValuesSet() == false)
                    {
                        if (Double.IsNaN(OhmsLaw.Voltage))
                        {
                            Console.Write("Enter Voltage --> ");
                            try { OhmsLaw.Voltage = Double.Parse(Console.ReadLine()); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(OhmsLaw.Current))
                        {
                            Console.Write("Enter Current --> ");
                            try { OhmsLaw.Current = Double.Parse(Console.ReadLine()); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(OhmsLaw.Value))
                        {
                            Console.Write("Enter Resistance --> ");
                            try { OhmsLaw.Value = Double.Parse(Console.ReadLine()); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(OhmsLaw.Power))
                        {
                            Console.Write("Enter Power --> ");
                            try { OhmsLaw.Power = Double.Parse(Console.ReadLine()); }
                            catch (Exception e) { }
                        }

                    }
                    PrintComponent(OhmsLaw);


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


                //Resistor testing = new Resistor() { Power = 125d };
                //PrintComponent(testing);

                //testing.Voltage = 50d;
                //PrintComponent(testing);

                //List<IComponent> myList = new List<IComponent> { new Resistor(), new LED() };
                //foreach (IComponent item in myList)
                //{
                //    PrintComponent(item);
                //}
                Console.ReadLine();
            }
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
    /*
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
    */

    public class Resistor : IComponent
    {
        //public double voltage, power, current;
        //double resistance;

        private int variablesSet = 0;

        private double voltage = Double.NaN;
        private double current = Double.NaN;
        private double resistance = Double.NaN;
        private double power = Double.NaN;

        //double voltage, current, power;

        public bool ValuesSet()
        {
            if (Double.IsNaN(voltage) || Double.IsNaN(current) || Double.IsNaN(resistance)  || Double.IsNaN(power)) return false;
            else return true;
        }


        public double Voltage { get { return voltage; }
            set
            {
                voltage = value;
                if (!Double.IsNaN(current)) //we have voltage and current
                {
                    resistance = voltage / current;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(resistance))//we have voltage and resistance
                {
                    current = voltage / resistance;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(power))//we have voltage and power
                {
                    resistance = (voltage * voltage) / power;
                    current = Math.Sqrt(power/resistance);
                }
                else
                {
                    Console.WriteLine("One more value required");
                }
            }
        }
        public double Current { get { return current; }
            set
            {
                current = value;
                if (!Double.IsNaN(voltage)) //we have voltage and current
                {
                    resistance = voltage / current;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(resistance))//we have current and resistance
                {
                    voltage = current * resistance;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(power))//we have current and power
                {
                    resistance = power / (current * current);
                    voltage = current * resistance;
                }
                else
                {
                    Console.WriteLine("One more value required");
                }

            }
        }
        public double Value { get { return resistance; }
            set
            {
                resistance = value;
                if (!Double.IsNaN(voltage)) //we have resistance and voltage
                {
                    current = voltage / resistance;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(current))//we have resistance and current
                {
                    voltage = current * resistance;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(power))//we have resistance and power
                {
                    current = Math.Sqrt(power / resistance);
                    voltage = current * resistance;
                }
                else
                {
                    Console.WriteLine("One more value required");
                }

            }
        }
        public double Power { get { return power; }
            set
            {
                power = value;
                if (!Double.IsNaN(voltage)) //we have power and voltage
                {
                    resistance = (voltage * voltage) / power;
                    current = Math.Sqrt(power / resistance);
                }
                else if (!Double.IsNaN(current))//we have power and current
                {
                    resistance = power / (current * current);
                    voltage = current * resistance;
                }
                else if (!Double.IsNaN(resistance))//we have power and resistance
                {
                    current = Math.Sqrt(power / resistance);
                    voltage = current * resistance;
                }
                else
                {
                    Console.WriteLine("One more value required");
                }

                // power = resistance * 
            }
        }

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
            Console.WriteLine("Voltage = " + voltage);
            Console.WriteLine("Current = " + current);
            Console.WriteLine("Resistance = " + resistance);
            Console.WriteLine("Power = " + power);

            //throw new NotImplementedException();
        }
    }

    public class LED : IComponent
    {

        public double Voltage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Current { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Power { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DisplayValues()
        {
            Console.WriteLine("LED");

            //if (Double.IsNaN(voltage))
            //if(voltage == null)
            {
                Console.WriteLine("idk");
            }

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
