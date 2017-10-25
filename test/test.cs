using System;
using System.Xml;
using System.Xml.Linq;

namespace test
{
    public class test
    {
        static void Main()
        {
            Context context = new Context();
            
        }
    }

    public class State
    {
        public virtual void DoAction(Context context)
        {
            
        }
    }

    public class StartState : State
    {
        public override void DoAction(Context context)
        {
            Console.WriteLine("start state");
            base.DoAction(context);
        }
    }

    public class StopState : State
    {
        public override void DoAction(Context context)
        {
            Console.WriteLine("StopState");
            base.DoAction(context);
        }
    }

    public class Context
    {
        private State _state;

        public Context()
        {
            _state = null;
        }

        public State _State
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}