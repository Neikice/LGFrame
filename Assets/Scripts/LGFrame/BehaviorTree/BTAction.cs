using System;

namespace LGFrame.BehaviorTree
{
    public class BTAction : BTNode
    {
        public Action Action;

        public BTAction() : base() { this.name = "Action"; }

        public BTAction(BTNode parent) : base(parent) { this.name = "Action"; }

        public override BTResult Tick()
        {
            this.Action.Invoke();

            return this.State = BTResult.Success;
        }
    }
}