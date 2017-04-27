using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGFrame;

public class GameObjectStateMachineMono : MonoBehaviour
{
    public bool isSetDefaultState = false;

    [SerializeField]
    protected GameObjectStateMachine stateMachine;
    public GameObjectStateMachine StateMachine
    {
        get { return this.stateMachine; }
    }

    [SerializeField]
    protected List<GameObjectMState> states;
    public List<GameObjectMState> States
    {
        get { return this.states; }
    }

    public GameObjectMState TryGetState(string key)
    {
        MState<GameObject>  temp;
        var isGet = this.StateMachine.Map.TryGetValue(key, out temp);
        if (isGet) return temp as GameObjectMState;

        return null; 
    }

    #region UnityEvent
    protected virtual void Awake()
    {
        this.stateMachine = new GameObjectStateMachine();
        this.stateMachine.CreateSM(this.gameObject);
        //初始化状态
        if (this.States != null)
            for (int i = 0; i < this.States.Count; i++)
            {
                stateMachine.AddState(this.States[i], this.gameObject);
            }
    }

    protected virtual void Start()
    {
        if (this.isSetDefaultState)
            this.States[0].toThis();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        this.stateMachine.UpdateState();
    }
    #endregion UnityEvent

    #region changeState
    public virtual void ChangeState(string state , bool isSame = false)
    {
        this.stateMachine.ChangeState(state,isSame);
    }

    public virtual void ChangeState(MState<GameObject> state,bool isSame = false)
    {
        this.stateMachine.ChangeState(state,isSame);
    }
    #endregion changeState
}
