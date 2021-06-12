using System;

namespace Modeling
{
    enum Type { A = 0, B = 1, C = 2}
    //detail types
    //A is processed only on 1st machine
    //B is processed on 1st and 2nd
    //C is processed on 1st, 2nd and 3rd
    class Detail
    {
        public Type type { get; }
        public bool[] states { get; } //states that can be processed

        public Detail(Type type)
        {
            this.type = type;
            states = new bool[(int)type + 1];
        }
        public bool IfReady() //checks if detail is ready
        {
            foreach (bool st in states)
                if (st == false) return false;
            return true;
        }

        public void ProcessState(Type stateNum)
        {
            //stateNum - what state should be processed
            if (stateNum == Type.C && (states[0] == false || states[1] == false)) throw new Exception("The detail haven't been processed in prev steps");
            states[(int)stateNum] = true;
        }
    }

}
