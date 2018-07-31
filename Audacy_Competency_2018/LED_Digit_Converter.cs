using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Audacy_Competency_2018
{
    public class LED_Digit_Converter
    {
        public static List<string> getFinalConvertedDigitStrings(string wholeInputText)
        {
            //Initialize the lists that would be required to manipulate the data
            List<string> finalConvertedDigits = new List<string>();
            List<int> emptySpaceLineIndexList = new List<int>();
            List<string> firstLineList = new List<string>();
            List<string> secondLineList = new List<string>();
            List<string> thirdLineList = new List<string>();

            printInputDate(wholeInputText);

            //Split the whole input into three lined input segments
            string[] inputStringArray = Regex.Split(wholeInputText, "\\r\\n");
            for (int index = 0; index < inputStringArray.Count(); index++)
            {
                //Find the empty line which indicates the line feed between two input lines
                if (inputStringArray[index].Replace(" ", "").Equals(""))
                {
                    emptySpaceLineIndexList.Add(index);
                }
            }

            //Since there can be only three input lines as per the problem statement
            //we are limiting the repeat counts to 3
            int repeatCount = 1;
            foreach (int lineItem in emptySpaceLineIndexList)
            {
                if (repeatCount <= 3)
                {
                    //if the empty line item is 3, then the input must be on line 0, 1 and 2
                    if (lineItem == 3)
                    {
                        firstLineList.Add(inputStringArray[0]);
                        firstLineList.Add(inputStringArray[1]);
                        firstLineList.Add(inputStringArray[2]);
                    }
                    //if the empty line item is 7, then the input must be on line 4, 5 and 6
                    else if (lineItem == 7)
                    {
                        secondLineList.Add(inputStringArray[lineItem - 3]);
                        secondLineList.Add(inputStringArray[lineItem - 2]);
                        secondLineList.Add(inputStringArray[lineItem - 1]);
                    }
                    //the third line indicates the third input entry
                    else
                    {
                        thirdLineList.Add(inputStringArray[lineItem - 3]);
                        thirdLineList.Add(inputStringArray[lineItem - 2]);
                        thirdLineList.Add(inputStringArray[lineItem - 1]);
                    }
                }
                repeatCount++;
            }
            //Add the strings to the final converted digit list by triaging the lines
            finalConvertedDigits.Add(triageThelines(firstLineList));
            finalConvertedDigits.Add(triageThelines(secondLineList));
            finalConvertedDigits.Add(triageThelines(thirdLineList));
            return finalConvertedDigits;
        }

        /// <summary>
        /// Triage each line entries to a subset of 3 characters per string
        /// in that way, we get the first character's first segment and thereby
        /// getting the second and third line segment of the first character
        /// </summary>
        /// <param name="setOfThreeLines"></param>
        /// <returns>The entire line item in a digitally readable format</returns>
        public static string triageThelines(List<string> setOfThreeLines)
        {
            string test = "";
            List<string> firstLineTriageList = new List<string>();
            List<string> secondLineTriageList = new List<string>();
            List<string> thirdLineTriageList = new List<string>();
            int index = 0;
            
            foreach (string currentRow in setOfThreeLines)
            {
                int count = 0;
                for (int i = 0; i < currentRow.Length; i++)
                {
                    if (count == i)
                    {
                        test = currentRow.Substring(i, 3);
                        if (index == 0)
                        {
                            firstLineTriageList.Add(test);
                        }
                        else if (index == 1)
                        {
                            secondLineTriageList.Add(test);
                        }
                        else
                        {
                            thirdLineTriageList.Add(test);
                        }

                        count = count + 4;
                    }
                }
                index++;
            }

            //Determine the digits from the triaged lines
            List<string> digitList = new List<string>();
            string numberDetermined = "";
            for (int i = 0; i < firstLineTriageList.Count(); i++)
            {
                //Pass the first line triaged three character string along with second and third character triaged strings
                string determinedDigit = determineDigitsFromLines(firstLineTriageList[i], secondLineTriageList[i], thirdLineTriageList[i]);
                if(determinedDigit != null)
                {
                    if (numberDetermined != "")
                    {
                        numberDetermined = numberDetermined + determinedDigit;
                    }
                    else
                    {
                        numberDetermined = determinedDigit;
                    }
                }                
            }
            return numberDetermined;
        }

        /// <summary>
        /// Based on the stricture of first, second, and third character strings, we get to determine the 
        /// digit that it represents in a combined form
        /// </summary>
        /// <param name="firstLineSegment"></param>
        /// <param name="secondLineSegment"></param>
        /// <param name="thirdLineSegment"></param>
        /// <returns>The actual digital string</returns>
        public static string determineDigitsFromLines(string firstLineSegment, string secondLineSegment, string thirdLineSegment)
        {
            if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals("| |") && thirdLineSegment.Equals("|_|"))
            {
                return "0";
            }
            else if (firstLineSegment.Equals("   ") && secondLineSegment.Equals("|  ") && thirdLineSegment.Equals("|  "))
            {
                return "1";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals(" _|") && thirdLineSegment.Equals("|_ "))
            {
                return "2";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals(" _|") && thirdLineSegment.Equals(" _|"))
            {
                return "3";
            }
            else if (firstLineSegment.Equals("   ") && secondLineSegment.Equals("|_|"))
            {
                return "4";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals("|_ ") && thirdLineSegment.Equals(" _|"))
            {
                return "5";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals("|_ ") && thirdLineSegment.Equals("|_|"))
            {
                return "6";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals("  |") && thirdLineSegment.Equals("  |"))
            {
                return "7";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals("|_|") && thirdLineSegment.Equals("|_|"))
            {
                return "8";
            }
            else if (firstLineSegment.Equals(" _ ") && secondLineSegment.Equals("|_|") && thirdLineSegment.Equals(" _|"))
            {
                return "9";
            }
            return null;
        }

        private static void printInputDate(string wholeInputText)
        {
            Console.WriteLine("***************************************************************************");
            Console.WriteLine("Input file contains the following text: ");
            Console.WriteLine(wholeInputText);
            Console.WriteLine("***************************************************************************");
        }
    }
}
