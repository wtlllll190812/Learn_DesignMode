using System.Collections;
using UnityEngine;
using System.Collections.Generic;

// using IEnumerator = UnityEngine.MonoBehaviour;
namespace DesignMode.EventQueue
{        
    /// <summary>
    /// 声明委托
    /// </summary>
    public delegate void InternalEventDelegate(GameEventBase e);

    /// <summary>
    /// 游戏事件基类
    /// </summary>
    public class GameEventBase
    {
        public event InternalEventDelegate GameEvent;

        /// <summary>
        /// 事件触发
        /// </summary>
        public void OnHandler()
        {
            GameEvent(this);
        }
    }
    
    /// <summary>
    /// 事件队列
    /// </summary>
    public class EventQueue: Singleton.Singleton<EventQueue>
    {
        private Queue<GameEventBase> _eventQueue = new Queue<GameEventBase>();
        private Dictionary<string, GameEventBase> _eventDic = new Dictionary<string, GameEventBase>();

        private void Start()
        {
            StartCoroutine("DealQueue");
        }

        /// <summary>
        /// 事件发生
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="e">事件对象</param>
        public void EventOccurred(string name, GameEventBase e)
        {
            if (_eventDic.ContainsKey(name)&&!_eventQueue.Contains(e))
            {
                _eventQueue.Enqueue(e);
            }
            else
            {
                Debug.Log("The Event is not registered");
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="name">事件名称</param>
        /// <param name="e">事件对象</param>
        public void RegisterEvent(string name, GameEventBase e)
        {
            if(_eventDic.ContainsKey(name))
                Debug.Log("The event had been registered");
            else
                _eventDic.Add(name,e);
        }   
       
        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="name">监听器名称</param>
        /// <param name="han">监听方法</param>
        public void AddListener(string name,InternalEventDelegate han)
        {
            if (_eventDic.ContainsKey(name))
            {
                _eventDic[name].GameEvent += han;
            }
            else
            {
                Debug.LogError("Event is not exist");
            }
        }

        /// <summary>
        /// 清空事件队列
        /// </summary>
        IEnumerator DealQueue()
        {
            while(_eventQueue.Count > 0)
            {
                var e = _eventQueue.Dequeue() ;
                e.OnHandler();
            }
            yield return null;
        }
    }
}