using System;
using UnityEngine;
using DesignMode.State;
namespace DefaultNamespace
{
    public class StateTest : MonoBehaviour
    {
        public FsmManager Fsm;

        private void Awake()
        {
            Fsm = new FsmManager();
            RunState rs = new RunState(Fsm,"Run");
            WalkState ws = new WalkState(Fsm,"Walk");
            
            Fsm.AddState(rs);
            Fsm.AddState(ws);
        }

        private void Update()
        {
            Fsm.Update();
        }
    }
    public class RunState : State
    {
        public RunState(FsmManager fsm, string id) : base(fsm, id) { }

        public override void OnEnter()
        {
            Debug.Log($"State:'{StateID}' entered");
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            Debug.Log($"State:'{StateID}' exited");
        }

        public override void Check()
        {
            if(Input.GetKeyDown(KeyCode.W))
                Fsm.SetState("Walk");
        }
    }

    public class WalkState : State
    {
        public WalkState(FsmManager fsm, string id) : base(fsm,  id) { }

        public override void OnEnter()
        {
            Debug.Log($"State:'{StateID}' entered");
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            Debug.Log($"State:'{StateID}' exited");
        }

        public override void Check()
        {
            if(Input.GetKeyDown(KeyCode.R))
                Fsm.SetState("Run");
        }
    }
}