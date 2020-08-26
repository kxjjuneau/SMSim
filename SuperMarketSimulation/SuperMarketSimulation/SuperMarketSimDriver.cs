/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       SuperMarketSimDriver.cs
//  Description:    Driver class for the simulation of a supermarket
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
using PriorityQueue_Wiener;

namespace SuperMarketSimulation
{
    /// <summary>
    /// Driver class for the simulation of a supermarket
    /// </summary>
    class SuperMarketSimDriver
    {


        /// <summary>
        /// creates and instace of the supermarket class and calls its methods in order to run through
        /// the simulation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int choice = 1;

            Console.WriteLine("Welcome to Super Market Simulator. The defualt amount of registers in simulation is 7. It will proccess the events as fast as possible unless otherwise specified.\n");
            Console.Write("1.) Start simulation\n2.) choose number of registers\n3.) set time between event outputs (in millliseconds)\n4.) quit\nChoice: ");
            Int32.TryParse(Console.ReadLine(), out choice);
            Console.Clear();
            while (choice < 4)
            {
                if(choice == 3)
                {
                    Console.Write("Set Speed of events (in milliseconds): ");
                    Int32.TryParse(Console.ReadLine(), out SuperMarket.SleepTime);
                }
                else if(choice == 2)
                {
                    Console.Write(" How many registers do you want to simulate: ");
                    Int32.TryParse(Console.ReadLine(), out SuperMarket.NumOfRegisters);
                }
                else if (choice == 1)
                {
                    SuperMarket SuperMarketSim = new SuperMarket();
                    SuperMarketSim.GenerateCustomerEvents();
                    SuperMarketSim.DoSimulation();
                    Console.WriteLine(SuperMarketSim.ShowStats());                    
                }
                Console.Write("1.) Start simulation\n2.) Choose number of register\n3.) set time between event outputs (in millliseconds)\n4.) quit\nChoice: ");
                Int32.TryParse(Console.ReadLine(), out choice);
                Console.Clear();
            }
            Console.WriteLine("Thank you for useing the SuperMarket Simulator.\nPress Any key to exit...");
            Console.ReadKey();

        }

    
    }
}
