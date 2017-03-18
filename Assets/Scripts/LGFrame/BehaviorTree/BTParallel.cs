using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame.BehaviorTree
{
    /// <summary>
    /// 同时执行所有子Node，有Running返回Running，否则返回success
    /// </summary>
    public class BTParallel : BTNode
    {
        public BTParallel() : base() { this.name = "PrioritySelector"; }

        public BTParallel(BTNode parent) : base(parent) { this.name = "PrioritySelector"; }


        public override BTResult Tick()
        {
            if (this.CheckEnd()) return this.State;

            this.State = BTResult.Running;

            var running = BTResult.Ready;

            for (int i = 0; i < this.ChildrenNotes.Count; i++)
            {
                if (this.ChildrenNotes[i].Tick() == BTResult.Running)
                    running = BTResult.Running;
            }

            if (running == BTResult.Running) return this.State = BTResult.Running;

            this.State = BTResult.Success;

            return this.State;
        }
    }
}
