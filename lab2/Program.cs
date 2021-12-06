using System;
using System.Collections.Generic;
using System.IO;


namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[255, 255];
            for (var i = 1; i < 255; i++)
            {
                array[i, i] = 2;
            }
            var startx = 0;
            var starty = 0;
            array = transform(array, startx, starty);

            Console.Write(array[22, 20]);

            List<int> arr = new List<int>();
            var N = 0;
            var tempS = System.IO.File.ReadAllText("input.txt").Split(' ', '\r', '\n', '\t');



            foreach (string element in tempS)
            {
                if (element == tempS[0])
                {
                    N = Convert.ToInt32(element);
                }
                else if (!string.IsNullOrEmpty(element))
                {
                    Console.WriteLine(string.IsNullOrEmpty(element));
                    arr.Add(Convert.ToInt32(element));
                }
            }

            var result = array[Convert.ToInt32(tempS[0]), Convert.ToInt32(tempS[1])];






            System.IO.File.WriteAllText("output.txt", string.Join("player - ", result));


            Console.Write(arr);
            //var result = solve(n, a);
            //File.WriteAllText("output.txt", result);
        }

        static int[,] transform(int[,] test, int startx, int starty)
        {
            if (test[startx,starty] == 0)
            {
                for (var i = 1; i < 35 - startx; i++)
                {
                    test[i + startx,starty] = 2;
                }
                for (var i = 1; i < 35 - starty; i++)
                {
                    test[startx,i + starty] = 2;

                }
                if (startx == 254)
                {
                    if (starty == 254)
                    {
                        return test;
                    }
                    else
                    {
                        startx = 0;
                        starty += 1;
                        Console.WriteLine('2');
                        transform(test, startx, starty);
                    }
                    
                }
                else
                {

                    transform(test, startx + 1,starty);
                }

                return test;
            }
            else
            {
                return test;
            }

        }
        }
}
