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
            return parent;
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
            return parent;
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
            return parent;
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
            return parent;
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
            return parent;
        }

        public static ITickNode Action(this ITickNode parent, params Action[] actions)
        {
            var tempnode = new BTAction(parent);
            if (actions.Length > 0)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    parent.AddChild(new BTAction(actions[i]));
                }
            }
            return parent;
        }
    }
}
