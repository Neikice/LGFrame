using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.BehaviorTree.Decorate
{
    /// <summary>
    /// 行为树会进入Running状态，直到时间到
    /// </summary>
    public class Timer_DelayTree : BTDecorator
    {
        public float DelayTime;
        public bool canTick;

        #region 构造函数
        public Timer_DelayTree(BTNode node, float delayTime) : base(node)
        {
            this.name = "Decorator_Timer";
            this.DelayTime = delayTime;
            this.canTick = false;
        }

        #endregion 构造函数

        public override BTResult Tick()
        {
            if (this.node.CheckEnd()) return this.State;

            if (canTick)
            {
                this.node.Tick();

                return this.State;
            }
            else if (this.State != BTResult.Running)
            {
                this.State = BTResult.Running;
                Observable.Timer(TimeSpan.FromSeconds(DelayTime)).Take(1)
                    .Subscribe(_ =>
                    {
                        Debug.Log("等了秒 " + DelayTime);
                        this.canTick = true;
                        this.node.RootNode.Tick();
                    });
            }
            return this.State;
        }



        public override void Clear()
        {
            this.canTick = false;
            base.Clear();
        }
    }
}
