using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace CTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            String CaseXML = "";
            String javaFile = "";
            bool doesCompile = false; ; bool argumentIncorrect = false; bool showDetail = false;
            if (args.Length == 0) argumentIncorrect = true;
            List<String> arguments = new List<String>();
            foreach (String argString in args)
	        {
		        arguments.Add(argString);
	        }
            while (arguments.Count != 0)
            {
                String option = arguments[0];
                switch (option)
                {
                    case "-case":
                        CaseXML = arguments[1];
                        arguments.RemoveAt(0);
                        arguments.RemoveAt(0);
                        break;
                    case "-java":
                        doesCompile = true;
                        javaFile = arguments[1];
                        arguments.RemoveAt(0);
                        arguments.RemoveAt(0);
                        break;
                    case "-class":
                        doesCompile = false;
                        javaFile = arguments[1];
                        arguments.RemoveAt(0);
                        arguments.RemoveAt(0);
                        break;
                    case "--show-detail" :
                        showDetail = true;
                        arguments.RemoveAt(0);
                        break;
                    default:
                        argumentIncorrect = true;
                        arguments.RemoveAt(0);
                        break;
                }

            }
            App app = new App();
            if (argumentIncorrect == true)
            {
                Console.WriteLine("Usage: CTest <-class <Compiled java class>> <-java <Java source file>> -case <Test case name> <--show-detail>");
            }
            else
            {
                app.Test(CaseXML, javaFile, doesCompile, showDetail);
            }
        }
    }
}
