using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.BehaviorTree
{
    public class BTTreeTemplate : BTTree
    {
        public override void Init()
        {
            root = new BTRoot(this);

            root
                 .Sequence(
                   BTNode.Condition(() => { Debug.Log("第一次Check"); return false; })
                            .Action(() => Debug.Log("第一次Action")),
                   BTNode.Condition(() => { Debug.Log("第二次Check"); return true; })
                            .Action(() => Debug.Log("第二次Action")
               ));

            //var priotick = new BTPrioritySelector();
            //var action3 = new BTAction();
            //{
            //    condition.CheckAction = () => { Debug.Log("第一次Check"); return true; };
            //    action.Action += () => Debug.Log("第一次Action");
            //    condition2.CheckAction = () => { Debug.Log("第二次Check"); return true; };
            //    action2.Action += () => Debug.Log("第二次Action");
            //}

            //var decorate_Timer = new Decorate.Timer_DelayNode(condition2, 3);

            //root.AddChild(leaf);

            //leaf.AddChild(condition);
            //condition.AddChild(action);
            ////condition.AddChild(priotick);
            ////    priotick.AddChild(action3);
            //leaf.AddChild(decorate_Timer);
            //condition2.AddChild(action2);

        }
    }
}
