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
       
       //     MinAvg();
            //Tuple<int, int> num = new Tuple<int, int>(10, 45);
            //num = GCD(num);
            //Console.WriteLine(num.Item1);
            //string line = "3 5 10";
            //string[] tempArr = line.Split(' ');
            //int multiplier1 = Int32.Parse(tempArr[0]);
            //int multiplier2 = Int32.Parse(tempArr[1]);
            //int num = Int32.Parse(tempArr[2]);

            //PrintMultiPle(multiplier1, multiplier2, num);

            for (int i = 0; i < 50;i++ )
            {
                Console.WriteLine(IfFunctionIsCalled10TimesInaMinute());

                Thread.Sleep(5000);
                if (i == 9)
                    Thread.Sleep(35000);
            }

                Console.Read();
       
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


        private static void PrintMultiPle(int multiPlier1, int multiPlier2, int num)
        {
            for (int i = 1; i <= num; i++)
            {
                if (i % multiPlier1 == 0 && i % multiPlier2 == 0)
                {
                    Console.Write("FB ");
                }
                else if (i % multiPlier1 == 0)
                {
                    Console.Write("F ");

                }
                else if (i % multiPlier2 == 0)
                {
                    Console.Write("B ");

                }
                else
                {
                    Console.Write(string.Format("{0} ", i));
                }
            }
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
    }
}
