using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Modeling
{
    class Statistic
    {
        public List<Detail>[] allDetails { get; } //sorted in array by type
        public string allD { get; private set; } //same but in string format
        public string[] Queues { get; } //for each machine
        public List<Detail> readyDetails { get; } //just all details
        public float[] procentage { get; private set; } //of ready details
        public Type[] prTypes { get; private set; }
        string fileNameSave;

        public Statistic(Factory factory, string fileName = "log.txt")
        {
            fileNameSave = fileName;
            int i = 0;
            readyDetails = factory.readyDetail;
            procentage = new float[3];
            prTypes = new Type[3];
            Queues = new string[3];
            allDetails = new List<Detail>[3];
            List<Detail>[] detailsInQueue = new List<Detail>[3];

            //getting info about ready details
            var readyD = readyDetails.GroupBy(x => x.type).OrderBy(group => group.Key).Select(y => new { count = y.Count(), detType = y.Key });
            foreach (var elem in readyD)
            {
                if (readyDetails.Count != 0) procentage[i] = (float)elem.count / readyDetails.Count * 100;
                else procentage[i] = 0;
                prTypes[i] = elem.detType; i++;
            }
            //getting info about details in queue (for each machine)
            Machine[] m = factory.machines;
            for (i = 0; i < 3; i++)
            {
                detailsInQueue[i] = m[i].detailsToProcess.ToList();
                foreach (Detail det in detailsInQueue[i])
                    Queues[i] += det.type.ToString() + " ";
            }
            //getting overall info (ab every detail)
            foreach (Detail det in factory.allDetails)
                allD += det.type + " ";
            for (i = 0; i < 3; i++)
                allDetails[i] = ReadyDetailsByType(factory.allDetails, (Type)Enum.GetValues(typeof(Type)).GetValue(i));
        }

        //to print in log file
        public void PrintToFile()
        {
            string writePath = Directory.GetCurrentDirectory() +  @"/" + fileNameSave;
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
                {
                    //sw.WriteLine("Time: " + factory.minutesWorked.ToString());
                    //all details info
                    sw.WriteLine("All details: " + allD.Length / 2);
                    for (int i = 0; i < 3; i++)
                        sw.WriteLine("Details of type " + Enum.GetValues(typeof(Type)).GetValue(i).ToString() + " : " + allDetails[i].Count);
                    //queues in machines current
                    for (int i = 0; i < 3; i++)
                    {
                        sw.WriteLine("Machine " + (i + 1));
                        sw.WriteLine("    Queue: " + Queues[i]);
                    }

                    sw.WriteLine("Ready details: " + readyDetails.Count);
                    for (int i = 0; i < 3; i++)
                        sw.WriteLine(procentage[i].ToString() + "% of type " + prTypes[i].ToString());
                    sw.WriteLine("---------------------------");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void PrintBeginning()
        {
            string writePath = Directory.GetCurrentDirectory() + @"/" + fileNameSave;
            using (StreamWriter sw = new StreamWriter(writePath, true, Encoding.Default))
            {
                sw.WriteLine("***********************");
                sw.WriteLine("Starting to work...");
            }
        }

        public List<Detail> ReadyDetailsByType(List<Detail> toChooseFrom, Type type)
        {
            List<Detail> statD = toChooseFrom.Where(x => (x.type == type)).ToList();
            return statD;
        }
    }
}
