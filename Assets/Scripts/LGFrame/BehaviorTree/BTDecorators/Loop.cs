using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.BehaviorTree.Decorate
{
    public class Loop : BTDecorator
    {
        public bool isLimited;

        public int count;

        protected int number;


        #region 构造函数
        public Loop(ITickNode node):base(node)
        {
            this.isLimited = false;

            this.count = 0;

            this.number = 0;

            this.name = "Decorator_Loop";
        }

        public Loop(ITickNode node, int count) : this(node)
        {
            this.isLimited = true;

            this.count = count;
        }

        public Loop()
        {
            this.isLimited = false;

            this.count = 0;

            this.number = 0;

            this.name = "Decorator_Loop";
        }

        public Loop(int count)
        {
            this.isLimited = true;

            this.count = count;

            this.number = 0;

            this.name = "Decorator_Loop";
        }
        #endregion 构造函数

        public override BTResult Tick()
        {
            if (this.node.State == BTResult.Running) return this.State = BTResult.Running;

            if (this.number >= this.count)
                return this.State = BTResult.Success;

            this.number++;

            Debug.LogFormat("count = {1} || number = {0} ", this.number, this.count);

            this.node.Tick();


            return this.State = BTResult.Ready;
        }

    }
}
