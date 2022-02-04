using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


// 命令模式
namespace DesignMode.Command
{
    /// <summary>
    /// 命令抽象基类
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// 撤销命令
        /// </summary>
        public abstract void Undo();
        /// <summary>
        /// 重做命令
        /// </summary>
        public abstract void Redo();

        /// <summary>
        /// 命令详细信息
        /// </summary>
        /// <returns>命令信息</returns>
        public abstract string Message();
    }

    /// <summary>
    /// 旋转物体子类
    /// </summary>
    public class RotateCommand : Command
    {
        public Transform target;//目标物体
        public float angle;//旋转角度
        public Vector3 Axis;//旋转轴
        public RotateCommand(Transform target, float angle, Vector3 axis)
        {
            this.target = target;
            this.angle = angle;
            this.Axis = axis;
        }
        public override void Execute()
        {
            target.RotateAround(target.position, Axis, angle);
            Debug.Log($"Command'{ToString()}':  Execute;  \nMessage: " + Message());
        }
        public override void Undo()
        {
            target.RotateAround(target.position, Axis, -angle);
            Debug.Log($"Command'{ToString()}':  Undo; \nMessage: " + Message());
        }

        public override void Redo()
        {
            target.RotateAround(target.position, Axis, angle);
            Debug.Log($"Command'{ToString()}':  Redo; \nMessage: " + Message());
        }

        public override string Message()
        {
            return $"Target:{target.name};  Angle{angle};  Axis{Axis}";
        }
        public override string ToString()
        {
            return "RotateCommand";
        }
    }
    
    /// <summary>
    /// 命令管理器
    /// </summary>
    public class CommandManager
    {
        private  Queue<Command> CommandQueue = new Queue<Command>();//命令队列
        private  List<Command> CommandStackUndo = new List<Command>();//命令栈(用于撤销)
        private  List<Command> CommandStackRedo = new List<Command>();//命令栈(用于重做)
        private int stackSize;//命令栈大小

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size">命令栈大小</param>
        public CommandManager(int size)
        {
            stackSize = size;
        }
        /// <summary>
        /// 向队列中添加命令
        /// </summary>
        /// <param name="command">命令</param>
        public void AddCommand(Command command)
        {
            CommandQueue.Enqueue(command);
        }

        /// <summary>
        /// 执行命令队列
        /// </summary>
        public IEnumerator Execute()
        {
            while (true)
            {
                for (var i = 0; i < CommandQueue.Count; i++)
                {
                    var c = CommandQueue.Dequeue();
                    c.Execute();
                    CommandStackUndo.Add(c);
                    if (CommandStackUndo.Count > stackSize)
                        CommandStackUndo.RemoveAt(0);
                }
                yield return null;
            }
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public  void Undo()
        {
            if (CommandStackUndo.Count > 0)
            {
                var c = CommandStackUndo.Last();
                c.Undo();
                CommandStackUndo.Remove(c);
                CommandStackRedo.Add(c); 
                if (CommandStackRedo.Count > stackSize)
                    CommandStackRedo.RemoveAt(0);
            }
        }

        /// <summary>
        /// 重做
        /// </summary>
        public  void Redo()
        {
            if (CommandStackRedo.Count > 0)
            {
                var c= CommandStackRedo.Last();
                c.Redo();
                CommandStackRedo.Remove(c);
                CommandStackUndo.Add(c);
                if (CommandStackUndo.Count > stackSize)
                    CommandStackUndo.RemoveAt(0);
            }
        }
    }
}
