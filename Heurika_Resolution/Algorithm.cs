using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_resolution
{
    class Algorithm
    {
        public Algorithm()
        {

        }

        //this is complete (see p. 356), but for it to be efficient, we need to guide it towards the empty state,
        //because this algo might take the longest path instead of the shortest..
        public bool linearResolution(Clauses C, List<string> formula)
        {
            C.Add(formula); // C = (KB+formula)
            List<string> R = formula;
            Console.WriteLine("Check if KB := !" + C.Print(formula));
            Console.WriteLine(C.Print());
            while (R.Any())
            {//At every step we use the previously derived clause and a clause R to derive a new clause.
                List<string> clause = C.bestClauseViaHeuristics(R); Console.WriteLine("resolution with: " + C.Print(R) + " and " + C.Print(clause));
                if (clause == null) { return false; }
                R = resolutionRule(R, clause); Console.WriteLine("= " + C.Print(R));
                int idxWithSameClause = C.sameClauseAt(R);
                if (idxWithSameClause > -1)// there is  
                {
                    Console.WriteLine("Same clause at " + (idxWithSameClause + 1).ToString() + ", don't add it again");
                }
                else
                {
                    C.Add(R); Console.WriteLine(C.Print(R) + " is added to KB");// resulting clause is not in Clauses yet, so add it..
                }
                Console.WriteLine(C.Print());
            }
            Console.WriteLine("empty clause in KB");
            return true;
        }

            //proof by refutation
            //infer new conclusions
            //resolution inference rule

            //input resolution: A = (KB+!a) in CNF. clauses can only be derived using at least one clause from A.
            //meaning: at every step, we yous the previously derived clause and a new clause from A to derive a new clause.
            //problem: this procedure is not complete!!So we might never reach the empty clause even if it was possible to derive is using regular resolution (which is complete).

            //therefore use a complete methode, called Linear Resolution  P.356
            //to do resolution: to get from R0 to R1, take Ci being element of all C AND all previous resolutions.. this makes it complete..

        //takes two clauses and returns a new one
        public List<string> resolutionRule(List<string> clause1, List<string> clause2)
        {
            List<int> idx1 = new List<int>();
            List<int> idx2 = new List<int>();

            // find the complementary literals
            for (int i = 0; i < clause1.Count(); i++)
            {
                string lookFor = negate(clause1[i]);
                for (int j = 0; j < clause2.Count(); j++)
                {
                    if (lookFor == clause2[j])
                    {
                        idx1.Add(i); // list of indexes which should be deleted out of clause1
                        idx2.Add(j);
                    }
                }
            }
            //now idx1,2 contain all indexes which represent complementary literals, remove them
            foreach (int removeHere in idx1)
            {
                clause1.RemoveAt(removeHere);
            }
            foreach (int removeHere in idx2)
            {
                clause2.RemoveAt(removeHere);
            }
            clause1.AddRange(clause2);
            clause1 = clause1.Distinct().ToList(); //makes (a + a + b)=> (a + b)
            return new List<string>(clause1);
        }

        public string negate(string toNegate)
        {
            if (toNegate[0] == '!')
            {
                return toNegate[1].ToString();
            }
            else
            {
                return "!" + toNegate[0];
            }

        }

    }

}
