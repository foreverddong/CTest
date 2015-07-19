using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
namespace CTest
{
    class App
    {
        XmlDocument document;
        public App()
        {
            document = new XmlDocument();
        }
        private String trimString(string originalString)
        {
           originalString = originalString.Replace("\r", "");
           originalString = originalString.Replace("\t", "");
           return originalString;
        }
        public void Test(String XMLPath, String JavaFilePath, bool doesCompile, bool ShowDetail)
        {
            string sPath = Environment.GetEnvironmentVariable("PATH");
            if (doesCompile == true)
            {
                System.Diagnostics.Process javac = new System.Diagnostics.Process();
                javac.StartInfo.FileName = "cmd.exe";
                javac.StartInfo.RedirectStandardInput = true;
                javac.StartInfo.UseShellExecute = false;
                javac.Start();
                javac.StandardInput.WriteLine("javac " + JavaFilePath);
            }
            System.Threading.Thread.Sleep(1000);
            document.Load(XMLPath);
            XmlNode rootNode = document.SelectSingleNode("Test");
            XmlNodeList testCases = rootNode.SelectNodes("Case");
            List<TestInfo> info = new List<TestInfo>();
            if (JavaFilePath.EndsWith(".java"))
            {
                JavaFilePath = JavaFilePath.Substring(0, JavaFilePath.Length - 5);
            }
            if (JavaFilePath.EndsWith(".class"))
            {
                JavaFilePath = JavaFilePath.Substring(0, JavaFilePath.Length - 6);
            }
            foreach (XmlNode node in testCases)
            {
                System.Diagnostics.Process java = new System.Diagnostics.Process();
                java.StartInfo.RedirectStandardInput = true;
                java.StartInfo.RedirectStandardOutput = true;
                java.StartInfo.UseShellExecute = false;
                TestInfo caseInfo = new TestInfo();
                info.Add(caseInfo);
                caseInfo.StdInput = node.SelectSingleNode("Input").InnerText;
                caseInfo.StdOutput = node.SelectSingleNode("Output").InnerText;
                caseInfo.StdOutput = this.trimString(caseInfo.StdOutput);
                java.StartInfo.FileName = "java";
                java.StartInfo.Arguments = JavaFilePath + " " + node.SelectSingleNode("Input").InnerText;
                Console.WriteLine(java.StartInfo.Arguments);
                java.Start();
                caseInfo.ActualOutput = java.StandardOutput.ReadToEnd();
                if (caseInfo.ActualOutput.EndsWith("\r\n"))
                { caseInfo.ActualOutput = caseInfo.ActualOutput.Substring(0, caseInfo.ActualOutput.Length - 2); }
                caseInfo.ActualOutput = this.trimString(caseInfo.ActualOutput);
                if (caseInfo.ActualOutput == caseInfo.StdOutput)
                {
                    caseInfo.isPassed = true;
                    int x;
                }
                else
                {
                    caseInfo.isPassed = false;
                }
            }
            bool totalPassed = true;
            foreach (TestInfo caseInfo in info)
            {
                if (caseInfo.isPassed == false)
                {
                    totalPassed = false;
                }
            }
            if (totalPassed == false)
            {
                Console.WriteLine("Your program did't pass the test!");
            }
            else
            {
                Console.WriteLine("Your program passed the test!");
            }
            if (ShowDetail == true)
            {
                Console.WriteLine("========================Test  Case  Detail============================");
                int i = 1;
                foreach (TestInfo caseInfo in info)
                {
                   Console.WriteLine("\n========================" + "Test :" + i + " ========================");
                   Console.WriteLine("Test Input :" + caseInfo.StdInput);
                   Console.WriteLine("Expected Output : " + caseInfo.StdOutput);
                   Console.WriteLine("Your Result : " + caseInfo.ActualOutput);
                   if (caseInfo.isPassed == true) { Console.WriteLine("Match : YES"); }
                   else { Console.WriteLine("Match : NO"); }
                   Console.WriteLine("=======================================================================");
                   i++;
                }
            }
        }
    }
    class TestInfo
    {
       public bool isPassed;
       public String StdInput;
       public String StdOutput;
       public String ActualOutput;
    }
}
