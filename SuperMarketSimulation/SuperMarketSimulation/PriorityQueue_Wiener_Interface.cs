/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       PriorityQueue_Wiener_interfaces.cs
//  Description:    Interface that requires, clear, IsEmpty, and Count to be implemented
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


namespace PriorityQueue_Wiener
{
    /// <summary>
    /// Interface that requires, clear, IsEmpty, and Count to be implemented
    /// </summary>
    /// <typeparam name="T">type of object</typeparam>
    public interface IContainer<T>
    {
        void Clear();
        bool IsEmpty();
        int Count { get; set; }
    }

    /// <summary>
    /// interface that requires, enqueue, dequeue, and peek to be implemented.
    /// used in Wiener's Priority queue.
    /// </summary>
    /// <typeparam name="T">type of object</typeparam>
    public interface IPriorityQueue<T> : IContainer<T> where T : IComparable
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }
}