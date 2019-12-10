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
                    Resistor OhmsLaw = new Resistor();
                    while (OhmsLaw.ValuesSet() == false)
                    {
                        if (Double.IsNaN(OhmsLaw.Voltage))
                        {
                            Console.Write("Enter Voltage --> ");
                            try { OhmsLaw.SetVoltage(Double.Parse(Console.ReadLine())); }// = Double.Parse(Console.ReadLine()); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(OhmsLaw.Current))
                        {
                            Console.Write("Enter Current --> ");
                            try { OhmsLaw.SetCurrent(Double.Parse(Console.ReadLine())); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(OhmsLaw.Value))
                        {
                            Console.Write("Enter Resistance --> ");
                            try { OhmsLaw.SetValue(Double.Parse(Console.ReadLine())); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(OhmsLaw.Power))
                        {
                            Console.Write("Enter Power --> ");
                            try { OhmsLaw.SetPower(Double.Parse(Console.ReadLine())); }
                            catch (Exception e) { }
                        }
                    }
                    PrintComponent(OhmsLaw);
                }
                else if (userInput == "2")
                {
                    Resistor resistor1 = new Resistor();
                    Resistor resistor2 = new Resistor();

                    double sourceVoltage = Double.NaN; 
                    while (resistor1.ValuesSet() == false || resistor2.ValuesSet() == false)
                    {
                        if (Double.IsNaN(sourceVoltage))
                        {
                            try
                            {
                                double input;

                                if (!Double.IsNaN(resistor1.Voltage) && !Double.IsNaN(resistor2.Voltage))
                                {
                                    sourceVoltage = resistor1.Voltage + resistor2.Voltage;
                                }
                                else
                                {
                                    Console.Write("Enter Source Voltage --> ");
                                    input = Double.Parse(Console.ReadLine());
                                    
                                    if (!Double.IsNaN(resistor1.Voltage) && Double.IsNaN(resistor2.Voltage))
                                    {
                                        if (input <= resistor1.Voltage)
                                        {
                                            Console.WriteLine("Voltage of source cannot be less than resistor voltage");
                                            throw new InvalidOperationException("Voltage of source cannot be less than resistor voltage");
                                        }
                                        resistor2.SetVoltage(input - resistor1.Voltage, resistor1, sourceVoltage);

                                    }
                                    else if (Double.IsNaN(resistor1.Voltage) && !Double.IsNaN(resistor2.Voltage))
                                    {
                                        if (input <= resistor2.Voltage)
                                        {
                                            Console.WriteLine("Voltage of source cannot be less than resistor voltage");
                                            throw new InvalidOperationException("Voltage of source cannot be less than resistor voltage");
                                        }
                                        resistor1.SetVoltage(input - resistor2.Voltage, resistor2, sourceVoltage);
                                    }
                                    else
                                    {
                                        sourceVoltage = input;
                                    }
                                }
                            }
                            catch (Exception e) { }
                        }

                        if (Double.IsNaN(resistor1.Voltage))
                        {
                            Console.Write("Enter Voltage (R1) --> ");
                            try {
                                double input = Double.Parse(Console.ReadLine());

                                if (!Double.IsNaN(sourceVoltage))
                                {
                                    if (input >= sourceVoltage)
                                    {
                                        Console.WriteLine("Voltage of resistor cannot be greater than Source Voltage");
                                        throw new InvalidOperationException("Voltage of resistor cannot be greater than Source Voltage");
                                    }
                                    resistor2.SetVoltage(sourceVoltage - input,resistor1, sourceVoltage);
                                }
                                resistor1.SetVoltage(input, resistor2, sourceVoltage);
                            }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor1.Current) && Double.IsNaN(resistor2.Current))
                        {
                            Console.Write("Enter Current of Circuit --> ");
                            try {
                                double input = Double.Parse(Console.ReadLine());

                                resistor1.SetCurrent(input, resistor2, sourceVoltage);
                                resistor2.SetCurrent(input, resistor1, sourceVoltage);

                            }
                            catch (Exception e) {  }
                        }
                        if (Double.IsNaN(resistor1.Value))
                        {
                            Console.Write("Enter Resistance (R1) --> ");
                            try
                            {
                                double input = Double.Parse(Console.ReadLine());

                                resistor1.SetValue(input, resistor2, sourceVoltage);

                                //if we need the voltage of r1, and currently have the source V aswell as R2, we can get it
                                if (Double.IsNaN(resistor1.Voltage) && !Double.IsNaN(sourceVoltage) && !Double.IsNaN(resistor2.Value))
                                {
                                    resistor1.SetVoltage(sourceVoltage * (resistor1.Value/(resistor1.Value+resistor2.Value)), resistor2, sourceVoltage);
                                }

                            }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor1.Power))
                        {
                            Console.Write("Enter Power (R1) --> ");
                            //try { resistor1.Power = Double.Parse(Console.ReadLine()); }
                            try { resistor1.SetPower(Double.Parse(Console.ReadLine()), resistor2, sourceVoltage); }
                            catch (Exception e) { }
                        }


                        if (Double.IsNaN(resistor2.Voltage))
                        {
                            Console.Write("Enter Voltage (R2) --> ");
                            try
                            {
                                double input = Double.Parse(Console.ReadLine());

                                if (!Double.IsNaN(sourceVoltage))
                                {
                                    if (input >= sourceVoltage)
                                    {
                                        throw new InvalidOperationException("Voltage of resistor cannot be greater than Source Voltage");
                                    }
                                    resistor1.SetVoltage(sourceVoltage - input, resistor2, sourceVoltage);
                                }
                                resistor2.SetVoltage(input, resistor1, sourceVoltage);
                            }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor2.Value))
                        {
                            Console.Write("Enter Resistance (R2) --> ");
                            try
                            {
                                double input = Double.Parse(Console.ReadLine());

                                resistor2.SetValue(input, resistor1, sourceVoltage);

                                //if we need the voltage of r1, and currently have the source V aswell as R2, we can get it
                                if (Double.IsNaN(resistor2.Voltage) && !Double.IsNaN(sourceVoltage) && !Double.IsNaN(resistor1.Value))
                                {
                                    resistor2.SetVoltage(sourceVoltage * (resistor2.Value / (resistor2.Value + resistor1.Value)), resistor1, sourceVoltage);
                                }

                            }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor2.Power))
                        {
                            Console.Write("Enter Power (R2) --> ");
                            //try { resistor2.Power = Double.Parse(Console.ReadLine()); }
                            try { resistor2.SetPower(Double.Parse(Console.ReadLine()), resistor1, sourceVoltage); }
                            catch (Exception e) { }
                        }
                    }
                    PrintComponent(resistor1);
                    PrintComponent(resistor2);
                }
                else if (userInput == "3")
                {
                    Resistor resistor = new Resistor();
                    LED led = new LED();

                    while (led.ValuesSet() == false || resistor.ValuesSet() == false)
                    {
                        if (Double.IsNaN(led.Voltage))
                        {
                            Console.Write("Enter Source Voltage --> ");
                            //try { resistor.SetVoltage(Double.Parse(Console.ReadLine())); }// = Double.Parse(Console.ReadLine()); }
                            try { led.SetVoltage(Double.Parse(Console.ReadLine()), resistor); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(led.Value))
                        {
                            Console.Write("Enter Voltage Drop --> ");
                            //try { resistor.SetVoltage(Double.Parse(Console.ReadLine())); }// = Double.Parse(Console.ReadLine()); }
                            try { led.SetValue(Double.Parse(Console.ReadLine()), resistor); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(led.Current))
                        {
                            Console.Write("Enter Current of Circuit --> ");
                            //try { resistor.SetVoltage(Double.Parse(Console.ReadLine())); }// = Double.Parse(Console.ReadLine()); }
                            try { led.SetCurrent(Double.Parse(Console.ReadLine()), resistor); }
                            catch (Exception e) { }
                        }
                    }
                    PrintComponent(resistor);
                    /*
                    while (resistor.ValuesSet() == false)
                    {
                        if (Double.IsNaN(resistor.Voltage))
                        {
                            Console.Write("Enter Voltage --> ");
                            try { resistor.SetVoltage(Double.Parse(Console.ReadLine())); }// = Double.Parse(Console.ReadLine()); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor.Current))
                        {
                            Console.Write("Enter Current --> ");
                            try { resistor.SetCurrent(Double.Parse(Console.ReadLine())); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor.Value))
                        {
                            Console.Write("Enter Resistance --> ");
                            try { resistor.SetValue(Double.Parse(Console.ReadLine())); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor.Power))
                        {
                            Console.Write("Enter Power --> ");
                            try { resistor.SetPower(Double.Parse(Console.ReadLine())); }
                            catch (Exception e) { }
                        }
                    }
                    PrintComponent(resistor);*/

                }
                else if (userInput == "4")
                {
                    Environment.Exit(0);
                }
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
    }

    public class Resistor : IComponent
    {

        private int variablesSet = 0;

        private double voltage = Double.NaN;
        private double current = Double.NaN;
        private double resistance = Double.NaN;
        private double power = Double.NaN;

        public bool ValuesSet()
        {
            if (Double.IsNaN(voltage) || Double.IsNaN(current) || Double.IsNaN(resistance)  || Double.IsNaN(power)) return false;
            else return true;
        }
        public void SetVoltage(double value)
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
                current = Math.Sqrt(power / resistance);
            }
            else
            {
                Console.WriteLine("One more value required");
            }
        }
        public void SetVoltage(double value, IComponent component,double sourceVoltage)
        {
            //Type type = typeof(IComponent);
            //Console.WriteLine(type);

            bool isLed = true;

            if (component == this) { throw new InvalidOperationException("component cannot be itself"); }

            try { LED led = (LED)component; }
            catch (InvalidCastException e) { isLed = false; }

            if (isLed)//is a led
            {
                LED led = (LED)component;


            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;

                voltage = value;

                //res.SetVoltage(current);


                if (!Double.IsNaN(current)) //we have voltage and current
                {
                    resistance = voltage / current;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(resistance))//we have voltage and resistance
                {
                    current = voltage / resistance;
                    res.SetCurrent(current, this, sourceVoltage);

                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(power))//we have voltage and power
                {
                    resistance = (voltage * voltage) / power;

                    current = Math.Sqrt(power / resistance);
                    res.SetCurrent(current, this, sourceVoltage);
                }
                else
                {
                    Console.WriteLine("One more value required");
                }
            }


            //try { Resistor res = (Resistor)component; }
            //catch { Console.WriteLine("nope"); }


        }
        public void SetCurrent(double value)
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
        public void SetCurrent(double value,IComponent component, double sourceVoltage)
        {
            bool isLed = true;

            if (component == this) { throw new InvalidOperationException("component cannot be itself"); }

            try { LED led = (LED)component; }
            catch (InvalidCastException e) { isLed = false; }

            if (isLed)//is a led
            {
                LED led = (LED)component;
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;

                current = value;

                //if (Double.IsNaN(res.current)) { res.SetCurrent(current, this); }
                //res.SetCurrent(current); 

                if (!Double.IsNaN(voltage)) //we have voltage and current
                {
                    resistance = voltage / current;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(resistance))//we have current and resistance
                {
                    voltage = current * resistance;
                    //if(Double.IsNaN(res.voltage))
                    if (!Double.IsNaN(sourceVoltage))
                    {
                        res.SetVoltage(sourceVoltage - voltage, this, sourceVoltage);
                    }


                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(power))//we have current and power
                {
                    resistance = power / (current * current);

                    voltage = current * resistance;
                    if (!Double.IsNaN(sourceVoltage))
                    {
                        res.SetVoltage(sourceVoltage - voltage, this, sourceVoltage);
                    }
                }
                else
                {
                    Console.WriteLine("One more value required");
                }
            }
        }
        public void SetValue(double value)
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
        public void SetValue(double value, IComponent component, double sourceVoltage)
        {
            bool isLed = true;

            if (component == this) { throw new InvalidOperationException("component cannot be itself"); }

            try { LED led = (LED)component; }
            catch (InvalidCastException e) { isLed = false; }

            if (isLed)//is a led
            {
                LED led = (LED)component;
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;

                //current = value;

                //if (Double.IsNaN(res.current)) { res.SetCurrent(current, this); }


                resistance = value;
                if (!Double.IsNaN(voltage)) //we have resistance and voltage
                {
                    current = voltage / resistance;
                    res.SetCurrent(current, this, sourceVoltage);

                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(current))//we have resistance and current
                {
                    voltage = current * resistance;
                    if (!Double.IsNaN(sourceVoltage))
                    {
                        res.SetVoltage(sourceVoltage - voltage, this, sourceVoltage);
                    }

                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(power))//we have resistance and power
                {
                    current = Math.Sqrt(power / resistance);
                    res.SetCurrent(current, this, sourceVoltage);

                    voltage = current * resistance;
                    if (!Double.IsNaN(sourceVoltage))
                    {
                        res.SetVoltage(sourceVoltage - voltage, this, sourceVoltage);
                    }
                }
                else
                {
                    Console.WriteLine("One more value required");
                }
            }
        }
        public void SetPower(double value)
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
        }
        public void SetPower(double value, IComponent component, double sourceVoltage)
        {
            bool isLed = true;

            if (component == this) { throw new InvalidOperationException("component cannot be itself"); }

            try { LED led = (LED)component; }
            catch (InvalidCastException e) { isLed = false; }

            if (isLed)//is a led
            {
                LED led = (LED)component;
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;

                //current = value;

                //if (Double.IsNaN(res.current)) { res.SetCurrent(current, this); }

                power = value;
                if (!Double.IsNaN(voltage)) //we have power and voltage
                {
                    resistance = (voltage * voltage) / power;

                    current = Math.Sqrt(power / resistance);
                    res.SetCurrent(current, this,sourceVoltage);

                }
                else if (!Double.IsNaN(current))//we have power and current
                {
                    resistance = power / (current * current);

                    voltage = current * resistance;
                    if (!Double.IsNaN(sourceVoltage))
                    {
                        res.SetVoltage(sourceVoltage - voltage, this, sourceVoltage);
                    }
                }
                else if (!Double.IsNaN(resistance))//we have power and resistance
                {
                    current = Math.Sqrt(power / resistance);
                    res.SetCurrent(current, this, sourceVoltage);

                    voltage = current * resistance;
                    if (!Double.IsNaN(sourceVoltage))
                    {
                        res.SetVoltage(sourceVoltage - voltage, this, sourceVoltage);
                    }
                }
                else
                {
                    Console.WriteLine("One more value required");
                }
            }
        }

        public double Voltage { get { return voltage; }}
        public double Current { get { return current; }}
        public double Value { get { return resistance; }}
        public double Power { get { return power; }}


        public void DisplayValues()
        {
            Console.WriteLine("Voltage = " + voltage);
            Console.WriteLine("Current = " + current);
            Console.WriteLine("Resistance = " + resistance);
            Console.WriteLine("Power = " + power);
        }
    }

    public class LED : IComponent
    {
        private double voltage = Double.NaN;
        private double current = Double.NaN;
        private double voltageDrop = Double.NaN;

        public double Voltage { get { return voltage; } }
        public double Current { get { return current; } }
        public double Value { get { return voltageDrop; } }
        public double Power { get { throw new NotImplementedException(); } }

        public bool ValuesSet()
        {
            if (Double.IsNaN(voltage) || Double.IsNaN(current) || Double.IsNaN(voltageDrop)) return false; //|| Double.IsNaN(power)) return false;
            else return true;
        }

        public void SetCurrent(double value, IComponent component, double sourceVoltage = Double.NaN)
        {
            Resistor res = (Resistor)component;

            current = value;
            //res.SetCurrent(current);
            if (!Double.IsNaN(voltage) && !Double.IsNaN(voltageDrop)) //we have voltage and current
            {
                res.SetVoltage(voltage - voltageDrop);
                res.SetCurrent(current);

                //resistance = voltage / current;
                //power = (voltage * voltage) / resistance;
            }
            //else
            //{
            //    Console.WriteLine("One more value required");
            //}
            //throw new NotImplementedException();
        }
        public void SetVoltage(double value, IComponent component, double sourceVoltage = Double.NaN)
        {
            Resistor res = (Resistor)component;

            //voltage = value;
            //res.SetCurrent(current);

            if (!Double.IsNaN(voltageDrop))
            {
                if (value <= voltageDrop)
                {
                    Console.WriteLine("Voltage of source cannot be less than voltage drop");
                    throw new InvalidOperationException("Voltage of source cannot be less than voltage drop");
                }
            }
            voltage = value;

            if (!Double.IsNaN(current) && !Double.IsNaN(voltageDrop)) //we have voltage and current
            {
                res.SetCurrent(current);
                res.SetVoltage(voltage - voltageDrop);
            }
                //throw new NotImplementedException();
        }
        public void SetValue(double value, IComponent component, double sourceVoltage = Double.NaN)
        {
            Resistor res = (Resistor)component;

            
            //res.SetCurrent(current);
            if (!Double.IsNaN(voltage))
            {
                if (value >= voltage)
                {
                    Console.WriteLine("Voltage Drop cannot be greater than source voltage");
                    throw new InvalidOperationException("Voltage of source cannot be less than voltage drop");
                }
            }
            voltageDrop = value;
            if (!Double.IsNaN(current) && !Double.IsNaN(voltage)) //we have voltage and current
            {
                res.SetCurrent(current);
                res.SetVoltage(voltage - voltageDrop);
            }

            //throw new NotImplementedException();
        }
        public void DisplayValues()
        {
            Console.WriteLine("LED");
            Console.WriteLine("Voltage = " + voltage);
            Console.WriteLine("Current = " + current);
            Console.WriteLine("Voltage Drop = " + voltageDrop);
            //Console.WriteLine("Power = " + power);
        }
        public void SetPower(double value)
        {
            throw new NotImplementedException();
        }

        public void SetPower(double value, IComponent component, double sourceVoltage = Double.NaN)
        {
            throw new NotImplementedException();
        }

        public void SetValue(double value)
        {
            throw new NotImplementedException();
        }
        public void SetVoltage(double value)
        {
            throw new NotImplementedException();
        }
        public void SetCurrent(double value)
        {
            throw new NotImplementedException();
        }
    }



    public interface IComponent
    {
            //void CalculateVoltage(double current,double resistance);

        bool ValuesSet();

        double Voltage { get; }//set; }
        double Current { get; }//set; }
        double Value { get; }//set; }
        double Power { get; }//set; }

        void SetVoltage(double value);
        void SetVoltage(double value,IComponent component, double sourceVoltage = Double.NaN);
        void SetCurrent(double value);
        void SetCurrent(double value,IComponent component, double sourceVoltage= Double.NaN);
        void SetValue(double value);
        void SetValue(double value,IComponent component, double sourceVoltage= Double.NaN);
        void SetPower(double value);
        void SetPower(double value,IComponent component, double sourceVoltage= Double.NaN);




        


        void DisplayValues();



        //void PrintComponent(IComponent)

        //void PrintComponent(ICo)


    }




}
