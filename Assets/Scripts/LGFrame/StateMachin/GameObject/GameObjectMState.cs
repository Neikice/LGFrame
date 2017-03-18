using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

namespace LGFrame
{
    [Serializable]
    public class GameObjectMState : MState<GameObject>
    {
        #region Events Class
        [Serializable]
        public class Events
        {
            public UnityEvent EnterEvent_Unity;
            public UnityEvent ExecuteEvent_Unity;
            public UnityEvent ExitEvent_Unity;
        }
        #endregion Events Class

        public Events m_Events;

        protected GameObject gameObject;
        protected Transform transform;
        //子类使用System.Serializable 序列化，unity调用默认无参的构造函数，所以使用自定义方法序列化
        public override void CreateState(GameObject go, StateMachine<GameObject> owner)
        {
            base.CreateState(go, owner);

            this.gameObject = go;
            this.transform = gameObject.transform;
        }

        public GameObject GameObject
        {
            get { return gameObject; }
        }

        public Transform Transform
        {
            get { return transform; }
        }

        public override void Enter()
        {
            base.Enter();
            if (this.m_Events.EnterEvent_Unity != null)
                this.m_Events.EnterEvent_Unity.Invoke();
        }

        public override void Execute()
        {
            base.Execute();
            if (this.m_Events.ExecuteEvent_Unity != null)
                this.m_Events.ExecuteEvent_Unity.Invoke();
        }

        public override void Exit()
        {
            base.Exit();
            if (this.m_Events.ExitEvent_Unity != null)
                this.m_Events.ExitEvent_Unity.Invoke();
        }

    }
}