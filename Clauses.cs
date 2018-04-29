using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_resolution
{
    class Clauses
    {
        //define KB as CNF: C=all clauses.
        public List<List<string>> allClauses = new List<List<string>>();

        public Clauses()
        {
        }

        public void Add(List<string> toAdd)
        {
            allClauses.Add(new List<string>(toAdd));
        }

        public List<string> Get(int idx)
        {
            return allClauses[idx];
        }

        //gives the first one that matches.. not using any heuristiks.. takes best one of children..
        public List<string> bestClauseViaHeuristics(List<string> givenClause)
        {
            // do heuristics here...
            //for each literal in givenClause,
            for (int i=0; i<givenClause.Count(); i++)
            {
                string literal = givenClause[i];
                if (literal[0] == '!')
                {
                    int idx = ContainsString(literal[1].ToString()); //this makes !a--> a
                    if (idx >= 0)
                    {
                        return new List<string>(allClauses[idx]);
                    }
                }
                else
                {
                    int idx = ContainsString("!"+literal[0].ToString()); //this makes !a--> a
                    if (idx >= 0)
                    {
                        return new List<string>(allClauses[idx]);
                    }
                }
            }
            return null;
        }

        // do the resolution rule -> take two clauses and make one out of them
        //pre: no empty clauses
        //post: resolution done on two clauses, returns one clause
    

        //returns clause containing the searched string
        public int ContainsString(string toFind)
        {
            for (int i = 0; i < allClauses.Count(); i++)
            {
                for (int j = 0; j < allClauses[i].Count(); j++)
                {
                    if (allClauses[i][j] == toFind) { return i; }
                }
            }
            return -1;
        }

        //returns indes of matched clause, if none, returns -1
        public int sameClauseAt(List<string> clause2)
        {
            for (int i = 0; i < allClauses.Count(); i++)
            {
                List<string> clause1 = allClauses[i];
                clause1.Sort();
                clause2.Sort();
                if(clause1.Count() == clause2.Count())
                {
                    if (EqualClauses(clause2, clause1)) { return i; }
                    //return index on first found.
                }
            }
            return -1;
        }

        //pre: clauses are sorted
        public bool EqualClauses(List<string> clause1, List<string> clause2)
        {
            for(int i=0; i<clause1.Count(); i++)
            {
                if (clause1[i] != clause2[i]) { return false; }
            }
            return true;
        }

        public string Print()
        {
            string output="KB: ";
            for(int i=0; i<allClauses.Count(); i++)
            {
                allClauses[i].Sort();
                output = output + "(";
                for (int j=0; j<allClauses[i].Count(); j++)
                {
                    if (j == 0)
                    {
                        output = output + allClauses[i][j];
                    }
                    else
                    {
                        output = output + "+" + allClauses[i][j];
                    }
                }
                output = output + ")";
            }
            return output;
        }

        public string Print(List<List<string>> C)
        {
            string output = "";
            for (int i = 0; i < C.Count(); i++)
            {
                output = output + "(";
                for (int j = 0; j < C[i].Count(); j++)
                {
                    if (j == 0)
                    {
                        output = output + C[i][j];
                    }
                    else
                    {
                        output = output + "+" + C[i][j];
                    }
                }
                output = output + ")";
            }
            return output;
        }
        public string Print(List<string> clause)
        {
            if (clause == null)
            {
                return "()";
            }
            string output = "(";
                for (int j = 0; j <clause.Count(); j++)
                {
                    if (j == 0)
                    {
                        output = output + clause[j];
                    }
                    else
                    {
                        output = output + "+" + clause[j];
                    }
                }
            output = output + ")";
            return output;
        }
    }
}
