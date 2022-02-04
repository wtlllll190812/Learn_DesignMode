using System.Collections.Generic;
using UnityEngine;


namespace DesignMode.FlyWeight
{
    /// <summary>
    /// 享元模式基类
    /// </summary>
    /// <typeparam name="T">外部数据</typeparam>
    /// <typeparam name="TS">内部数据</typeparam>
    public class FlyWeight<T, TS>where T:class
    {
        public string FlyWeightName;
       
        //内部变量
        public TS Inside;
        
        //外部变量
        protected T Outside;
        public T _Outside
        {
            set => Outside = value;
            get => Outside;
        }

        public FlyWeight(TS inside, T outside,string name)
        {
            Outside = outside;
            Inside = inside; 
            FlyWeightName = name;
        }
    }
    
    /// <summary>
    /// 享元对象工厂
    /// </summary>
    /// <typeparam name="T">外部数据</typeparam>
    /// <typeparam name="TS">内部数据</typeparam>
    public class FlyWeightFactory<T, TS>where T:class
    {
        public Dictionary<string, FlyWeight<T, TS>> FlyWeightDic = new Dictionary<string, FlyWeight<T, TS>>();


        /// <summary>
        /// 添加享元对象
        /// </summary>
        /// <param name="name">对象名称</param>
        /// <param name="inside">内部数据</param>
        /// <param name="outside">外部数据</param>
        /// <returns></returns>
        public FlyWeight<T, TS> AddFlyWeight(string name,TS inside, T outside)
        {
            FlyWeight<T, TS> flyWeight;
            
            if (FlyWeightDic.ContainsKey(name))
            {
                Debug.Log("已存在");
                flyWeight = new FlyWeight<T, TS>( inside, FlyWeightDic[name]._Outside, name);
            }
            else
            {
                flyWeight= new FlyWeight<T, TS>(inside, outside, name);
                FlyWeightDic.Add(name, flyWeight);
                Debug.Log("已创建");
            }
            return flyWeight;
        }
    }
}