using UnityEngine;
using System;
using System.Collections.Generic;

using Sirenix.Serialization;
using Sirenix.OdinInspector;

using DesignMode.Command;
using DesignMode.FlyWeight;
// using DesignMode.Achieve;

using Random = UnityEngine.Random;


public class CommandTest : MonoBehaviour
{
    private CommandManager _commandManager;
    private void Awake()
    {
        _commandManager = new CommandManager();
    }

    private void Update()
    {
        HandlerInput();
    }

    private void FixedUpdate()
    {
        _commandManager.Execute();
    }

    private void HandlerInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _commandManager.AddCommand(new RotateCommand(transform, Random.Range(0.0f, 90f), Vector3.up));
        if (Input.GetKeyDown(KeyCode.E))
            _commandManager.AddCommand(new RotateCommand(transform, -Random.Range(0.0f, 90f), Vector3.forward));
        if (Input.GetKeyDown(KeyCode.Z))
            _commandManager.Undo();
        if (Input.GetKeyDown(KeyCode.Y))
            _commandManager.Redo();
    }
}

