# CodeDiagram

## other call graph

https://sourceforge.net/projects/tceetree/
https://ensoftcorp.github.io/call-graph-toolbox/tutorials/
https://github.com/ofabry/go-callvis
http://www.clifford.at/yosys/screenshots.html
https://www.fpga4student.com/2017/05/how-to-write-verilog-testbench-for.html

## how to dotnet 5

0. [download & install sdk](https://dotnet.microsoft.com/download/dotnet/5.0)
1. create workspace directory
```
SolutionDir
|---Solution.sln //2
|---ProjectDir
  |---Project.csproj //3
  |---(Class.cs...) 
|---Project.TestDir
  |---Project.Test.csproj //4
  |---(Class_test.cs...) 
```
2. create solution
```
dotnet new solution
```
3. create project dir
```
dotnet new console
```
4. dotnet new xunit
```
dotnet new xunit
dotnet add reference ../ProjectDir/Project.csproj
```