using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Labs
{
    class Lab1 : ILab
    {
        public string Description { get; set; }

        public Lab1()
        {
            Description = "Lab 1 связаное с минимальным количеством сортировок контейнера";
        }
        public string Execute(string input)
        {

            List<int> arr = new List<int>();
            var N = 0;
            var tempS = input.Split(' ', '\r', '\n', '\t');



            foreach (string element in tempS)
            {
                if (!string.IsNullOrEmpty(element))
                {
                    Console.WriteLine(string.IsNullOrEmpty(element));
                    arr.Add(Convert.ToInt32(element));
                }
            }
            Console.WriteLine(arr);

            var sum = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                sum += arr[i];
            }

            sum = sum - solve(arr);
            string ansv = getMin(arr[0], arr[1]) + " " + getMax(arr[0], arr[1]);

            return (Convert.ToString(arr));
            //var result = solve(n, a);
            //File.WriteAllText("output.txt", result);
        }

        static int solve(List<int> arr)
        {
            var ans = 0;
            var temp = 0;
            var temp1 = 0;
            var temp2 = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                if (temp < arr[i])
                {
                    temp2 = temp1;
                    temp1 = temp;
                    temp = arr[i];
                }
            }
            return temp + temp1 + temp2;
        }
        static int getMax(int s, int k)
        {
            int maxn = Convert.ToInt32(Math.Pow(10, k)) - 1;
            int ans;
            for (int j = maxn; j >= 1; j--)
            {
                int n, sum = 0, m;
                n = j;
                while (n > 0)
                {
                    m = n % 10;
                    sum = sum + m;
                    n = n / 10;
                }
                if (sum == s)
                {
                    return j;
                }

            }
            return ans = 0;
        }
        static int getMin(int s, int k)
        {
            int maxn = Convert.ToInt32(Math.Pow(10, k)) - 1;
            int ans;
            for (int j = Convert.ToInt32(Math.Pow(10, k - 1)); j <= maxn; j++)
            {
                int n, sum = 0, m;
                n = j;
                while (n > 0)
                {
                    m = n % 10;
                    sum = sum + m;
                    n = n / 10;
                }
                if (sum == s)
                {
                    return j;
                }

            }
            return ans = 0;
        }
    }
}
