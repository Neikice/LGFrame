using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LGFrame.BehaviorTree
{
    public class BTCondition : BTNode
    {
        public Func<bool> CheckAction;

        protected bool isSuccessful;

        public BTCondition() : base() { this.name = "Condition"; }

        public BTCondition(BTNode parent) : base(parent) { this.name = "Condition"; }

        public override BTResult Tick()
        {
            if (this.CheckEnd()) return this.State;

            this.isSuccessful = this.CheckAction.Invoke();

            if(this.isSuccessful)
            {
                for (int i = 0; i < ChildrenNotes.Count; i++)
                {
                    ChildrenNotes[i].Tick();
                }
            }

            return isSuccessful ? this.State = BTResult.Success : this.State = BTResult.Failure;
        }

    }
}
