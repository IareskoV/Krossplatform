using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Labs
{
    public class Lab2 : ILab
    {
        public string Description { get; set; }

        public Lab2()
        {
            Description ="Lab 2 вводим данные по поводу клеточки  2 5 и смотрим выиграет ли игрок 0 или 1";
        }
        public string Execute(string input)
        {
            List<double> arr = new List<double>();
            var N = 0;
            double p = 1;
            var tempS = input.Split(' ', '\r', '\n', '\t');


            foreach (string element in tempS)
            {
                if (element == tempS[0])
                {
                    N = Convert.ToInt32(element);
                }
                else if (!string.IsNullOrEmpty(element))
                {
                    Console.WriteLine(string.IsNullOrEmpty(element));
                    string temp = element.Replace('.', ',');
                    arr.Add(Convert.ToDouble(temp));
                }
            }

            for (int z = 0; z < N; z++)
            {
                p = p * arr[z] + (1 - p) * (1 - arr[z]);
            }

            string result = Convert.ToString(p);
            return Convert.ToString(result);







            Console.Write(arr);
            //var result = solve(n, a);
            //File.WriteAllText("output.txt", result);
        }

        static int[,] transform(int[,] test, int startx, int starty)
        {
            if (test[startx, starty] == 0)
            {
                for (var i = 1; i < 35 - startx; i++)
                {
                    test[i + startx, starty] = 2;
                }
                for (var i = 1; i < 35 - starty; i++)
                {
                    test[startx, i + starty] = 2;

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

                    transform(test, startx + 1, starty);
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
