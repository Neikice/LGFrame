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
        public static void AddNodesToParent(ITickNode parent, ITickNode[] nodes)
        {
            if (nodes.Length > 0)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    parent.AddChildNode(nodes[i]);
                }
            }
        }

        public static ITickNode End(this ITickNode parent)
        {
            return parent.RootNode;
        }

        public static ITickNode PrioritySelector(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTPrioritySelector(parent);
            AddNodesToParent(tempnode, nodes);
            return tempnode;
        }

        public static ITickNode Sequence(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTSequence(parent);
            AddNodesToParent(tempnode, nodes);
            return tempnode;
        }

        public static ITickNode Parallel(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTParallel(parent);
            AddNodesToParent(tempnode, nodes);
            return tempnode;
        }

        public static ITickNode Condition(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTCondition(parent);
            AddNodesToParent(tempnode, nodes);
            return tempnode;
        }

        public static ITickNode Condition(this ITickNode parent, Func<bool> condition)
        {
            return new BTCondition(parent, condition);
        }

        public static ITickNode Action(this ITickNode parent, params ITickNode[] nodes)
        {
            var tempnode = new BTAction(parent);
            AddNodesToParent(tempnode, nodes);
            return tempnode;
        }

        public static ITickNode Action(this ITickNode parent, Action action)
        {
            return new BTAction(parent, action);
        }

        #region IDecoratorNode

        public static ITickNode Loop(this ITickNode Ticknode, int loopcount)
        {
            var tempnode = new Decorate.Loop(Ticknode, loopcount);
            if (Ticknode.ParentNode != null)
            {
                Ticknode.ParentNode.RemoveNode(Ticknode);
                Ticknode.ParentNode.AddChildNode(tempnode);
            }
            return tempnode;
        }

        public static ITickNode Time_Delay(this ITickNode Ticknode, float delayTime)
        {
            var tempnode = new Decorate.Timer_DelayNode(Ticknode, delayTime);
            if (Ticknode.ParentNode != null)
            {
                Ticknode.ParentNode.RemoveNode(Ticknode);
                Ticknode.ParentNode.AddChildNode(tempnode);
            }
            return tempnode;
        }

        #endregion IDecoratorNode
    }
}
