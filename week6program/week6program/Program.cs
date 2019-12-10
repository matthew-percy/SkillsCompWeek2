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

                if (userInput == "1") //Ohms Law
                {
                    Resistor OhmsLaw = new Resistor();
                    while (OhmsLaw.ValuesSet() == false) //keep going forever until 2 values have been sent to the resistor
                    {
                        if (Double.IsNaN(OhmsLaw.Voltage)) //ask for voltage if the voltage doesnt already exist
                        {
                            Console.Write("Enter Voltage --> ");
                            try { OhmsLaw.SetVoltage(Double.Parse(Console.ReadLine())); }
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
                else if (userInput == "2")//Voltage Divider Rule
                {
                    Resistor resistor1 = new Resistor();
                    Resistor resistor2 = new Resistor();

                    double sourceVoltage = Double.NaN; 
                    while (resistor1.ValuesSet() == false || resistor2.ValuesSet() == false) //keep going forever until 3 values have been sent
                    {
                        if (Double.IsNaN(sourceVoltage)) 
                        {
                            try
                            {
                                double input;

                                if (!Double.IsNaN(resistor1.Voltage) && !Double.IsNaN(resistor2.Voltage)) //if we have the voltage of both resistors, calculate the source voltage
                                {
                                    sourceVoltage = resistor1.Voltage + resistor2.Voltage; //E = Vr1 + Vr2
                                }
                                else
                                {
                                    Console.Write("Enter Source Voltage --> ");
                                    input = Double.Parse(Console.ReadLine());
                                    
                                    if (!Double.IsNaN(resistor1.Voltage) && Double.IsNaN(resistor2.Voltage)) //if we have voltage of R1, but not R2
                                    {
                                        if (input <= resistor1.Voltage) //make sure source voltage is greater than resistors voltage
                                        {
                                            Console.WriteLine("Voltage of source cannot be less than resistor voltage");
                                            throw new InvalidOperationException("Voltage of source cannot be less than resistor voltage");
                                        }
                                        resistor2.SetVoltage(input - resistor1.Voltage, resistor1, sourceVoltage);//Vr2 = E - Vr1

                                    }
                                    else if (Double.IsNaN(resistor1.Voltage) && !Double.IsNaN(resistor2.Voltage))//if we have voltage of R2, but not R1
                                    {
                                        if (input <= resistor2.Voltage)//make sure source voltage is greater than resistors voltage
                                        {
                                            Console.WriteLine("Voltage of source cannot be less than resistor voltage");
                                            throw new InvalidOperationException("Voltage of source cannot be less than resistor voltage");
                                        }
                                        resistor1.SetVoltage(input - resistor2.Voltage, resistor2, sourceVoltage);//Vr1 = E - Vr2
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
                                    if (input >= sourceVoltage) //make sure resistors voltage is less than source voltage
                                    {
                                        Console.WriteLine("Voltage of resistor cannot be greater than Source Voltage");
                                        throw new InvalidOperationException("Voltage of resistor cannot be greater than Source Voltage");
                                    }
                                    resistor2.SetVoltage(sourceVoltage - input,resistor1, sourceVoltage);//Vr2 = E - Vr1
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

                                resistor1.SetCurrent(input, resistor2, sourceVoltage);//current is same in series, therefore we set both together
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

                                //if we need the voltage of r1, and currently have the source V aswell as R2, we can use voltage divider rule
                                if (Double.IsNaN(resistor1.Voltage) && !Double.IsNaN(sourceVoltage) && !Double.IsNaN(resistor2.Value))
                                {
                                    resistor1.SetVoltage(sourceVoltage * (resistor1.Value/(resistor1.Value+resistor2.Value)), resistor2, sourceVoltage);
                                    //Vr1 = E * (r1/r1+r2)
                                }

                            }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor1.Power))
                        {
                            Console.Write("Enter Power (R1) --> ");
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
                                    if (input >= sourceVoltage)//make sure resistors voltage is less than source voltage
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

                                //if we need the voltage of r2, and currently have the source V aswell as R1, we can use voltage divider rule
                                if (Double.IsNaN(resistor2.Voltage) && !Double.IsNaN(sourceVoltage) && !Double.IsNaN(resistor1.Value))
                                {
                                    resistor2.SetVoltage(sourceVoltage * (resistor2.Value / (resistor2.Value + resistor1.Value)), resistor1, sourceVoltage);
                                    //Vr2 = E * (r2/r1+r2)
                                }
                            }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(resistor2.Power))
                        {
                            Console.Write("Enter Power (R2) --> ");
                            try { resistor2.SetPower(Double.Parse(Console.ReadLine()), resistor1, sourceVoltage); }
                            catch (Exception e) { }
                        }
                    }
                    PrintComponent(resistor1);
                    PrintComponent(resistor2);
                }
                else if (userInput == "3") //LED limiting resistor
                {
                    Resistor resistor = new Resistor();
                    LED led = new LED();

                    while (led.ValuesSet() == false || resistor.ValuesSet() == false)//keep going forever until all LED values are sent
                    {
                        if (Double.IsNaN(led.Voltage))
                        {
                            Console.Write("Enter Source Voltage --> ");
                            try { led.SetVoltage(Double.Parse(Console.ReadLine()), resistor); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(led.Value))
                        {
                            Console.Write("Enter Voltage Drop --> ");
                            try { led.SetValue(Double.Parse(Console.ReadLine()), resistor); }
                            catch (Exception e) { }
                        }
                        if (Double.IsNaN(led.Current))
                        {
                            Console.Write("Enter Current of Circuit --> ");
                            try { led.SetCurrent(Double.Parse(Console.ReadLine()), resistor); }
                            catch (Exception e) { }
                        }
                    }
                    PrintComponent(resistor);
                    PrintComponent(led);
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
                //Console.WriteLine("One more value required");
            }
        }
        public void SetVoltage(double value, IComponent component,double sourceVoltage)
        {

            bool isLed = true;

            if (component == this) { throw new InvalidOperationException("component cannot be itself"); }

            try { LED led = (LED)component; }
            catch (InvalidCastException e) { isLed = false; }

            if (isLed)//is a led
            {
                throw new NotImplementedException();
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;
                voltage = value;

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
                    //Console.WriteLine("One more value required");
                }
            }
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
                //Console.WriteLine("One more value required");
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
                throw new NotImplementedException();
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;
                current = value;

                if (!Double.IsNaN(voltage)) //we have voltage and current
                {
                    resistance = voltage / current;
                    power = (voltage * voltage) / resistance;
                }
                else if (!Double.IsNaN(resistance))//we have current and resistance
                {
                    voltage = current * resistance;

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
                    //Console.WriteLine("One more value required");
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
                //Console.WriteLine("One more value required");
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
                throw new NotImplementedException();
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;
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
                    //Console.WriteLine("One more value required");
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
                //Console.WriteLine("One more value required");
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
                throw new NotImplementedException();
            }
            else //is a resistor
            {
                Resistor res = (Resistor)component;
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
                    //Console.WriteLine("One more value required");
                }
            }
        }

        public double Voltage { get { return voltage; }}
        public double Current { get { return current; }}
        public double Value { get { return resistance; }}
        public double Power { get { return power; }}

        public void DisplayValues()
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("RESISTOR");
            Console.WriteLine("Voltage = " + voltage);
            Console.WriteLine("Current = " + current);
            Console.WriteLine("Resistance = " + resistance);
            Console.WriteLine("Power = " + power);
            Console.ForegroundColor = defaultColor;
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

            if (!Double.IsNaN(voltage) && !Double.IsNaN(voltageDrop)) //we have all necessary values
            {
                res.SetVoltage(voltage - voltageDrop);
                res.SetCurrent(current);
            }
        }
        public void SetVoltage(double value, IComponent component, double sourceVoltage = Double.NaN)
        {
            Resistor res = (Resistor)component;

            if (!Double.IsNaN(voltageDrop))
            {
                if (value <= voltageDrop)//ensure voltage drop is lower than source voltage
                {
                    Console.WriteLine("Voltage of source cannot be less than voltage drop");
                    throw new InvalidOperationException("Voltage of source cannot be less than voltage drop");
                }
            }
            voltage = value;

            if (!Double.IsNaN(current) && !Double.IsNaN(voltageDrop)) //we have all necessary values
            {
                res.SetCurrent(current);
                res.SetVoltage(voltage - voltageDrop);
            }
        }
        public void SetValue(double value, IComponent component, double sourceVoltage = Double.NaN)
        {
            Resistor res = (Resistor)component;

            if (!Double.IsNaN(voltage)) //ensure voltage drop is lower than source voltage
            {
                if (value >= voltage)
                {
                    Console.WriteLine("Voltage Drop cannot be greater than source voltage");
                    throw new InvalidOperationException("Voltage of source cannot be less than voltage drop");
                }
            }
            voltageDrop = value;
            if (!Double.IsNaN(current) && !Double.IsNaN(voltage)) //we have all necessary values
            {
                res.SetCurrent(current);
                res.SetVoltage(voltage - voltageDrop);
            }
        }
        public void DisplayValues()
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("LED");
            Console.WriteLine("Voltage = " + voltage);
            Console.WriteLine("Current = " + current);
            Console.WriteLine("Voltage Drop = " + voltageDrop);
            Console.ForegroundColor = defaultColor;
        }
        public void SetPower(double value){throw new NotImplementedException();} //unused functions from interface
        public void SetPower(double value, IComponent component, double sourceVoltage = Double.NaN){throw new NotImplementedException();}
        public void SetValue(double value){ throw new NotImplementedException();}
        public void SetVoltage(double value){throw new NotImplementedException();}
        public void SetCurrent(double value){ throw new NotImplementedException();}
    }

    public interface IComponent
    {
        bool ValuesSet();

        double Voltage { get; }
        double Current { get; }
        double Value { get; }
        double Power { get; }

        void SetVoltage(double value); 
        void SetVoltage(double value,IComponent component, double sourceVoltage = Double.NaN);//these overloaded versions of each method are used for part 2 (Voltage Divider Rule) and part 3 (LED limiting resistor)
                                                                                              //the IComponent variable allows us to call the functions of the other component in the circuit, sourceVoltage is only used in part2 due to the class having no parameter for source voltage

        void SetCurrent(double value);
        void SetCurrent(double value,IComponent component, double sourceVoltage= Double.NaN);
        void SetValue(double value);
        void SetValue(double value,IComponent component, double sourceVoltage= Double.NaN);
        void SetPower(double value);
        void SetPower(double value,IComponent component, double sourceVoltage= Double.NaN);

        void DisplayValues();
    }
}
