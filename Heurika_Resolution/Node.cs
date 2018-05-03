using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heurika_Resolution
{
    class Node
    {
        public Clauses Kb;
        public Node parent;
        public List<string> ResolvedClause;
        public int Cost;



        public List<Node> getChildren()
        {
            List<Node> children = new List<Node>();

            for (int i = 0; i < Kb.allClauses.Count; i++)
            {
                if (canSolve(Kb.allClauses[i]))
                {
                    var clause1 = new List<string>(Kb.allClauses[i]);
                    var clause2 = new List<string>(ResolvedClause);
                    var solvedClause = Algorithm.resolutionRule(clause1, clause2, out int heuristic);
                    //Kb.allClauses.Add(solvedClause);
                    var tempKb = new Clauses { allClauses = new List<List<string>>(Kb.allClauses)};
                    tempKb.allClauses.Add(ResolvedClause);
                    //Need to add pathCost
                    var node = new Node()
                    {
                        Kb = tempKb, //I wonder if it works, since syntax is a bit weird xD
                        parent = this,
                        ResolvedClause = solvedClause,
                        Cost = Cost + heuristic
                    };

                    children.Add(node);
                }
            }

            return children;
        }

        //Checks wheter it's possible to solve two clauses
        private bool canSolve(List<string> clause)
        {
            for (int i = 0; i < clause.Count; i++)
            {
                //When it's negation, look for "positive" literal
                if (clause[i][0] == '!')
                {
                    var temp = clause[i][1];
                    if (ResolvedClause.Contains(temp.ToString()))
                    {
                        return true;
                    }
                }

                //When it's "positive" look for negation
                else
                {
                    var temp = "!" + clause[i][0];
                    if (ResolvedClause.Contains(temp))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}