using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurika_Resolution
{
    static class Algorithm
    {

        //it is like Uniform cost search, but we have an ordered queue
        //path cost is set in problem class..
        // algo from Book: figure 3.14
        public static Boolean Astar(Clauses kb, List<string> formula)
        {
            //Node node = new Node(problem.initial(), null, null, 0);
            var node = new Node
            {
                Kb = kb,
                ResolvedClause = formula,
                Cost = 0
            };
            List<Node> frontier = new List<Node>(); //priority queue
            frontier.Add(node);
            List<Node> expanded = new List<Node>();
            var index = 0;

            while (frontier.Any())
            {
                frontier.OrderBy(x => x.Cost); //orders frontier in ascending order
                node = frontier[0]; //picks the one with lowest value
                Console.WriteLine(node.Kb.Print() + " =: " + node.Kb.Print(node.ResolvedClause));
                //Console.WriteLine("current node: " + node.State().print());
                if (node.Kb.allClauses.Count == 0 || frontier.Exists(x => x.ResolvedClause.Count() == 0))
                {
                    return true;
                }

                frontier.Remove(node);
                expanded.Add(node);
                foreach (Node child in node.getChildren())
                {   //if child state not in frontier and not in expanded
                    if ((!frontier.Exists(x => x.Kb == child.Kb)))
                    {
                        if (!expanded.Exists(x => x.Kb == child.Kb))
                        {
                            frontier.Add(child);
                        }
                    }
                    else//here we know that chil is in frontier, as above if failed.
                    {   //if node already exist in frontier, but child can reach it wirh less cost, then replace node in frontier.
                        int i = frontier.FindIndex(x => x.Kb == child.Kb);
                        if (frontier[i].Cost > child.Cost)
                        {
                            frontier[i] = child;
                        }
                    }
                }
            }
            Console.WriteLine("\n" + "Frontier is empty, no solution found.");
            return false; //if frontier is empty
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
        public static List<string> resolutionRule(List<string> clause1, List<string> clause2, out int heursitic)
        {
            List<int> idx1 = new List<int>();
            List<int> idx2 = new List<int>();
            heursitic = 0;

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
            heursitic = clause1.Count;
            return new List<string>(clause1);
        }

        public static string negate(string toNegate)
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
