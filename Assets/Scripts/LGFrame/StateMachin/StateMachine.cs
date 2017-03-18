using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LGFrame
{
    [System.Serializable]
    public abstract class StateMachine<T>
    {
        public string CurrentStateName;
        protected MState<T> currenState;
        protected MState<T> previousState;
        protected MState<T> golbalState;
        protected Dictionary<string, MState<T>> map;

        protected T controller;
        public T Controller
        {
            get { return this.controller; }
        }
        /// <summary>
        /// 锁定当前的State,不能转变
        /// </summary>
        private bool lockCurrentState = false;

        #region State

        /// <summary>
        /// 当前状态
        /// </summary>
        public virtual MState<T> CurrenState
        {
            get { return currenState; }
            private set
            {
                currenState = value;
                CurrentStateName = currenState.Name;
            }
        }
        /// <summary>
        /// 上一个状态
        /// </summary>
        public MState<T> PreviousState { get { return previousState; } }
        /// <summary>
        /// 全局状态
        /// </summary>
        public MState<T> GolbalState { get { return golbalState; } }
        public Dictionary<string, MState<T>> Map { get { return map; } }
        /// <summary>
        /// 是否锁定当前状态
        /// </summary>
        public bool LockCurrentState
        {
            get { return lockCurrentState; }
            set { lockCurrentState = value; }
        }
        #endregion

        //构造函数
        public StateMachine()
        {
            map = new Dictionary<string, MState<T>>();
            LockCurrentState = false;
        }

        #region 转换状态相关
        public void SetGolbalState(MState<T> state)
        {
            if (golbalState != null)
                golbalState.Exit();
            golbalState = state;
            if (golbalState != null)
                golbalState.Enter();
        }
        public virtual bool ChangeState(MState<T> nextState)
        {
            return ChangeState(nextState, false);
        }
        /// <summary>
        /// 如果要判断为同一状态，不进行切换，调用，true 判断，false 不判断
        /// </summary>
        /// <param name="nextState"></param>
        /// <param name="ignor">是否判断为同一状态</param>
        /// <returns></returns>
        public virtual bool ChangeState(MState<T> nextState, bool isSame)
        {
            if (LockCurrentState)
                return false;

            if (nextState == null)
            {
                Debug.Log("State Machine ===>  nextState is null State");
                return false;
            }
            //if (CurrenState == null)
            //{ }

            if (isSame && nextState.Name == CurrenState.Name)
            {
                //Debug.Log(CurrenState.Name + "    <<=======>>   " + nextState.Name);
                return false;
            }

            previousState = (CurrenState == null) ? nextState : CurrenState;
            CurrenState.Exit();
            //Debug.Log("_currenState = " + _currenState.Name);
            //Debug.Log("nextState = " + _currenState.Name);
            CurrenState = nextState;
            CurrenState.Enter();
            return true;
        }

        public virtual bool ChangeState(string stateName)
        {
            return this.ChangeState(stateName, false);
        }

        public virtual bool ChangeState(string stateName,bool isSame)
        {
            MState<T> state;
            if (map.TryGetValue(stateName, out state))
                return ChangeState(state,isSame);

            Debug.Log("State isn't exsit!");
            return false;
        }

        public bool ChangeState(object _Object, MstateEventArgs args)
        {
            return ChangeState(args.StateName);
        }

        public virtual void UpdateState()
        {
            CurrenState.Execute();
            if (golbalState != null)
                golbalState.Execute();
        }

        public virtual void OnTrigger(Collider other)
        {
            CurrenState.OnTrigger(other);
        }

        public virtual void OnTriggerExit(Collider other)
        {
            CurrenState.OnTriggerExit(other);
        }
        public void RevettoPreviousState()
        {
            ChangeState(previousState);
        }

        public bool isInState(MState<T> State)
        {
            //Debug.Log(State.Name + "<===>  " + _currenState.Name);

            return State.Name.Equals(CurrenState.Name) ? true : false;
        }


        public bool isInState(string State)
        {
            //Debug.Log(State.Name + "<===>  " + _currenState.Name);

            return State.Equals(CurrenState.Name) ? true : false;
        }
        #endregion 转换状态相关

        #region 状态机初始化
        /// <summary>
        /// 初始化所拥有的状态，在CreatSM中调用，具体类实现
        /// </summary>
        /// <param name="gameObject"></param>
        protected virtual void initial(T controller)
        {
            ///initialState( state, gameObject);
            ///ChangeState( state)
        }
        /// <summary>
        /// 初始化状态机，供外部创建状态机时调用
        /// </summary>
        /// <param name="gameObject"></param>
        public virtual void CreateSM(T controller)
        {
            MState<T> startState = new nullMState<T>();
            CurrenState = startState;
            previousState = startState;
            this.controller = controller;
            initial(Controller);
        }
        #endregion 状态机初始化


        #region 增减状态
        //增加，减少状态
        protected bool addState(MState<T> state)
        {
            if (map.ContainsKey(state.Name))
            {
                //Debug.Log(state.Name + "is already exist");
                return false;
            }


            map.Add(state.Name, state);
            return true;
        }

        protected bool delete(MState<T> state)
        {
            return this.Delete(state.Name);
        }

        public bool AddState(MState<T> state ,T controller)
        {
            if (this.controller == null)
            {
                Debug.Log("未初始化controller");
                return false;
            }

            if (map.ContainsKey(state.Name))
            {
                //Debug.Log(state.Name + "is already exist");
                return false;
            }

            if (state.StateMachine == null)
                state.CreateState(controller, this);

            map.Add(state.Name, state);
            return true;
        }

        public bool Delete(string stateName)
        {
            if (!map.ContainsKey(stateName))
            {
                //Debug.Log(state.Name + "is not Contained.");
                return false;
            }
            map.Remove(stateName);
            return true;

        }
            

        #endregion 增减状态
    }
}