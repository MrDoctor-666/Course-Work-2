using System;
using System.Collections.Generic;

namespace Modeling
{
    class Factory
    {
        int A, B; //minutes
                  //each A +- B minutes factiry gets new details
        int minutesToWork = 8 * 60; //standart to work - 8 hours
        public int minutesWorked { get; private set; } //worked currently
        int[] machAllWorkTime = new int[3]; //current combined work time for each machine
        int[] machIterWT = new int[3]; //work time on each iteraion
        ControlQueue queueControler;
        public Machine[] machines { get; } //three machines

        //lists for further statictic
        public List<Detail> allDetails { get; private set; }
        public List<Detail> readyDetail { get; private set; }
        public void AddToReady(Detail value) { readyDetail.Add(value); }
        public Factory(int[] C, int[] D)
        {
            //ControlQueue.CleanInfo();
            allDetails = new List<Detail>();
            minutesWorked = 0;
            //create 3 machines
            machines = new Machine[3];
            for (int i = 0; i < 3; i++)
            {
                Type types = (Type)Enum.GetValues(typeof(Type)).GetValue(i);
                machines[i] = new Machine(types, C[i], D[i]);
            }
            //creating a controller
            queueControler = new ControlQueue(machines);
        }

        public void SetWorkingParamets(int hoursToWork = 8, int newDetTimeMinutes = 15, int errorTime = 2)
        {
            machAllWorkTime = new int[3];
            machIterWT = new int[3];
            minutesToWork = hoursToWork * 60;
            minutesWorked = 0;
            this.A = newDetTimeMinutes;
            this.B = errorTime;
            allDetails = new List<Detail>();
            queueControler = new ControlQueue(machines);
        }

        public int Work() //one iteration
        {
            //generating random number of random details
            Random rand = new Random();
            List<Detail> newDetails = new List<Detail>();
            int n = rand.Next(1, 4);
            for (int i = 0; i < n; i++)
            {
                Detail newD = new Detail((Type)Enum.GetValues(typeof(Type)).GetValue(rand.Next(0, 3)));
                newDetails.Add(newD);
                allDetails.Add(newD);
            }

            //putting them in a queue
            queueControler.ControlNew(newDetails);

            //this iter working time
            //(aka next step will be in)
            int thisWT = rand.Next(A - B, A + B);
            //process all that can be processed in this time
            bool NeedAnotherTry = WorkOneIteration(0, thisWT);
            WorkOneIteration(1, thisWT);
            if (NeedAnotherTry) WorkOneIteration(0, thisWT);
            WorkOneIteration(2, thisWT);

            minutesWorked += thisWT;
            readyDetail = queueControler.readyDetail;
            //finishing work condition
            if ((minutesWorked+A-B) > minutesToWork) return -1;
            return thisWT;
        }

        //additional to better read the code
        bool WorkOneIteration(int i, int thisWT)
        {
            //returns true if machine is wmpty at some piont of iteration
            //(there are no more details in queue
            int tryP = 0;
            while (true)
            {
                if (machIterWT[i] < (thisWT))
                {
                    tryP = machines[i].Process(queueControler);
                    if (tryP == 0) { machAllWorkTime[i] += (thisWT); machIterWT[i] = 0; return true;  } //if there are no more details in queue
                    else if ((machIterWT[i] + tryP) >= thisWT) { machIterWT[i] = (machIterWT[i] + tryP) - thisWT; machAllWorkTime[i] += thisWT; break; } //if the iteration ends
                    else { machIterWT[i] += tryP;/* machAllWorkTime[i] += thisWT;*/ }
                }
                else
                {
                    machAllWorkTime[i] += thisWT;
                    machIterWT[i] -= thisWT; break;
                }
            }
            return false;
        }
    }
}
