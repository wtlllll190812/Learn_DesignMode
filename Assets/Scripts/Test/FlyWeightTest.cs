using UnityEngine;
using DesignMode.FlyWeight;

namespace DefaultNamespace
{
    public class FlyWeightTest:MonoBehaviour
    {
        
    }
}

public class Tree : FlyWeight<OutsideData, string>
{
    public Tree(OutsideData outsideData) : base(" qwp",outsideData,"Game") { }
}

public class OutsideData
{
    public int Number;

    public OutsideData(int number)
    { 
        Number=number;
    }
}
