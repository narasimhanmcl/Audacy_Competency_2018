﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Audacy_Competency_2018
{
    public class Audacy_Business_Logic
    {
        public static void Main(string[] args)
        {
            initializeApp();
            List<string> invalidInputList = new List<string>();
            string wholeInputText = null;            

            //Get the input file location from the user
            string inputFileLocation = Console.ReadLine();
            try
            {
                //Check if the input file exists in the location specified
                if (File.Exists(inputFileLocation))
                {
                    if (Regex.IsMatch(inputFileLocation, ".*.txt"))
                    {
                        wholeInputText = System.IO.File.ReadAllText(@inputFileLocation);
                        if (wholeInputText != null)
                        {
                            //Split and add the entires as individual line items
                            String[] inputArray = Regex.Split(wholeInputText, "\\r\\n");
                            Console.WriteLine("Output is: ");
                            for (int index = 0; index < inputArray.Count(); index++)
                            {

                                //Remove any spaces present in the input
                                string currentString = inputArray[index].Contains(" ") ?
                                    inputArray[index].Replace(" ", "") : inputArray[index];

                                //Check if the input line is a valid input line
                                if (!isValidInput(inputArray[index]))
                                {
                                    //Add all invalid entries to a list
                                    invalidInputList.Add(inputArray[index]);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Input file is empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid file format. Please use *.txt format file as input.");
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist in the location specified. Please input the exact file location and try again.");
                }
            } catch (Exception e){
                Console.WriteLine("Exception occurred with error message: " + e.Message);
            }
            closeApp(invalidInputList);
        }

        public static bool isValidInput(string inputString)
        {
            //Remove any spaces present in the input
            string currentString = inputString.Contains(" ") ?
                inputString.Replace(" ", "") : inputString;
            
            //Check if the input string passes the provided condition
            int distinctNumberCount = new String(currentString.Distinct().ToArray()).Length;

            //each line being (n * 3 + (n-1)) characters long, where `n` is the count of numbers in a particular line where each line has numbers only
            if ((Regex.IsMatch(currentString, "\\d+")) &&
                currentString.Length.Equals(distinctNumberCount * 3 + (distinctNumberCount - 1)))
            {
                Console.WriteLine(currentString);
                return true;
            }
            return false;
        }

        private static void initializeApp()
        {
            Console.WriteLine("***************************************************************************");
            Console.WriteLine("Enter the input file location (Example: C:\\Users\\Abc\\Test.txt): ");
        }

        private static void closeApp(List<string> invalidList)
        {
            Console.WriteLine("***************************************************************************");
            if (invalidList.Count() > 0)
            {
                Console.WriteLine("Invalid entires were:");
                for (int index = 0; index < invalidList.Count(); index++)
                {
                    Console.WriteLine(invalidList[index]);
                }
                Console.WriteLine("***************************************************************************");
            }            
            System.Console.ReadKey();
        }
    }
}