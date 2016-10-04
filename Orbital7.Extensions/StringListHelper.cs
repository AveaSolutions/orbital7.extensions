﻿using System;
using System.Collections.Generic;

namespace Orbital7.Extensions
{
    public static class StringListHelper
    {     
        // NOTE: This method assumes that the lists contain unique values and not duplicates.
        public static bool ListsEqual(List<string> list1, List<string> list2)
        {
            bool listsEqual = true;

            // Compare count.
            if (list1.Count == list2.Count)
            {
                // Check contents.
                foreach (string value in list1)
                {
                    if (!list2.Contains(value))
                    {
                        listsEqual = false;
                        break;
                    }
                }
            }
            // Else not equal.
            else
            {
                listsEqual = false;
            }

            return listsEqual;
        }

        public static bool ListMatch(List<string> list1, List<string> list2)
        {
            bool match = false;

            // Determine which set should be searched on the basis of length; you want
            // to loop through the smaller set to limit the number of comparisons.
            List<string> listToSearch = null;
            List<string> listToCompare = null;
            if (list1.Count > list2.Count)
            {
                listToSearch = list2;
                listToCompare = list1;
            }
            else
            {
                listToSearch = list1;
                listToCompare = list2;
            }

            // Look through the list to search.
            foreach (string searchItem in listToSearch)
            {
                foreach (string compareItem in listToCompare)
                {
                    if (compareItem.Equals(searchItem, StringComparison.CurrentCultureIgnoreCase))
                    {
                        match = true;
                        break;
                    }
                }

                if (match) break;
            }

            return match;
        }

        public static List<string> GetListsDifference(List<string> mainList, List<string> compareList)
        {
            List<string> difference = new List<string>();

            foreach (string item in mainList)
                if (!compareList.Contains(item)) difference.Add(item);

            return difference;
        }
    }
}
