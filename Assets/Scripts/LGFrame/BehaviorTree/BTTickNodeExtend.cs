using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGFrame.BehaviorTree;

namespace LGFrame.BehaviorTree
{
    public static class TickNodeExtend
    {
        public static ITickNode End(this ITickNode parent)
        {
           return parent.RootNode; 
        }

        public static ITickNode PrioritySelector(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTPrioritySelector(parent);
            if (nodes.Length > 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    tempnode.AddChild(nodes[i]);
                }
            }
            return tempnode;
        }

        public static ITickNode Sequence(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTSequence(parent);
            if (nodes.Length > 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    tempnode.AddChild(nodes[i]);
                }
            }
            return tempnode;
        }

        public static ITickNode Parallel(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTParallel(parent);
            if (nodes.Length > 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    tempnode.AddChild(nodes[i]);
                }
            }
            return tempnode;
        }

        public static ITickNode Condition(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTCondition(parent);
            if (nodes.Length > 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    tempnode.AddChild(nodes[i]);
                }
            }
            return tempnode;
        }

        public static ITickNode Condition(this ITickNode parent,Func<bool> condition)
        {
            var tempnode = new BTCondition(parent, condition);
            return tempnode;
        }

        public static ITickNode Action(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTAction(parent);
            if (nodes.Length > 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    tempnode.AddChild(nodes[i]);
                }
            }
            return tempnode;
        }

        public static ITickNode Action(this ITickNode parent, Action action)
        {
            var tempnode = new BTAction(parent, action);
            return tempnode;
        }

        public static ITickNode Loop(this ITickNode Ticknode, int loopcount)
        {
            var decorator = new Decorate.Loop(Ticknode, loopcount);
            return decorator;

        }

    }
}
