using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignMode.State
{
    #region 状态类
    
    /// <summary>
    /// 状态抽象类
    /// </summary>
    public abstract class State
    {
        /// <summary>
        /// 状态编号
        /// </summary>
        public readonly string StateID;
        
        /// <summary>
        /// 所属状态机
        /// </summary>
        public readonly FsmManager Fsm;

        public State(FsmManager fsm, string id)
        {
            StateID = id;
            Fsm = fsm;
        }
        /// <summary>
        /// 进入状态时
        /// </summary>
        public abstract void OnEnter();
        
        /// <summary>
        /// 刷新时
        /// </summary>
        public abstract void OnUpdate();//刷新时
        
        /// <summary>
        /// 退出状态时
        /// </summary>
        public abstract void OnExit();//退出状态时
      
        /// <summary>
        /// 检查状态时
        /// </summary>
        public abstract void Check();//检查状态
    }
    
    /// <summary>
    /// 父状态基类
    /// </summary>
    public class ParentState
    {
        /// <summary>
        /// 状态编号
        /// </summary>
        public readonly string StateID;
        
        /// <summary>
        /// 所属状态机
        /// </summary>
        public readonly FsmManager Fsm;
        
        /// <summary>
        /// 子状态字典
        /// </summary>
        private Dictionary<string, State> _stateDic = new Dictionary<string, State>();
        
        /// <summary>
        /// 当前状态
        /// </summary>
        private State _currentState;
        
        
        public ParentState(FsmManager fsm, string id)
        {
            StateID = id; 
            Fsm = fsm;
        }
        
        /// <summary>
        /// 添加子状态
        /// </summary>
        /// <param name="state">子状态</param>
        public void AddSonState(State state)
        {
            Debug.Log(state.StateID);
            if (_currentState == null)
            {
                _currentState = state;
            }
            if (_stateDic.ContainsKey(state.StateID))
            {
                Debug.Log("The state is exist");
                return;
            }
            _stateDic.Add(state.StateID,state);
        }
        
        /// <summary>
        /// 删除子状态
        /// </summary>
        /// <param name="stateID">子状态ID</param>
        public void DeleteSonState(string stateID)
        {
            if(!_stateDic.ContainsKey(stateID))
            {
                Debug.LogError("无法删除不存在的状态");
                return;
            }
            _stateDic.Remove(stateID);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="stateID">状态ID</param>
        public void SetSonState(string stateID)
        {
            if (_stateDic.ContainsKey(stateID))
            {
                _currentState.OnExit();
                _currentState = _stateDic[stateID];
                _currentState.OnEnter();
            }
            else
            {
                Debug.Log($"Can not find state ‘{stateID}’");
                return;
            }
        }
        
        /// <summary>
        /// 设置当前状态
        /// </summary>
        public virtual void OnUpdate()
        {
            _currentState.OnUpdate();
            _currentState.Check();
        }

        /// <summary>
        /// 状态切换检测
        /// </summary>
        public virtual void Check()
        {
            
        }
    }
    #endregion

    #region 状态机类
    
    /// <summary>
    ///状态机抽象类
    /// </summary>
    public class StateManager
    {
        /// <summary>
        /// 刷新状态机
        /// </summary>
        public virtual void Update(){}
        
        /// <summary>
        /// 添加状态机
        /// </summary>
        /// <param name="state">状态机对象</param>
        public virtual void AddState(State state) { }
        
        /// <summary>
        /// 删除指定状态
        /// </summary>
        /// <param name="stateID">状态ID</param>
        public virtual void DeleteState(string stateID){}
        
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="stateID">状态编号</param>
        public virtual void SetState(string stateID){}
    }
    
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class FsmManager: StateManager
    {
        private Dictionary<string, State> StateDic = new Dictionary<string, State>();
        private State _currentState;
        
        
        /// <summary>
        /// 刷新状态机
        /// </summary>
        public override void Update()
        {
            _currentState.OnUpdate();
            _currentState.Check();
        }
        
        /// <summary>
        /// 添加状态机
        /// </summary>
        /// <param name="state">状态机对象</param>
        public override void AddState(State state)
        {
            Debug.Log(state.StateID);
            if (_currentState == null)
            {
                _currentState = state;
            }
            if (StateDic.ContainsKey(state.StateID))
            {
                Debug.Log("The state is exist");
                return;
            }
            StateDic.Add(state.StateID,state);
        }
        
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="stateID">状态编号</param>
        public override void SetState(string stateID)
        {
            if (StateDic.ContainsKey(stateID))
            {
                _currentState.OnExit();
                _currentState = StateDic[stateID];
                _currentState.OnEnter();
            }
            else
            {
                Debug.Log($"Can not find state ‘{stateID}’");
                return;
            }
        }
        
        /// <summary>
        /// 删除指定状态
        /// </summary>
        /// <param name="stateID">状态ID</param>
        public override void DeleteState(string stateID)
        {
            if(!StateDic.ContainsKey(stateID))
            {
                Debug.LogError("无法删除不存在的状态");
                return;
            }
            StateDic.Remove(stateID);
        }
    }

    /// <summary>
    /// 并发状态机
    /// </summary>
    public class CsmManager: StateManager
    {
        private List<Dictionary<string, State>> _stateLst= new List<Dictionary<string, State>>();
        private State[] _currentState;
        
        public int StateNumber;//并行状态机数量

        public CsmManager(int number)
        {
            StateNumber = number;
            for (int i = 0; i<number; i++)
            { 
                var fState = new Dictionary<string, State>();
                _stateLst.Add(fState);
            }
            _currentState = new State[number];
        }
        
        /// <summary>
        /// 刷新状态机
        /// </summary>
        public override void Update()
        {
            foreach (var item in _currentState)
            {
                item.OnUpdate();
                item.Check();
            }
        }
        
        /// <summary>
        /// 添加状态机
        /// </summary>
        /// <param name="state">状态机对象</param>
        public void AddState(State state,int index)
        {
            Debug.Log(state.StateID);
            if (_currentState[index] == null)
            {
                _currentState[index] = state;
            }
            if (_stateLst[index].ContainsKey(state.StateID))
            {
                Debug.Log("The state is exist");
                return;
            }
            _stateLst[index].Add(state.StateID,state);
        }
        
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="stateID">状态编号</param>
        public  void SetState(string stateID,int index)
        {
            if (_stateLst[index].ContainsKey(stateID))
            {
                _currentState[index].OnExit();
                _currentState[index] = (_stateLst[index])[stateID];
                _currentState[index].OnEnter();
            }
            else
            {
                Debug.Log($"Can not find state ‘{stateID}’");
                return;
            }
        }
        
        /// <summary>
        /// 删除指定状态
        /// </summary>
        /// <param name="stateID">状态ID</param>
        public  void DeleteState(string stateID,int index)
        {
            if(!_stateLst[index].ContainsKey(stateID))
            {
                Debug.LogError("无法删除不存在的状态");
                return;
            }
            _stateLst[index].Remove(stateID);
        }
    }
    

    #endregion
}