using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.BehaviorTree
{
    public abstract class BTTree : MonoBehaviour
    {
        /// <summary>
        /// Tick的帧间隔
        /// </summary>
        public int IntervalFrame;

        public BTRoot root;

        public abstract void Init();

        public virtual void Start()
        {
            this.Init();
        }

        public virtual void OnEnable()
        {
            Observable.IntervalFrame(IntervalFrame)
                .TakeUntilDisable(this)
                .Subscribe(_ =>
                    {
                        Debug.Log(this.root.TreeStruct());
                        this.root.Tick();
                    },
                    _ => new System.Exception("BTTree出错了"));
        }
    }
}
