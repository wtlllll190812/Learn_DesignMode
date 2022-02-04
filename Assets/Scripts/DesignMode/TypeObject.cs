using System;
using UnityEngine;

namespace DesignMode.TypeObject
{
    
    /// <summary>
    /// 类型对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    [Obsolete("该程序仅为练习，实际项目应该重新编写",false)]
    public class TypeObject<T> where T:class
    {
        private T _data;

        internal TypeObject(T data)
        {
            _data = data;
        }

        public Owner<T> NewOwner()
        {
            Owner<T> owner;
            owner = new Owner<T>(this);
            return owner;
        }
    }
    
    
    [Obsolete("该程序仅为练习，实际项目应该重新编写",false)]
    public class Owner<T> where T:class
    {
        private TypeObject<T> _data;
        internal Owner(TypeObject<T> data)
        {
            _data = data;
        }
    }
}