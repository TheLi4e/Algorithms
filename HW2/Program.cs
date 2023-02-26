using Algorithms;

var linkedList = new Algorithms.LinkedList<int>();
linkedList.Add(1);
linkedList.Add(2);
linkedList.Add(3);
linkedList.Add(4);
linkedList.Add(5);
foreach (int link in linkedList) Console.WriteLine(link);
Console.WriteLine();
Console.ReadKey();

//Task1. Необходимо реализовать метод разворота связного списка (двухсвязного или односвязного на выбор).
linkedList.Reverse();
foreach (int link in linkedList) Console.WriteLine(link);
Console.ReadKey();
