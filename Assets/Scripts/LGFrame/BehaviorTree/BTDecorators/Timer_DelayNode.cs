using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.BehaviorTree.Decorate
{
    /// <summary>
    /// 延迟执行修饰的Node，结果立即返回Success或Failure
    /// </summary>
    public class Timer_DelayNode : BTDecorator
    {
        public float DelayTime;

        BTResult returnState;

        #region 构造函数
        public Timer_DelayNode(ITickNode node, float delayTime) : base(node)
        {
            this.name = "Decorator_Timer";
            this.DelayTime = delayTime;
            this.returnState = BTResult.Success;
        }

        #endregion 构造函数

        public override BTResult Tick()
        {
            if (this.node.CheckEnd()) return this.State;

            this.State = BTResult.Running;
            Observable.Interval(TimeSpan.FromSeconds(DelayTime)).Take(1)
                .Subscribe(_ =>
                {
                    Debug.Log("等了秒 " + DelayTime);
                    this.node.Tick();
                });

            return this.returnState;
        }

        public override void Clear()
        {
            base.Clear();
        }
    }
}
