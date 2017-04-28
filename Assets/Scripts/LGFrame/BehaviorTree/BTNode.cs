using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame.BehaviorTree
{
    public abstract partial class BTNode : ITickNode
    {
        public string name;

        public ITickNode ParentNode { get; set; }

        public virtual ITickNode RootNode
        {
            get
            {
                if (this.ParentNode == null) return this;

                return this.ParentNode.RootNode;
            }
        }

        public List<ITickNode> childrenNotes;

        public List<ITickNode> ChildrenNotes { get { return this.childrenNotes; } }

        protected BTResult state;

        public BTResult State { get { return this.state; } set { this.state = value; } }

        #region 构造函数
        public BTNode()
        {
            this.name = "Root";
            this.State = BTResult.Ready;
            this.childrenNotes = new List<ITickNode>();
        }

        public BTNode(ITickNode parrent) : base()
        {
            this.ParentNode = parrent;
            parrent.AddChildNode(this);
        }
        #endregion 构造函数

        public abstract BTResult Tick();

        public virtual void Clear()
        {
            this.State = BTResult.Ready;

            if (this.ChildrenNotes != null)
            {
                for (int i = 0; i < this.ChildrenNotes.Count; i++)
                    this.ChildrenNotes[i].Clear();
            }
        }


        /// <summary>
        /// 判断并返回state状态是不是Success或者Failure， 否则返回Ready
        /// </summary>
        /// <returns></returns>
        public bool CheckEnd()
        {
            if (this.State == BTResult.Success || this.State == BTResult.Failure) return true;

            return false;
        }


        public void AddChildNode(ITickNode node)
        {
            if (this.ChildrenNotes == null) this.childrenNotes = new List<ITickNode>();

            this.ChildrenNotes.Add(node);
            node.ParentNode = this;
        }

        public void RemoveNode(ITickNode node)
        {
            if (this.ChildrenNotes == null) return;

            if (this.ChildrenNotes.Contains(node))
                this.ChildrenNotes.Remove(node);
        }

        public ITickNode Contain(params ITickNode[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                this.AddChildNode(nodes[i]);
            }
            return this;
        }

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

            if (this.ChildrenNotes != null)
            {
                foreach (var item in this.ChildrenNotes)
                {
                    str.Append(item.TreeStruct(thislayer));
                }
            }

            return str.ToString();
        }
    }
}


