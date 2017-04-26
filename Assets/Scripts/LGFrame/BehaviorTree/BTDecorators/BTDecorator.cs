using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LGFrame.BehaviorTree.Decorate
{
    public abstract class BTDecorator : TickNode
    {
        protected string name;

        protected BTNode node;

        public BTResult State { get { return this.node.State; } set { this.node.State = value; } }

        public TickNode ParentNode
        {
            get { return this.node.ParentNode; }
            set { this.node.ParentNode = value; }
        }

        public List<TickNode> ChildrenNotes { get { return this.node.ChildrenNotes; } }

        public BTDecorator(BTNode node)
        {
            this.name = "DefaultDecorator";
            this.node = node;
        }

        public abstract BTResult Tick();

        public string TreeStruct(int layer)
        {
            int thislayer = layer + 1;

            var str = new StringBuilder();

            str.Append("|");

            for (int i = 0; i < thislayer; i++)
            {
                str.Append("-");
            }

            str.Append(name);

            str.Append(this.node.TreeStruct(thislayer));

            return str.ToString();
        }

        public virtual void Clear()
        {
            this.node.Clear();
        }
    }

}