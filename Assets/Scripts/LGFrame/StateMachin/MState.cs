using UnityEngine;
using System.Collections;
using System;
namespace LGFrame
{
    [System.Serializable]
    public abstract class MState<T>
    {
        [SerializeField]
        protected string name = "";
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        protected StateMachine<T> stateMachine;
        public StateMachine<T> StateMachine;

        //构造函数
        public MState() { name = initialClassName(); }
        public Action EnterEvent;
        public Action ExecuteEvent;
        public Action ExitEvent;
        //triggerEnter事件
        public Action<Collider> onTriggerAction;
        //triggerExit事件
        public Action<Collider> onTriggerExitAction;
        /// <summary>
        /// 状态进入时调用一次
        /// </summary>
        public virtual void Enter()
        {
            if (EnterEvent != null)
                EnterEvent();
        }
        /// <summary>
        /// 状态每帧调用
        /// </summary>
        public virtual void Execute()
        {
            if (ExecuteEvent != null)
            {
                ExecuteEvent();
            }
        }
        /// <summary>
        /// 状态退出时调用
        /// </summary>
        public virtual void Exit()
        {
            if (ExitEvent != null)
                ExitEvent();
        }
        /// <summary>
        /// trigger进行的时候执行
        /// </summary>
        public void OnTrigger(Collider other)
        {
            if (onTriggerAction != null)
                onTriggerAction(other);
        }

        /// <summary>
        /// trigger退出的时候执行
        /// </summary>
        public void OnTriggerExit(Collider other)
        {
            if (onTriggerExitAction != null)
                onTriggerExitAction(other);
        }


        public void ResetEntertEvent()
        {
            EnterEvent = null;
        }
        public void ResetExecutetEvent()
        {
            ExecuteEvent = null;
        }
        public void ResetExitEvent()
        {
            ExitEvent = null;
        }
        public void ResetTriggerEvent()
        {
            onTriggerAction = null;
        }
        public void ResetTriggerExitEvent()
        {
            onTriggerExitAction = null;
        }
        public virtual string initialClassName()
        {
            return this.GetType().ToString();
        }

        public virtual void toThis()
        {
            if (stateMachine == null) return;
            stateMachine.ChangeState(this, true);
        }

        public bool isInState()
        {
            return stateMachine.isInState(this);
        }

        public virtual void CreateState(T controller, StateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

    }

    public class MstateEventArgs : EventArgs
    {
        string _stateName;
        public string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }
    }
    public class nullMState<T> : MState<T>
    {
        public nullMState() { name = "null"; }
    }
}