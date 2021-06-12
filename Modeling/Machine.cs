using System;
using System.Collections.Generic;

namespace Modeling
{
    
    class Machine
    {
        Type type;
        //time to process is C+-D
        public int C { get; }
        int D; //minutes
        public Queue<Detail> detailsToProcess { get; }
        
        public Machine(Type typeM, int C, int D)
        {
            if (C < 0) throw new Exception("Cannot create machine");
            this.type = typeM;
            this.C = C;
            this.D = D;
            detailsToProcess = new Queue<Detail>();
        }
        public void AddDetail(Detail detail) //adding smth to queue
        {
            detailsToProcess.Enqueue(detail);
        }
        public int Process(ControlQueue queueControler)//returns time that took to process
        {
            if (detailsToProcess.Count == 0) return 0;

            //processing
            Detail detailToProcess = detailsToProcess.Dequeue();
            detailToProcess.ProcessState(type);
            //controling queue
            queueControler.ControlInMachine(detailToProcess);

            Random rand = new Random();
            return rand.Next(C - D, C + D); //random work time
        }
    }
}
