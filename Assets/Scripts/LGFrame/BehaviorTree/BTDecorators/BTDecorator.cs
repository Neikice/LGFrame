using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LGFrame.BehaviorTree.Decorate
{
    public abstract class BTDecorator : IDecoratorNode
    {
        protected string name;

        protected ITickNode node;

        protected BTResult state;

        public BTResult State { get { return this.state; } set { this.state = value; } }

        public ITickNode RootNode { get { return this.node.RootNode; } }

        public ITickNode ParentNode
        {
            get { return this.node.ParentNode; }
            set { this.node.ParentNode = value; }
        }

        public List<ITickNode> ChildrenNotes { get { return this.node.ChildrenNotes; } }

        public BTDecorator(ITickNode node) : this()
        {
            this.node = node;
        }

        public BTDecorator() { this.name = "DefaultDecorator"; this.state = BTResult.Ready; }

        public abstract BTResult Tick();

        public ITickNode Decorator(ITickNode node)
        {
            return this.node = node;
        }

        //public string TreeStruct(int layer)
        //{
        //    int thislayer = layer + 1;

        //    var str = new StringBuilder();

        //    str.Append("|");

        //    for (int i = 0; i < thislayer; i++)
        //    {
        //        str.Append("-");
        //    }

        //    str.Append(name);

        //    str.Append(this.node.TreeStruct(thislayer));

        //    return str.ToString();
        //}

        public virtual string TreeStruct(int layer)
        {
            int thislayer = layer + 1;

            var str = new StringBuilder();

            str.Append("|");

            for (int i = 0; i < thislayer; i++)
            {
                str.Append("-");
            }

            str.AppendFormat("{0}【{1}】{2}", name, this.State.ToString(), "\n");

            str.Append(this.node.TreeStruct(thislayer));

            return str.ToString();
        }

        public virtual void Clear()
        {
            this.node.Clear();
        }

        public void AddChildNode(ITickNode node)
        {
            this.node.AddChildNode(node);
        }

        public void RemoveNode(ITickNode node)
        {
            this.node.RemoveNode(node);
        }

        public ITickNode Contain(params ITickNode[] list)
        {
            return this.node.Contain(list);
        }

        public bool CheckEnd()
        {
            return this.node.CheckEnd();
        }


    }

}