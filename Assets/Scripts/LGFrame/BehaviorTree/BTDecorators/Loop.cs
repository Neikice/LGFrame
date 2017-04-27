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
            var node = this.node as BTNode;
            if (node.CheckEnd()) return this.State;

            this.number++;

            this.node.Tick();

            if (this.number > this.count)
                return this.State ;

            return this.State = BTResult.Running;
        }

        public override void Clear()
        {
            this.number = 0;
            base.Clear();
        }
    }
}
