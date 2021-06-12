using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling
{
    class ControlQueue
    {
        //for specific working pattern (queues creations)
        public Machine[] machines { get; private set; }
        public List<Detail> readyDetail { get; private set; }

        public ControlQueue(Machine[] machines)
        {
            readyDetail = new List<Detail>();
            this.machines = machines;
        }
        //if details comes from machine
        public void ControlInMachine(Detail detail)
        {
            //if ready (all A details will go here)
            if (detail.IfReady()) { readyDetail.Add(detail); }
            //if it's type C and it was processed on 1st and 2nd machines
            else if (detail.type == Type.C && detail.states[0] && detail.states[1]) machines[2].AddDetail(detail);
            //if detail B was processed on 1st, then we send it on 2nd
            else if (detail.states[0]) machines[1].AddDetail(detail);
            else machines[0].AddDetail(detail);
        }
        //if details have just arrived
        public void ControlNew(List<Detail> newDetails)
        {
            foreach (Detail detail in newDetails)
            {
                if (detail.type == Type.A) machines[0].AddDetail(detail);
                else
                {
                    //check where queue is shorter (in minutes)
                    if (machines[0].detailsToProcess.Count * machines[0].C > machines[1].detailsToProcess.Count * machines[1].C)
                        machines[1].AddDetail(detail);
                    else machines[0].AddDetail(detail);
                }
            }
        }
    }
}
