/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//  Project:        Project 4: SuperMarket Simulation
//  Filename:       Utility.cs
//  Description:    Contains common Utility methods usefull for multiple programs
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

namespace UtilityNameSpace
{
    class Utility
    {
        private static Random r = new Random();

        /// <summary>
        /// Welcome Message for all projects
        /// </summary>
        /// <param name="ProgramName">String: Name of current Program</param>
        /// <param name="AssignmentName">String: current assignmnent</param>
        /// <returns>string: used to print welcome message</returns>
        public static string Welcome(string ProgramName, string AssignmentName)
        {
            return $"Welcome to {ProgramName}, By Joseph Juneau (juneau@etsu.edu), for CSCI-2210. Assignment: {AssignmentName}";
        }

        /// <summary>
        /// Takes a string and capitolizes the first letter of every word
        /// </summary>
        /// <param name="str">string to capitolize</param>
        /// <returns>returns the newly formatted string</returns>
        public static string CapitolizeWord(string str)
        {
            str = str.ToLower(); // lower all chars
            str = char.ToUpper(str[0]) + str.Substring(1);// capitlize the first char
            StringBuilder CharToReplace = new StringBuilder(str);
            int i = 0;
            do // capitolizes every char after a space
            {
                i++;
                i = str.IndexOf(' ', i);
                if (i < str.Length - 1 && i != -1)
                {
                    CharToReplace[i + 1] = char.ToUpper(str[i + 1]);
                    str = CharToReplace.ToString();
                }

            } while (i != -1 && i < str.Length);

            return str;
        }

        /// <summary>
        /// Exit message for program
        /// </summary>
        public static void ExitMessage(string PrgName)
        {
            Console.WriteLine($"Thank you for using the {PrgName}. Goodbye\n");
        }

        /// <summary>
        /// Tokenizes a string into a list of strings
        /// </summary>
        /// <param name="text">String to be tokenized into list</param>
        /// <returns>returns newly tokenized list</returns>
        public static List<string> Tokenize(string text)
        {
            List<string> TokenStrings;
            string[] textarray; // gets tokenenized string into string array
            string TempString;  // used to analyze contents of string
            char[] TokenSymbols = { '.', ',', '?', '!', '\n', '\r' }; // extract Delimeters from string and add to List
            int LastIndexOfString;// = Source.LastIndexOf(Find);

            textarray = text.Split(' ');
            TokenStrings = new List<string>(textarray);
            for (int i = 0; i < TokenStrings.Count; i++)
            {
                if (TokenSymbols.Any(x => TokenStrings[i].Contains(x.ToString())) && TokenStrings[i].Length > 1)
                {
                    // checks to see if there is a newline in the middle of sting, if there is also a \n at end it will first go to the last else statement and tokenize the last \n before doing the middle one
                    if (TokenStrings[i].Substring(1, TokenStrings[i].Length - 2).Contains("\n") && !TokenStrings[i].EndsWith("\n"))
                    {
                        TempString = TokenStrings[i].Substring(TokenStrings[i].LastIndexOf("\n") + 1, TokenStrings[i].Length - TokenStrings[i].LastIndexOf("\n") - 1);
                        TokenStrings.Insert(i + 1, TempString);
                        TokenStrings[i] = TokenStrings[i].Replace(TempString, "");
                    }
                    // same proccess as newline character check
                    else if (TokenStrings[i].Substring(1, TokenStrings[i].Length - 2 ).Contains("\r") && !TokenStrings[i].EndsWith("\r"))
                    {
                        TempString = TokenStrings[i].Substring(TokenStrings[i].LastIndexOf("\r"), TokenStrings[i].Length - TokenStrings[i].LastIndexOf("\r") );
                        TokenStrings.Insert(i + 1, TempString);
                        LastIndexOfString = TokenStrings[i].LastIndexOf("\r");
                        TokenStrings[i] = TokenStrings[i].Substring(0, LastIndexOfString);    
                    }
                    else
                    {
                        TokenStrings.Insert(i + 1, TokenStrings[i][TokenStrings[i].IndexOfAny(TokenSymbols)].ToString()); //insert at location after symbol found using the strings last charcter to insert
                        if (TokenStrings[i] != "\r\n")
                            TokenStrings[i] = TokenStrings[i].TrimEnd(TokenSymbols);
                        else
                            TokenStrings[i] = "\n";
                    }
                    //needs to recheck token incase of any repeated delimiters
                    i--;
                }
            }


            return TokenStrings;
        }

        /// <summary>
        /// returns a number based on the expected value
        /// using a negative exponential distribution model.
        /// </summary>
        /// <param name="ExpectedValue"></param>
        /// <returns></returns>
        public static double NegExp(double ExpectedValue)
        {
            return -ExpectedValue * Math.Log(r.NextDouble(), Math.E);
        }

        /// <summary>
        /// returns a number based off of the expected value of lambda
        /// using the poisson Distribution method.
        /// This method is based off of knuth's poisson algorithm.
        /// </summary>
        /// <param name="lambda">Expected Value</param>
        /// <returns>A number relativley close to the expected value</returns>
        public static int PoissonDistribution(double lambda)
        {
            double Limit = Math.Exp(-lambda);
            double p = r.NextDouble();
            int k = 0;

            do
            {
                k++;
                p *= r.NextDouble();
            } while (p > Limit);
            return k;

        }




    }
}
