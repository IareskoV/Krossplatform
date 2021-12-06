using System;
using System.Collections.Generic;
using System.IO;
namespace Vladlab1
{
    class Program
    {

        static void Main(string[] args)
        {
            List<int> arr = new List<int>();
            var N = 0;
            var tempS = System.IO.File.ReadAllText("input.txt").Split(' ', '\r', '\n','\t');



            foreach(string element in tempS)
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

            var sum = 0;
            for(int i = 0; i<arr.Count ;i++ )
            {
                sum += arr[i];
            }

            sum = sum - solve(arr);




            System.IO.File.WriteAllText("output.txt", string.Join(" ", sum));


            Console.Write(arr);
            //var result = solve(n, a);
            //File.WriteAllText("output.txt", result);
        }


        static int solve(List<int> arr)
        {
            var ans = 0;
            var temp = 0;
            var temp1 = 0;
            var temp2 = 0;
            for ( int i =0; i <arr.Count ;i++ )
            {
                if( temp < arr[i])
                {
                    temp2 = temp1;
                    temp1 = temp;
                    temp = arr[i];
                }
            }
            return temp + temp1 + temp2;
        }

    }
}   
