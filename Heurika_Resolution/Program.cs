using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_resolution
{
    class Program
    {

        static void Main(string[] args)
        {
            Clauses C = new Clauses();
            //add KB to Clauses in CNF form
            C.Add(new List<string> { "a", "!b" });//a+!b
            C.Add(new List<string> { "b", "!c", "!d" });//b+!c+!d
            C.Add(new List<string> { "b", "c", "!d" });//b+c+!d
            C.Add(new List<string> { "d" });//d
            List<string> formula = new List<string>() { "!a" }; //define as CNF, KB:=a --> formula/clause = !a to proof nonsatisfaction, 

            Console.WriteLine(C.Print());
            Console.ReadLine();

            Algorithm algo = new Algorithm();
            bool enumerates = algo.linearResolution(C, new List<string>(formula));
        
            Console.WriteLine("KB := !"+C.Print(formula)+" is " + enumerates.ToString());
            Console.ReadLine();
    }
    }
}
