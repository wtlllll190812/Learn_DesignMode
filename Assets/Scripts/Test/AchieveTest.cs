using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignMode.Achieve;

using Sirenix.OdinInspector;
using UnityEngine.Serialization;

public class AchieveTest : SerializedMonoBehaviour
{
    [FormerlySerializedAs("GE")] public AchieveEvent ge;
    public GameAchieve<int> Ga;

    // public GameAchieve<int> achieve;
    // [SerializeField]public Dictionary<GameEvent,GameAchieve<int>> test ;
    private void Awake()
    {
        ge = new AchieveEvent(10);
        Ga = new GameAchieve<int>("Test","This is a Test",AchieveKind.Jump,100,ge);
        Ga.@event = ge;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
            ge.MeetCondition();
    }
}
