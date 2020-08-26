/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       Customers.cs
//  Description:    Customer Class wich contains a unique ID, a waiting interval and an event for the customer
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
    /// <summary>
    /// Customer Class wich contains a unique ID, a waiting interval and an event for the customer
    /// </summary>
    class Customers
    {
        private int customerID; 
        private TimeSpan waitTime;  // time that the customer will spend at the front of the register
        private Evnt customerEvent; 

        public Evnt CustomerEvent
        {
            get { return customerEvent; }
            set { customerEvent = value; }
        }


        public TimeSpan WaitTime
        {
            get { return waitTime; }
            set { waitTime = value; }
        }


        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        //initializes the Customer class to default values 
        public Customers()
        {
            CustomerID = 0;
            CustomerEvent = new Evnt();
        }

        /// <summary>
        /// converts a Customer Class to a string class using the customer ID
        /// </summary>
        /// <returns>string representing a Customer</returns>
        public override string ToString()
        {
            return CustomerID.ToString();
        }

    }
}
