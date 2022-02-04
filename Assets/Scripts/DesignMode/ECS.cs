using System;
using System.Collections.Generic;
using UnityEngine;


namespace DesignMode.ECS
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Entity
    {
        private List<BaseComponent> _components = new List<BaseComponent>();
        
        /// <summary>
        /// 实体更新
        /// </summary>
        public void Update()
        {
            foreach (var item in _components)
            {
                item.Update();
            }
        }
        
        /// <summary>
        /// 添加组件
        /// </summary>
        /// <param name="component">组件对象</param>
        /// <typeparam name="T">组建类别</typeparam>
        public void AddComponent<T>(T component) where T: BaseComponent
        {
            _components.Add(component);
            component.OnAdd();
        }
        
        /// <summary>
        /// 移除组件
        /// </summary>
        /// <param name="component">组件对象</param>
        /// <typeparam name="T">组件类别</typeparam>
        public void RemoveComponent<T>(T component) where T: BaseComponent
        {
            foreach (var item in _components)
            {
                if (item.GetType() == typeof(T))
                {
                    item.OnRemove();
                    _components.Remove(item);
                    return;
                }
            }
            Debug.Log("There is not the Component you need");
        }
        
        /// <summary>
        /// 获取组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件对象</returns>
        public T GetComponent<T>() where T: BaseComponent
        {
            foreach (var item in _components)
            {
                if (item.GetType() == typeof(T))
                    return (T)item;
            }
            Debug.Log("There is not the Component you need");
            return null;
        }

    }
    /// <summary>
    /// 组件基类
    /// </summary>
    public abstract class BaseComponent
    {
        public Entity Owner;

        public BaseComponent(Entity e) => Owner = e;
        /// <summary>
        /// 组件添加时
        /// </summary>
        public abstract void OnAdd();
        /// <summary>
        /// 组件更新
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// 组件移除时
        /// </summary>
        public abstract void OnRemove();
    }

    public class TestComponent : BaseComponent
    {
        public TestComponent(Entity e) : base(e)
        {
        }

        public override void OnAdd()
        {
            Debug.Log("Component has been added");
        }

        public override void Update()
        {
            Debug.Log("This is a test");   
        }

        public override void OnRemove()
        {
            Debug.Log("Component has been removed");
        }

        public void Log()
        {
            Debug.Log("You got me!!");
        }
    }
}