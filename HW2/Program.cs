namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlackTree<int> Tree = new RedBlackTree<int>();
            Tree.Add(576);
            Tree.Add(234);
            Tree.Add(199);
            Tree.Add(890);
            Tree.Add(900);
            Tree.Add(673);
            Tree.Add(467);

            var thing = Tree.FindCeiling(0);
            var bla = Tree.FindCeiling(100);
            var who = Tree.FindCeiling(200);
            var sa = Tree.FindCeiling(300);
            var why = Tree.FindCeiling(500);
            var food = Tree.FindCeiling(700);
            var sugar = Tree.FindCeiling(100000);

            var things = Tree.FindFloor(0);
            var blas = Tree.FindFloor(100);
            var whos = Tree.FindFloor(200);
            var saw = Tree.FindFloor(300);
            var whys = Tree.FindFloor(500);
            var foods = Tree.FindFloor(700);
            var sugarw = Tree.FindFloor(100000);

            Console.ReadLine();
        }
    }
}