using System;

namespace LGFrame.BehaviorTree
{
    public class BTAction : BTNode
    {
        public Action Action;

        public BTAction() : base() { this.name = "Action"; }

        public BTAction(ITickNode parent) : base(parent) { this.name = "Action"; }

        public BTAction(Action action) : this() { this.Action = action; }

        public BTAction(ITickNode parent , Action action) : this(parent) { this.Action = action; }

        public override BTResult Tick()
        {
            if (this.Action != null)
                this.Action.Invoke();

            return this.State = BTResult.Success;
        }
    }
}