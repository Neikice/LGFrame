using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame.BehaviorTree
{
    public abstract class BTNode : TickNode
    {
        public string name;

        public TickNode ParentNode { get; set; }

        public virtual TickNode RootNode{
            get
            {
                if(this.ParentNode == null) return this;

                return this.ParentNode.ParentNode;
            }
        }

        public List<TickNode> ChildrenNotes;

        protected BTResult state;

        public BTResult State { get { return this.state; } set { this.state = value; } }

        #region 构造函数
        public BTNode()
        {
            this.name = "Root";
            this.State = BTResult.Ready;
            this.ChildrenNotes = new List<TickNode>();
        }

        public BTNode(BTNode parrent) : base()
        {
            this.ParentNode = parrent;
            parrent.AddChild(this);
        }
        #endregion 构造函数

        public abstract BTResult Tick();

        public virtual void Clear()
        {
            this.State = BTResult.Ready;

            for (int i = 0; i < this.ChildrenNotes.Count; i++)
                this.ChildrenNotes[i].Clear();
        }


        /// <summary>
        /// 判断并返回state状态是不是Success或者Failure， 否则返回Ready
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckEnd()
        {
            if (this.State == BTResult.Success || this.State == BTResult.Failure) return true;

            return false;
        }


        public void AddChild(TickNode node)
        {
            this.ChildrenNotes.Add(node);
            node.ParentNode = this;
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

            str.AppendFormat("{0}【{1}】{2}", name,this.State.ToString(), "\n");

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


