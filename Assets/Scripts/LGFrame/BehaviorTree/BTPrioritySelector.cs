using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame.BehaviorTree
{
    /// <summary>
    /// 按顺序执行，如果遇到了running或success则返回，不执行后续node
    /// </summary>
    public class BTPrioritySelector : BTNode
    {
        public BTPrioritySelector() : base() { this.name = "PrioritySelector"; }

        public BTPrioritySelector(BTNode parent) : base(parent) { this.name = "PrioritySelector"; }


        public override BTResult Tick()
        {
            if (this.CheckEnd()) return this.State;

            this.State = BTResult.Running;

            for (int i = 0; i < this.ChildrenNotes.Count; i++)
            {
                this.State = this.ChildrenNotes[i].Tick();

                if (this.State == BTResult.Running) return this.State = BTResult.Running;

                if (this.State == BTResult.Success) break;
            }

            return this.State;
        }
    }
}
