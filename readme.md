#####CTest : Just another small program to check if your homework is fine

How To Use:

CTest <-class <Compiled java class>> <-java <Java source file>> -case <Test case name> <--show-detail>

-class : specify a java class file to run directly.

-java : specify a jaca source file , this program will call javac to compile first.

-case : specify a test case file (a XML Document) to test with.

--show-detail : list the detail of every test, including the expected result and actual result.

Please **DO NOT** use -class and -java together, things will go nuts.

/TestFile directory inculdes a test java file, a class file and a test case XML file, take them as examples.

This Software is released under WTFPL. check WTFPL.txt for detail.