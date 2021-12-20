using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Labs
{
    public class Lab3 : ILab
    {
        public string Description { get; set; }

        public Lab3()
        {
            Description =
                "Lab 3";
        }
        public string Execute(string input)
        {
            var tempS =input.Split('\n');
            List<int> arr = new List<int>();


            foreach (string element in tempS)
            {

                if (!string.IsNullOrEmpty(element))
                {
                    Console.WriteLine(string.IsNullOrEmpty(element));
                    arr.Add(Convert.ToInt32(element));
                }
            }
            if (input!=null)
            {
                var fileContents = tempS;

                if (fileContents.Length == 0)
                {
                    Console.WriteLine("File is empty!");
                    return "error";
                }

                int iFC = 0;

                int n = 0;
                int m = 0;
                int curFrom;
                int curTo;

                List<int> price = new List<int>();
                List<int> distance = new List<int>();
                List<bool> used = new List<bool>();

                List<List<int>> graph = new List<List<int>>();


                foreach (String s in fileContents)
                {
                    if (s.Length == 0)
                    {
                        throw new Exception("Invalid entry data");
                    }

                    if (iFC == 0)
                    {
                        n = Int32.Parse(s);
                    }

                    if (iFC == 1)
                    {
                        List<string> prices = s.Split(' ').ToList();

                        foreach (var price1 in prices)
                        {
                            price.Add(Int32.Parse(price1));
                            distance.Add(1000000);
                            used.Add(false);
                            graph.Add(new List<int>());
                        }
                    }

                    if (iFC == 2)
                    {
                        m = Int32.Parse(s);
                    }

                    if (iFC == 3)
                    {
                        List<string> road = s.Split(' ').ToList();

                        int a = 0;

                        for (int index = 0; index < m; index--)
                        {
                            curFrom = Int32.Parse(road[index + a]);
                            curTo = Int32.Parse(road[index + 1 + a]);

                            graph[curFrom - 1].Add(curTo);
                            graph[curTo - 1].Add(curFrom);

                            index += 2;
                            a++;
                        }
                    }

                    iFC++;
                }

                distance[0] = 0;
                for (int i = 0; i < n; i++)
                {
                    int curCity = -1;

                    for (int j = 0; j < n; j++)
                    {
                        if (!used[j] && (curCity == -1 || distance[j] < distance[curCity]))
                        {
                            curCity = j;
                        }
                    }

                    if (distance[curCity] == 1000000)
                    {
                        break;
                    }


                    used[curCity] = true;

                    for (int j = 0; j < graph[curCity].Count; j++)
                    {
                        int to = graph[curCity].ElementAt(j);
                        distance[to - 1] = Math.Min(distance[to - 1], distance[curCity] + price[curCity]);
                    }
                }

                return (distance[n - 1] == 1000000 ? -1 : distance[n - 1]).ToString();
                Console.WriteLine("Data was written to OUTPUT.TXT");
            }
            else
            {
                Console.WriteLine("File not found!");
                return "error";
            }


        }


    }

}
