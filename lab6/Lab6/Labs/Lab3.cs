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
            List<int> arr = new List<int>();
            List<int> target = new List<int>();
            var N = 0;
            double p = 1;
            var tempS = input.Split(' ', '\r', '\n', '\t');
            bool temp = true;
            foreach (string element in tempS)
            {

                if (temp)
                {
                    temp = false;
                    N = Convert.ToInt32(element);
                }
                else if (!string.IsNullOrEmpty(element))
                {
                    if (target.Count < 2)
                    {
                        target.Add(Convert.ToInt32(element));
                    }
                    else
                    {
                        arr.Add(Convert.ToInt32(element));
                    }
                }
            }
            Tree Sys = new Tree(arr);
            int ans = Sys.findCommon(target[0], target[1]);

            string result = Convert.ToString(ans);

            return result;
        }
        public class Tree
        {

            public Tree(List<int> target)
            {
                List<int> temp = new List<int>();
                temp.Add(1);
                children.Add(new Node(1, target, temp));
            }
            public List<Node> children = new List<Node>();

            public int findCommon(int a, int b)
            {
                List<int> ans1 = new List<int>();
                List<int> ans2 = new List<int>();
                ans1 = children[0].findPath(a);
                ans2 = children[0].findPath(b);
                int ans = 0;
                foreach (int element1 in ans1)
                {
                    foreach (int element2 in ans2)
                    {
                        if (element1 == element2)
                        {
                            ans = element1;
                        }
                    }
                }

                return ans;
            }
        }
        public class Node
        {

            public Node(int a, List<int> target, List<int> p)
            {

                path = new List<int>(p);
                path.Add(a);
                number = a;

                for (int i = 0; i < target.Count; i++)
                {
                    if (target[i] == a)
                    {
                        Console.WriteLine(a);
                        children.Add(new Node(i + 2, target, path));
                    }
                }
            }
            public List<Node> children = new List<Node>();
            public int number;
            public List<int> path = new List<int>();

            public List<int> findPath(int a)
            {
                List<int> ans = new List<int>();
                if (this.number == a)
                {
                    ans = this.path;
                }
                else
                {
                    foreach (Node child in children)
                    {
                        if (ans.Count != 0)
                        {
                            break;
                        }
                        ans = child.findPath(a);


                    }
                }
                return ans;
            }

        }


    }

}
