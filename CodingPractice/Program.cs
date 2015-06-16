using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CodingPractice
{
    class Program
    {
        public static Queue<DateTime> queue = new Queue<DateTime>();
        static void Main(String[] args) {

            //AsyncTest();
            //test;
            //Console.WriteLine(strToInt("5782"));
            //FindMinOperations("abcd");

            /****
            int numSticks = 6;
            int[] sticks = new int[numSticks];
            sticks[0] = 5;
            sticks[1] = 4;
            sticks[2] = 4;
            sticks[3] = 2;
            sticks[4] = 2;
            sticks[5] = 8;


            //for (int i = 0; i < numSticks; i++)
            //    sticks[i] = Int32.Parse(Console.ReadLine());

            int[] arr = FindNumCuts(sticks);
            foreach (var i in arr)
                Console.WriteLine(i);
            *****/

            //Console.Write(OpeningClosingTagsRight("({[kd[{(jdkdk)}]]})"));

            /****
            List<int> list = new List<int>() { 1, 2, 0, 3, 10, 0, 5 };
            int[] arr = list.ToArray();
            PushZerosDown(arr);
            *****/

            //Console.WriteLine(FindFirstUniqueChar("fbcdhbfeced"));

            /***
            List<int> list = new List<int>() { 1, 2, 3, 5 };
            int[] arr1 = list.ToArray();

            List<int> list2 = new List<int>() { 1, 2, 3, 3, 10 };
            int[] arr2 = list2.ToArray();

            MergeSortedArrays(arr1, arr2);
             *****/
       //     MinAvg();
            
            //Tuple<int, int> num = new Tuple<int, int>(10, 45);
            //num = GCD(num);
            //Console.WriteLine(num.Item1);
            

           // Console.WriteLine(PrintMultiPle(3, 5, 15));

            /******
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(IfFunctionIsCalled10TimesInaMinute());

                Thread.Sleep(5000);
                if (i == 9)
                    Thread.Sleep(35000);
            }
            *******/

            Console.WriteLine(DecimalToBinary(233));

            Console.Read();
       
    }

        private static string DecimalToBinary(int deciml)
        {
            StringBuilder str = new StringBuilder();
            Stack<int> stack = new Stack<int>();

            while(deciml > 0)
            {
                int reminder;
                deciml = Math.DivRem(deciml, 2, out reminder);
                stack.Push(reminder);
            }
            while(stack.Count > 0)
            {
                str.Append(stack.Pop().ToString());
            }
            return str.ToString();
            

        }
        private static bool IfFunctionIsCalled10TimesInaMinute()
        {
            
            
            DateTime now = DateTime.Now;

            Console.WriteLine(string.Format("Call, time:{0}", now));

            queue.Enqueue(now);
            while(queue.Count >0 && now - queue.Peek() >= new TimeSpan(0,1,0))
            {
                queue.Dequeue();
            }
            if (queue.Count >= 10)
                return true;

            return false;
        }


        private static string PrintMultiPle(int multiPlier1, int multiPlier2, int num)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 1; i <= num; i++)
            {
                if (i % multiPlier1 == 0 && i % multiPlier2 == 0)
                {
                    str.Append("FB ");
                }
                else if (i % multiPlier1 == 0)
                {
                    str.Append("F ");

                }
                else if (i % multiPlier2 == 0)
                {
                    str.Append("B ");

                }
                else
                {
                    str.Append(string.Format("{0} ", i));
                }
            }
            str.Remove(str.Length - 1, 1);
            return str.ToString();
        }
        private static Tuple<int, int> GCD(Tuple<int,int> num)
        {
            
            int reminder = 0;
            if (num.Item2 == 0)
                return num;
            reminder = num.Item1 % num.Item2;


            Tuple<int, int> divRem = new Tuple<int, int>(num.Item2,reminder);
            return GCD(divRem);
        }

        private static void MinAvg()
        {
            int numCustomers = 5;
            int[] arrivals = new int[numCustomers];
            int[] cookingTimes = new int[numCustomers];

            arrivals[3] = 961148050;
            arrivals[2] = 951133776;
            arrivals[0] = 283280121;
            arrivals[1] = 317664929;
            arrivals[4] = 980913391;

            cookingTimes[0] = 782916802;
            cookingTimes[1] = 898415172;
            cookingTimes[2] = 376367013;
            cookingTimes[3] = 385599125;
            cookingTimes[4] = 847912645;
            //arrivals[0] = 961148050;
            //arrivals[1] = 951133776;
            //arrivals[2] = 283280121;
            //arrivals[3] = 317664929;
            //arrivals[4] = 980913391;

            //cookingTimes[0] = 385599125;
            //cookingTimes[1] = 376367013;
            //cookingTimes[2] = 782916802;
            //cookingTimes[3] = 898415172;
            //cookingTimes[4] = 847912645;

            //for(int i=0;i<numCustomers;i++)
            //    {
            //        string[] temp = Console.ReadLine().Split(' ');
            //        arrivals[i] = Int32.Parse(temp[0]);
            //        cookingTimes[i] = Int32.Parse(temp[1]);            
            //}     
            Console.WriteLine(FindMinAvgWaitTime(numCustomers, arrivals, cookingTimes));
            Console.Read();
        }
        private static long FindMinAvgWaitTime(int numCustomers, int[] arrivals, int[] cookTimes)
        {
            //THIS IS NOT GOOD, NEED PRIORITY QUEUE FOR THIS
            long TotalWaitTime = 0;
            long currentTime = arrivals[0];
            long customerProcessed = 0;

            long runningMin = int.MaxValue;
            while(customerProcessed <= numCustomers)
            {
                int i = 0;
                runningMin = long.MaxValue;
                int count = 0;
                while(
                    (i<numCustomers && (arrivals[i] <= currentTime) && ((arrivals[i] + cookTimes[i]) <= runningMin)))
                {
                    runningMin = arrivals[i] + cookTimes[i];
                    if (runningMin < 1)
                        runningMin = long.MaxValue;
                    i++;
                    count++;
                }
                if(count>0)
                --i;
                customerProcessed++;
                currentTime += cookTimes[i];
                TotalWaitTime += ( currentTime - arrivals[i]);
                arrivals[i] = 0;
                cookTimes[i] = 0;


            }


            return (long)TotalWaitTime/numCustomers;
        }

         private static void MergeSortedArrays(int[] arr1,int[]arr2)
        {
            

            int[] shortarr, longarr;
            int[] arr = new int[arr1.Length + arr2.Length];
            if(arr1.Length < arr2.Length)
            {
                shortarr = arr1;
                longarr = arr2;
            }
            else
            {
                shortarr = arr2;
                longarr = arr1;
            }
            for (int x = 0; x < shortarr.Length; x++)
                Console.Write(shortarr[x] + ",");
            Console.WriteLine();
            for (int x = 0; x < longarr.Length; x++)
                Console.Write(longarr[x] + ",");
            Console.WriteLine();

            int j=0;
            int k=0;
            for(int i=0;i<shortarr.Length;)
            {
                
                if(shortarr[i] <= longarr[j])
                {
                    arr[k] = shortarr[i];
                    k++;
                    i++;
                }
                else
                {
                    arr[k] = longarr[j];
                    j++;
                    k++;
                }
            }

            for (; j < longarr.Length;j++)
            {
                arr[k] = longarr[j];
                k++;
                j++;
            }
                for (int x = 0; x < arr.Length; x++)
                    Console.Write(arr[x] + ",");
        }
        private static char FindFirstUniqueChar(string str)
        {
            char[] arr = str.ToCharArray();
            Dictionary<char, int> dict = new Dictionary<char, int>();
            int j = 0;
            for(int i=0;i<arr.Length;i++)
            {

                int ct =  1;
                if(dict.TryGetValue(arr[i],out ct))
                {
                    dict[arr[i]] = ++ct;
                }
                else
                {
                    dict.Add(arr[i],1);
                }
                
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (dict[arr[i]] == 1)
                    return arr[i];
            }
            return arr[0];
        }

        private static void PushZerosDown(int[] arr)
        {
            for (int x = 0; x < arr.Length; x++)
                Console.Write(arr[x] + ",");

            int j = 0;
            int count = 0;
            int i = 0;
            for (; i < arr.Length;i++)
            {
                
                if(arr[i] != 0)
                {
                    if(count>0)//this is just to avoid copying happens without it before first zero
                    {
                        arr[j] = arr[i];
                    }
                    j++;                   
                }
                else
                {
                    count++;
                }

            }

            
             for (i = arr.Length - 1; i >= j; i--)
                 arr[i] = 0;

                for (int x = 0; x < arr.Length; x++)
                    Console.Write(arr[x] + ",");

            //int zerocount = 0;
            //for (int i = 0; i < arr.Length; )
            //{
            //    if (arr[i] == 0)
            //    {
            //        zerocount++;
            //        int j = i + 1;
            //        while (j<arr.Length&&arr[j] != 0)
            //        {
            //            arr[j - zerocount] = arr[j];
            //            j++;
            //        }
            //        Console.WriteLine();
            //        for (int x = 0; x < arr.Length; x++)
            //            Console.Write(arr[x] + ",");
            //        i = j;
            //    }
            //    else
            //        i++;
            //}
            //for (int i = arr.Length - zerocount; i < arr.Length; i++)
            //    arr[i] = 0;

            //Console.WriteLine();
            //for (int i = 0; i < arr.Length; i++)
            //    Console.Write(arr[i] + ",");
        }

       

        private static bool OpeningClosingTagsRight(string str)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>() { { '{', 1 }, { '(', 1 }, { '[', 1 }, { '}', '{' }, { ')', '(' }, { ']', '[' } };
           // Dictionary<char, char> dict2 = new Dictionary<char, char>() { { '{', '}' }, { '[', ']' }, { '(', ')' } };
            Stack<char> stack = new Stack<char>();
            foreach (var c in str)
            {
                int val = 0;
                if (dict.TryGetValue(c, out val))
                {
                    if (val == 1)
                        stack.Push(c);
                    else
                    {
                        if (stack.Count > 0)
                        {
                            char opening = stack.Pop();
                            if (opening != dict[c])
                                return false;
                        }

                    }

                }


            }
           
            return true;

        }

        private static int[] FindNumCuts(int[] sticks)
        {
            List<int> list = new List<int>(sticks);
            List<int> operationsList = new List<int>();
            int min = Int32.MaxValue;
            int arrcount = list.Count;
            bool done = false;
            while (!done)
            {
                int operations = 0;
                min = FindMin(list);
                done = true;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] > 0)
                    {
                        list[i] -= min;
                        operations++;
                        done = false;
                    }
                    
                    
                        
                    
                }
                operationsList.Add(operations);
                
            }
            return operationsList.ToArray();

        }
        private static int FindMin(List<int> arr)
        {
            int min = Int32.MaxValue;
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] != 0 && arr[i]< min)
                    min = arr[i];
            }
            return min;
        }
        private static int FindMinOperations(string str)
        {
            char[] arr = str.ToCharArray();
            int start = 0;
            int end = str.Length-1;
            int numOperations = 0;
            while (start < str.Length)
            {
                while (arr[start] > arr[end])
                {
                    numOperations++;
                    arr[start]--;
                }
                while (arr[start] < arr[end])
                {
                    numOperations++;
                    arr[end]--;
                }
                start++;
                end--;
            }
            return numOperations;
        }

        private static int strToInt(string str)
        {
           // not so elegant
            //int sum = 0;
            //int j = 0;
            //for (int i = str.Length; i > 0; i--)
            //{
            //    sum += (int)(Math.Pow(10, i - 1) * ((int)(str[j]) - (int)('0')));
            //    j++;
            //}
            //return sum;

            //elegant
            int res = 0;
            foreach (var c in str)
            {
               // Console.WriteLine(string.Format("res:{0},c:{1}", res, c));
                res = 10 * res + (c - '0');

            }
            //Console.WriteLine(string.Format("res:{0},c:{1}", res, str[str.Length-1]));
            return res;
        }

        private static void AsyncTest()
        {
            //ExecutionContext ct = ExecutionContext.Capture();
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //DelDemo.fun(30);

            // The asynchronous method puts the thread id here. 
            int threadId;

            // Create an instance of the test class.
            AsyncDemo ad = new AsyncDemo();

            // Create the delegate.
            AsyncMethodCaller caller = new AsyncMethodCaller(ad.TestMethod);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(3000,
                out threadId, null, null);

            //Thread.Yield();
            Console.WriteLine("Main thread {0} does some work.",
                Thread.CurrentThread.ManagedThreadId);

            // Call EndInvoke to wait for the asynchronous call to complete, 
            // and to retrieve the results. 
            string returnValue = caller.EndInvoke(out threadId, result);


            Console.WriteLine("The EnvInvoke executed on thread {0}, with return value \"{1}\".",
                threadId, returnValue);
        }
        public class AsyncDemo
        {
            // The method to be executed asynchronously. 
            public string TestMethod(int callDuration, out int threadId)
            {
                Console.WriteLine("Test method begins.");
                Thread.Sleep(callDuration);
                threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("The BeginInvoke executed on thread {0}",
              threadId);
                return String.Format("My call time was {0}.", callDuration.ToString());

            }
        }
        // The delegate must have the same signature as the method 
        // it will call asynchronously. 
        public delegate string AsyncMethodCaller(int callDuration, out int threadId);

        public class DelDemo
        {
            delegate void del1(int i);
            delegate int del2();


            public static void fun(int x)
            {
                del1 del1Instance = new del1((int i) => { Console.WriteLine(i); });
                del2 del2Instance = new del2(() => { return new Random().Next(); });

                

                del1Instance(x);
                Console.WriteLine(del2Instance());
                Console.Read();
            }

        }
    }
 }

