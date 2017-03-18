using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LGFrame.BehaviorTree
{
    public class BTRoot : BTNode
    {
        public BTTree Tree { get; private set; }

        public BTRoot(BTTree tree):base()
        {
            this.Tree = tree;
            this.ParentNode = null;
        }

        public override void Clear()
        {
            base.Clear();
            this.State = BTResult.Ready;
            Debug.Log("Clear()");
        }

        public override BTResult Tick()
        {
            if (this.CheckEnd()) { return this.State; }

            var childSate = BTResult.Running;

            for (int i = 0; i < this.ChildrenNotes.Count; i++)
            {
                childSate = this.ChildrenNotes[i].Tick();
            }

            if (childSate == BTResult.Running)
            {
                this.State = BTResult.Running;
            }
            else
            {
                this.Clear();
            }

            return this.State;
        }

        public string TreeStruct()
        {
            return base.TreeStruct(0);
        }
    }
}
