using System;
using DesignMode.ECS;
using UnityEngine;

namespace DefaultNamespace
{
    public class ECSTest: MonoBehaviour
    {
        public Entity E;
        private void Awake()
        {
            E = new Entity();
        }

        private void Update()
        {
            E.Update();
            if(Input.GetKeyDown(KeyCode.A))
                E.AddComponent(new TestComponent(E));
            if (Input.GetKeyDown(KeyCode.G))
            {
               var x= E.GetComponent<TestComponent>();
               x.Log();
            }
        }
    }
}