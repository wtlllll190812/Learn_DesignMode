using System;
using System.Collections.Generic;

using Sirenix.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignMode.Achieve
{
    /// <summary>
    /// 成就委托
    /// </summary>
    public delegate void AchieveDelegate(int number);
    
    /// <summary>
    /// 游戏事件
    /// </summary>

    [Serializable]
    public  class AchieveEvent
    {
        public event AchieveDelegate Achieve;
        public int number;

        public AchieveEvent(int number)
        {
            this.number = number;
        }
        public virtual void MeetCondition()
        {
            Achieve(number);
        }
    }

    /// <summary>
    /// 游戏成就类
    /// </summary>
    /// <typeparam name="T">奖励类型</typeparam>
    [Serializable]
    public class GameAchieve<T>
    {
         public string name;//成就名称
         public string describe;//成就描述
         public AchieveKind acKind;//成就枚举
         public T award;//达成成就时的奖励
         public int number;//奖励数量
         public bool isAchieved;//成就是否达成
         public AchieveEvent @event;

        public GameAchieve(string name,string describe,AchieveKind acKind,T award,AchieveEvent gEvent)
        {
            this.name = name;
            this.describe = describe;
            this.acKind = acKind;
            this.award = award;
            @event = gEvent;
            @event.Achieve+=new AchieveDelegate(OnAchieve);
        }
        /// <summary>
        /// 成就达成时
        /// </summary>
        public virtual void OnAchieve(int number)
        {
            isAchieved = true;
            this.number = number;
            Debug.Log($"{name}:  {describe} is achieved with a award{number}");
        }
    }

    /// <summary>
    /// 成就管理器
    /// </summary>
    [Serializable]
    public class AchieveManager:SerializedMonoBehaviour
    {
        public Dictionary<AchieveKind, AchieveEvent> AchieveDic = new Dictionary<AchieveKind, AchieveEvent>();

        public void AddAchieve()
        {
            
        }
    }
        
    /// <summary>
    /// 成就枚举
    /// </summary>
    public enum AchieveKind
    {
        Jump,Move,OneHundred
    }
}