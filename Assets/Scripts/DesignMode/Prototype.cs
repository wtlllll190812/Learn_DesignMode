using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DesignMode.Prototype
{
    //自己写的克隆接口
    interface IClone<T>
    {
        T Clone();
    }
    
    //ICloneable:c#自带的克隆接口
    //Object.MemberwiseClone：创建当前 Object 的浅表副本。
    
    /// <summary>
    /// 可克隆物体基类
    /// </summary>
    public class Cloneable<T>: ICloneable where T : new()
    {
        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns>克隆物体</returns>
        public virtual T ShallowClone()
        {
            return (T)this.MemberwiseClone();
        }
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns>克隆物体</returns>
        public virtual T DeepClone()
        {
            return (T)this.MemberwiseClone();
            #region 网友实现

            /*网友实现：https://blog.csdn.net/dingxiaowei2013/article/details/105628310*/
            // try
            // {
            //     using (Stream memoryStream = new MemoryStream())
            //     {
            //         BinaryFormatter formatter = new BinaryFormatter();
            //         formatter.Serialize(memoryStream, this);
            //         memoryStream.Position = 0;
            //         return (T)formatter.Deserialize(memoryStream);
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Debug.LogError("克隆异常:" + ex.ToString());
            // }
            // return default(T);

            #endregion
        }
        public object Clone()
        {
            return this.MemberwiseClone();//淺拷贝
        }
    }
}
