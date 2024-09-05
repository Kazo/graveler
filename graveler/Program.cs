
namespace graveler
{
    internal class Program
    {
        static int ThreadCount = 0;
        static bool[] ThreadUsed;
        static Thread[] thread;
        static Random seed = new Random();
        static bool loop;
        //static int[] attemptThread;
        static int[] bestThread;

        static void Main(string[] args)
        {
            ThreadCount = Environment.ProcessorCount;
            ThreadUsed = new bool[ThreadCount];
            bestThread = new int[ThreadCount];
            thread = new Thread[ThreadCount];
            Console.WriteLine("Starting at: " + DateTime.Now);

            int best = 0;
            int attempt = 0;

            loop = true;
            while (loop)
            {
                for (int i = 0; i < ThreadCount; i++)
                {
                    if (!ThreadUsed[i])
                    {
                        //Console.WriteLine("Thread " + i + " starting attempt " + attempt + " at " + DateTime.Now);
                        attempt += 1000000;
                        int index = i;
                        ThreadUsed[index] = true;
                        thread[index] = new Thread(() => DoAttemps(index));
                        thread[index].Start();
                    }
                }

                for (int i = 0; i < ThreadCount; i++)
                {
                    if (best < bestThread[i])
                    {
                        best = bestThread[i];

                        if (best >= 177)
                        {
                            loop = false;
                        }
                    }
                }

                if (attempt >= 1000000000)
                {
                    loop = false;
                }

                //wait for all remaining threads to finish.
                if (!loop)
                {
                    for (int i = 0; i < ThreadCount; i++)
                    {
                        thread[i].Join();
                    }
                }

                Thread.Sleep(1);
            }

            Console.WriteLine("Completed at: " + DateTime.Now);
            Console.WriteLine("best: " + best);
            Console.WriteLine("attemps: " + attempt);
            Console.ReadLine();
        }

        private static void DoAttemps(int index)
        {
            Random rand = new Random(seed.Next());
            int par = 0;
            int attempt = 0;
            int best = 0;

            while (par < 177 && attempt < 1000000)
            {
                par = 0;
                for (int i = 0; i < 231; i++)
                {
                    if (rand.Next(4) == 0)
                    {
                        par++;
                        if (best < par)
                        {
                            best = par;
                        }
                    }
                }
                attempt++;
            }

            /*if (attemptThread[index] < attempt)
            {
                attempts[index] = attempt;
            }*/

            if (bestThread[index] < best)
            {
                bestThread[index] = best;
            }

            ThreadUsed[index] = false;
        }
    }
}
