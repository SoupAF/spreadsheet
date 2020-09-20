// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

//Final implementaion written by Christopher Fish for CS3500, September 2020

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        //There is two HashSets in this class, one to hold depndents, and one to hold dependees
        //These will both be given values in constructors
        private List<List<string>> dependents;
        private List<List<string>> dependees;

        //These lists contain the strings used to create dependencies and can be used to find the index of a value's dependents/dependees
        private List<String> dependeeKey;
        private List<String> dependentKey;


        private int size;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            dependees = new List<List<string>>();
            dependents = new List<List<string>>();

           

            dependeeKey = new List<string>();
            dependentKey = new List<string>();

            size = 0;

        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            //Num of ordered pairs
            get { return size; }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            //Count dependees for given string s
            get 
            {
                int index = GetDependentKey(s);
                return dependents[index].Count;
            }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            //Check for dependants of string s
            int index = GetDependentKey(s);

            if (index >= 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            int index = GetDependeeKey(s);
            if (index >= 0)
                return true;
            else
                return false;
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            //If there are no dependents of s, return and empty list

            int index = GetDependentKey(s);
            if (index < 0)
                return new List<string>();

            //Otherwise, return a loist with all dependents of s
            else
            {
                List<string> result = new List<string>();
                result = dependents[index];
                return result;
            }
            
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            //If there are no dependees of s, return and empty list
            int index = GetDependeeKey(s);
            if (index < 0)
                return new List<string>();

            //Otherwise, return a loist with all dependees of s
            else
            {
                List<string> result = new List<string>();
                result = dependees[index];
                return result;
            }

        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            //Stores the index of s in the dependencies list
            int index;


            //Check if s has and depedents
            index = GetDependentKey(s);
            if(index >= 0)
            {
                //If s has dependents, but not t, add t 
                if (!dependents[index].Contains(t))
                {
                    dependents[index].Add(t);

                    //If t has dependees, add s to its list
                    if (dependeeKey.Contains(t))
                    {
                        index = dependeeKey.IndexOf(t);
                        dependees[index].Add(s);
                    }

                    //Otherwise, add t to the dependee list, and add s to its list
                    else
                    {
                        dependeeKey.Add(t);
                        index = dependeeKey.IndexOf(t);
                        dependees.Add(new List<string>());
                        dependees[index].Add(s);
                    }

                    
                    size++;
                }
                //If t is already present, nothing needs to be done
            }

            //If s has no dependents, add it to the list
            else
            {
                dependentKey.Add(s);
                index = GetDependentKey(s);
                dependents.Add(new List<string>());
                dependents[index].Add(t);
                size++;

                //If t has dependees, add s to its list
                if (dependeeKey.Contains(t))
                {
                    index = dependeeKey.IndexOf(t);
                    dependees[index].Add(s);
                }

                //Otherwise, add t to the dependee list, and add s to its list
                else
                {
                    dependeeKey.Add(t);
                    index = dependeeKey.IndexOf(t);
                    dependees.Add(new List<string>());
                    dependees[index].Add(s);
                }
                
            }

        }
        


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            //Check that the dependency exists, then remove it
            int index = GetDependentKey(s);
            if (index >= 0)
            {
                if (dependents[index].Contains(t))
                {
                    //Remove t from s' list of dependents
                    dependents[index].Remove(t);
                    size--;

                    //Remove s from t's list of dependees
                    index = GetDependeeKey(t);
                    dependees[index].Remove(s);
                }
            }
            
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            //Check that s has dependees
            int index = GetDependentKey(s);
            int oldSize;

            if (index < 0)
            {
                //If not, add s to the dependents lists
                dependentKey.Add(s);
                dependents.Add(new List<string>());
                oldSize = 0;
            }
            else oldSize = dependents[index].Count;


            //Replace all dependents
            index = GetDependentKey(s);
            List<string> oldDependents = dependents[index];
            dependents[index] = new List<string>(newDependents);

            //Remove old dependent references
            for (int i = 0; i < oldDependents.Count; i++)
            {
                int loc = GetDependeeKey(oldDependents.ElementAt<string>(i));
                dependees[loc].Remove(s);
            }

            //Update dependents list
            for (int i = 0; i < newDependents.Count(); i++)
            {
                //Check that the new dependee is part of the list, and if not add it
                if (GetDependeeKey(newDependents.ElementAt<string>(i)) < 0)
                {
                    dependeeKey.Add(newDependents.ElementAt<string>(i));
                    dependees.Add(new List<string>());
                }


                int loc = GetDependeeKey(newDependents.ElementAt<string>(i));
                dependees[loc].Add(s);
            }

            //Update size
            int sizeChange = oldSize - newDependents.Count();
            //If the new list is bigger, sizeChange will be negative and result in size increasing. Otherwise, it will shrink
            size = size - sizeChange;

        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            //Check that s has dependees
            int index = GetDependeeKey(s);
            if (index >= 0)
            {
                //If not, add s to the dependees lists
                dependeeKey.Add(s);
                dependees[GetDependeeKey(s)] = new List<string>();
            }
            
                int oldSize = dependees[index].Count;

                //Replace all dependees
                List<string> oldDependees = dependees[index];
                dependees[index] = new List<string>(newDependees);


                //Update dependents list
                for (int i = 0; i < newDependees.Count(); i++)
                {
                //Check that the new dependee is part of the list, and if not add it
                   if (GetDependentKey(newDependees.ElementAt<string>(i)) < 0)
                   {
                       dependentKey.Add(newDependees.ElementAt<string>(i));
                       dependents.Add(new List<string>());
                   }


                   int loc = GetDependentKey(newDependees.ElementAt<string>(i));
                   dependents[loc].Add(s);
                }

                //Update size
                int sizeChange = oldSize - newDependees.Count();
                //If the new list is bigger, sizeChange will be negative and result in size increasing. Otherwise, it will shrink
                size = size - sizeChange;

        }


        /// <summary>
        /// Returns the index value of a given strings dependee list
        /// </summary>
        /// <param name="s"> The string that you want dependees of</param>
        /// <returns>The index value of String s' dependee list</returns>
        private int GetDependeeKey(String s)
        {
            return dependeeKey.IndexOf(s);
        }

        /// <summary>
        /// Returns the index value of a given strings dependent list
        /// </summary>
        /// <param name="s"> The string that you want dependents of </param>
        /// <returns>The index value of String s' dependent list</returns>
        private int GetDependentKey(String s)
        {
            return dependentKey.IndexOf(s);
        }

        
    }

}
