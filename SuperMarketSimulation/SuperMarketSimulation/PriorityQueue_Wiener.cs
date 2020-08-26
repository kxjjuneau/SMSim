/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       PriorityQueue_Wiener.cs
//  Description:    Priority Queue based of of Wiener's model
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
using Node;

namespace PriorityQueue_Wiener
{
    /// <summary>
    /// Priority Queue based of of Wiener's model
    /// </summary>
    /// <typeparam name="T">Object type that will be placed in the queue</typeparam>
    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable
    {
        
        private Node<T> top;

        public int Count { get; set; }

        /// <summary>
        /// Enqueues an item into the priority queue based off 
        /// of the results from IComparable
        /// </summary>
        /// <param name="item">Item to place in the priority Queue</param>
        public void Enqueue(T item)
        {

            if (Count == 0)
                top = new Node<T>(item, null);
            else
            {
                Node<T> current = top;
                Node<T> previous = null;

                while(current != null && current.Item.CompareTo(item) >= 0)
                {
                    previous = current;
                    current = current.Next;
                }

                Node<T> newNode = new Node<T>(item, current);

                if(previous != null)
                {
                    previous.Next = newNode;
                }
                else
                {
                    top = newNode;
                }
            }
            Count++;
        }

        /// <summary>
        /// dequeues an item from the priority queue
        /// </summary>
        /// <returns>the item that was dequeued</returns>
        public T Dequeue()
        {
            if(IsEmpty())
            {
                throw new InvalidOperationException("Cannot remove from empty queue.");
            }
            else
            {
                Node<T> oldNode = top;
                top = top.Next;
                Count--;
                return oldNode.Item;
            }
        }

        /// <summary>
        /// clears the entire priority queue
        /// </summary>
        public void Clear()
        {
            top = null;
            Count = 0;
        }

        /// <summary>
        /// returns the top item from the queue without dequeuing it
        /// </summary>
        /// <returns>The top item of the queue</returns>
        public T Peek()
        {
            if (!IsEmpty())
                return top.Item;
            else
                throw new InvalidOperationException("Cannot obtain top of empty priority queue.");
        }

        /// <summary>
        /// checks to see if the priority queue is empty
        /// </summary>
        /// <returns>true if queue is empty</returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

    }

}
