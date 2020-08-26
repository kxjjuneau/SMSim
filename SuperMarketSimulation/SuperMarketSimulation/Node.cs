/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       Node.cs
//  Description:    Node class wich contains a Node of Type T and a next pointer
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

namespace Node
{
    /// <summary>
    /// Node class wich contains a Node of Type T and a next pointer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> where T: IComparable
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }

        /// <summary>
        /// creates a new node with a passed item and Next Node in list/queue
        /// </summary>
        /// <param name="value">Item of the New Node</param>
        /// <param name="link">Node which the new node will be linked to</param>
        public Node(T value, Node<T> link)
        {
            Item = value;
            Next = link;
        }
    }
}
