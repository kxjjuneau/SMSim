/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       Evnt.cs
//  Description:    Event class for customers
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

namespace SuperMarketSimulation
{

    enum EVENTTYPE { ENTER, LEAVE};
    /// <summary>
    /// Event class for customers
    /// </summary>
    class Evnt : IComparable
    {

        public EVENTTYPE Type { get; set; }
        public DateTime Time { get; set; }  //time of event trigger in Date Format
        public int Customer;                // customer ID of event

        //initializes a defualt event
        public Evnt()
        {
            Type = EVENTTYPE.ENTER;
            Time = DateTime.Now;
            Customer= -1;
        }

        /// <summary>
        /// initializes an event with the data passed in.
        /// </summary>
        /// <param name="type">The type of Event: ENTER or LEAVE</param>
        /// <param name="time">The Time of Event</param>
        /// <param name="customer">Customer associated with event</param>
        public Evnt(EVENTTYPE type, DateTime time, int customer)
        {
            Type = type;
            Time = time;
            Customer = customer;
        }

        /// <summary>
        /// prints out customer ID
        /// </summary>
        /// <returns>string holding the ID of the Customer attached to event</returns>
        public override string ToString()
        {
            return Customer.ToString();
        }

        /// <summary>
        /// Compares events based on time of event
        /// </summary>
        /// <param name="obj">should be an Evnt object in order to compare it to another Evnt</param>
        /// <returns>integer signinfying if an Evnt is less than, greater than, or Equal to another Evnt</returns>
        public int CompareTo(Object obj)
        {
            if (!(obj is Evnt))
                throw new ArgumentException("The argument is not a CustomerEvent object");
            Evnt CustomerCompare = (Evnt)obj;
            return (CustomerCompare.Time.CompareTo(Time));
        }
    }
}
