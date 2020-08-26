/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       SuperMarket.cs
//  Description:    handles the Generation, simulation and statistics involved with the Super Market simulator.
//  Course:         CSCI 2210-001 - Data Sctructures
//  Author:         Joseph Juneau (juneau@etsu.edu)
//  Created:        4/15/2018
//
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using PriorityQueue_Wiener;
using UtilityNameSpace;

namespace SuperMarketSimulation
{
    /// <summary>
    /// handles the Generation, simulation and statistics involved with the Super Market simulator.
    /// </summary>
    class SuperMarket
    {
        private static int NumCustomers = 600;                                  // Number of customers
        private static double HoursOfOperation = 16;                            // Total Hours of operation
        public static int NumOfRegisters = 7;                                  // Total Number of registers in store
        private static DateTime TimeWeOpen = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 8, 0, 0);// Opens at 8:00 am
        private static DateTime TimeWeClose = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);//closes at 12am
        private static double ExpectedStayTime = 345;                           // expected stay time in seconds
        private static PriorityQueue<Evnt> PQ = new PriorityQueue<Evnt>();      // priority queue order by time of event
        private static Random r = new Random();
        private static TimeSpan AvgWaitTime = new TimeSpan(0, 0, 0);            // the average wait time for the cutomers to be served
        private static TimeSpan MinWaitTime = new TimeSpan(0, 1000, 0);         // the fastest service time
        private static TimeSpan MaxWaitTime = new TimeSpan(0, 0, 0);            // the longest service time
        public static int SleepTime = 0;                                        // time in milliseconds to pause after each event is displayed

        private Customers customer;    //Customers containing an evnt class, wait time interval, and customerID


        public Customers Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        /// <summary>
        /// generates a new Customer with an Id, Event class, and wait time interval
        /// </summary>
        public SuperMarket()
        {
            Customer = new Customers();
        }

        /// <summary>
        /// generates enter venets for each customer arriving at the store based on poisson distribution
        /// </summary>
        public void GenerateCustomerEvents()
        {
            TimeSpan start;         //random start time in wich enter events are based
            int HoursInSeconds = 0; //hours in seconds at which the customer arrives
            int Patron = 0;         //customer id
            NumCustomers = Utility.PoissonDistribution(NumCustomers);   //number of customers arriving today

            for (Patron = 1; Patron <= NumCustomers; Patron++)
            {
                HoursInSeconds = (int)(r.NextDouble() * HoursOfOperation * 60*60);//convert random number between 0-1 to seconds in an hour < 16 hours
                start = new TimeSpan(0, 0, HoursInSeconds); //generate timespan with the arrival of the customer in seconds

                PQ.Enqueue(new Evnt(EVENTTYPE.ENTER, TimeWeOpen.Add(start), Patron));   // enqueue customer based on arrival time

            }



        }

        /// <summary>
        /// handles the simulation aspect of the class. creates register lines, fills the List of queues
        /// with customers and dequeues them at the proper time
        /// </summary>
        public void DoSimulation()
        {
            int SmallestLine = 0;   //reference to the smalles register line in order to place the customer in the correct queue
            Customers CustomerTemp = new Customers(); //holds the current customer and their event attached to them
            List<Queue<Customers>> RegisterLine = new List<Queue<Customers>>(); // list of all the registers. number of registers can be changed at the properties section of the class
            int LineIndexOfCustomer; //index of the line the the customer who is leaving is in. ex: customer 5 is leaving and he is in register 2. 2 = LineIndexOfCustomer
            // Events proccessed = total events procceses. Arrivals = total number of arrivals. Departures = total amount of departures. Maxqueue = largest queue found
            int EventsProccessed = 0, Arrivals = 0, Departures = 0, MaxQueue = 0;
            
            //if registers would wordwrap in console, extend console width with relation to the amount of registers
            if(NumOfRegisters*15>75)
                Console.WindowWidth = NumOfRegisters*15;
            
            //generate the list of queues and print them at top of console
            for (int i = 0; i < NumOfRegisters; i++)
            {
                RegisterLine.Add(new Queue<Customers>());
                Console.Write("REGISTER {0}", i.ToString().PadRight(5));
            }

            //while still events in priority queue
            while (PQ.Count > 0)
            {
                
                CustomerTemp = new Customers(); //generate new customer template
                CustomerTemp.CustomerEvent = PQ.Dequeue(); // dequeue event into Customer temp
                CustomerTemp.CustomerID = CustomerTemp.CustomerEvent.Customer;

                //generate a wait time interval for customer based on negative exponential distribution.
                CustomerTemp.WaitTime = new TimeSpan(0, 0, (int)(120 + Utility.NegExp(ExpectedStayTime-120)));
                
                if (CustomerTemp.CustomerEvent.Type == EVENTTYPE.ENTER)
                {
                    //if customer enters queue after closing hours
                    if (CustomerTemp.CustomerEvent.Time > TimeWeClose && CustomerTemp.CustomerEvent.Time < TimeWeOpen)
                    {
                        continue;
                    }
                    else
                    {
                        SmallestLine = MinRegisterLine(RegisterLine); // finds smalles queue
                        RegisterLine[SmallestLine].Enqueue(CustomerTemp);


                        //if only customer in queue, generate leave event on entering of queue
                        if (RegisterLine[SmallestLine].Count == 1)
                        {
                            PQ.Enqueue(new Evnt(EVENTTYPE.LEAVE, RegisterLine[SmallestLine].Peek().CustomerEvent.Time.Add(RegisterLine[SmallestLine].Peek().WaitTime), CustomerTemp.CustomerEvent.Customer));
                        }
                        //if the simulation output will overwrite the statistic output, move the output down a line.
                        if (MaxQueue < RegisterLine[SmallestLine].Count)
                        {
                            MaxQueue = RegisterLine[SmallestLine].Count;
                            Console.MoveBufferArea(0, MaxQueue + 2, 61, 5, 0, MaxQueue + 3);
                        }
                        Arrivals++;
                        Console.SetCursorPosition(SmallestLine * 15, RegisterLine[SmallestLine].Count + 1); //set cursor under the queue wich the customer eneters
                        Console.Write(CustomerTemp.CustomerID.ToString("000"));
                    }
                }
                else if (CustomerTemp.CustomerEvent.Type == EVENTTYPE.LEAVE)
                {
                    LineIndexOfCustomer = FindCustomerInLine(RegisterLine, CustomerTemp.CustomerID); // find customer who is leaving
                    if (LineIndexOfCustomer != -1) // if found
                    {

                        RegisterLine[LineIndexOfCustomer].Dequeue();
                        //if queue is not empty of one customer leaving
                        if (RegisterLine[LineIndexOfCustomer].Count > 0)
                        {
                            //add event containt LEAVE, a Date Time based on temp customers leave time and the id of the customer first in line in the smalles register line
                            PQ.Enqueue(new Evnt(EVENTTYPE.LEAVE, CustomerTemp.CustomerEvent.Time.Add(RegisterLine[LineIndexOfCustomer].Peek().WaitTime), RegisterLine[LineIndexOfCustomer].Peek().CustomerEvent.Customer));
                        }

                    }
                    Departures++;
                    Console.SetCursorPosition(LineIndexOfCustomer * 15, 2); // set cursor position to the position of the customer leaving
                    Console.Write("   ");
                    //copy contents below the leaving customer and move them up 1
                    Console.MoveBufferArea(LineIndexOfCustomer * 15, 3, 3, RegisterLine[LineIndexOfCustomer].Count, LineIndexOfCustomer * 15, 2);
                    AvgWaitTime += CustomerTemp.WaitTime;

                    if (MinWaitTime > CustomerTemp.WaitTime)
                        MinWaitTime = CustomerTemp.WaitTime;
                    if (MaxWaitTime < CustomerTemp.WaitTime)
                        MaxWaitTime = CustomerTemp.WaitTime;
                }

                EventsProccessed++;
                Console.SetCursorPosition(0, MaxQueue + 3); //update bottom statistics 
                Console.WriteLine("Longest Queue Encountered So Far: {0}\n", MaxQueue);
                Console.WriteLine("Events Processed So Far: {0} Arrivals: {1} Departures: {2}", EventsProccessed.ToString().PadLeft(4), Arrivals.ToString().PadLeft(4), Departures.ToString().PadLeft(4));
                if(SleepTime > 0)
                    Thread.Sleep(SleepTime);
            }
            AvgWaitTime = new TimeSpan(0, 0, (int)(AvgWaitTime.TotalSeconds / Arrivals));

            
        }

        /// <summary>
        /// format statistics pretaning to the simulation.
        /// displays Number of Customers, Average Wait Time, Minimum wait time, and maximum wait time.
        /// </summary>
        /// <returns>A string which holds the statistics of the simulation</returns>
        public string ShowStats()
        {
            string StatString = $"The Average service time for {NumCustomers} customers was: {AvgWaitTime.ToString()}\n";
            StatString += $"The minimum service time was {MinWaitTime.ToString()} and the maximum service time was {MaxWaitTime.ToString()}";
            return StatString;
        }

        /// <summary>
        /// Finds a customer at the front of a register based off of their ID
        /// </summary>
        /// <param name="RegisterLine">the register list tha holds the information of the customers in the queues</param>
        /// <param name="CustomerID">Customer to find</param>
        /// <returns>the index of the register if customer has been found, else returns -1</returns>
        private static int FindCustomerInLine(List<Queue<Customers>> RegisterLine, int CustomerID)
        {
            for (int i = 0; i < RegisterLine.Count; i++)
            {
                //if not customers in the queue, check next queue
                if (RegisterLine[i].Count == 0)
                    continue;
                else if (CustomerID == RegisterLine[i].Peek().CustomerID)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// finds the index of the smalles register queue in oreder to place
        /// a customer in that queue. works from left to right, so if all registers are
        /// equal, the first one will be considered the smallest
        /// </summary>
        /// <param name="RegisterLine">List of registers to check count</param>
        /// <returns>index of smallest register line</returns>
        private static int MinRegisterLine(List<Queue<Customers>> RegisterLine)
        {
            int IndexOfSmallest = 0;
            for (int i = 1; i < RegisterLine.Count; i++)
            {
                if (RegisterLine[IndexOfSmallest].Count > RegisterLine[i].Count)
                    IndexOfSmallest = i;
            }
            return IndexOfSmallest;
        }

    }
}
