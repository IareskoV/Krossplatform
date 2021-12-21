using System;
using System.Collections.Generic;
using System.IO;


namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            

            List<double> arr = new List<double>();
            var N = 0;
            double p = 1;
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
                    string temp = element.Replace('.', ',');
                    arr.Add(Convert.ToDouble(temp));
                }
            }

            for (int z = 0; z < N; z++)
            {
                p = p * arr[z] + (1 - p) * (1 - arr[z]);
            }

            string result = Convert.ToString(p);






            System.IO.File.WriteAllText("output.txt", result);


            Console.Write(arr);
            //var result = solve(n, a);
            //File.WriteAllText("output.txt", result);
        }

        
    }
}
