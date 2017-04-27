using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame.BehaviorTree
{
    public  static class BTNodes
    {
        public static ITickNode PrioritySelector()
        {
            return new BTPrioritySelector();
        }

        public static ITickNode Sequence()
        {
            return new BTSequence();
        }

        public static ITickNode Parallel()
        {
            return new BTParallel();
        }

        public static ITickNode Condition(Func<bool> func)
        {
            return new BTCondition(func);
        }

        public static ITickNode Action(Action action)
        {
            return new BTAction(action);
        }

        public static IDecoratorNode Loop()
        {
            return new Decorate.Loop();
        }
    }
}


