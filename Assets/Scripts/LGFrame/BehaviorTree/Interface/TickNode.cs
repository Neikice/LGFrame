using System;
using System.Collections.Generic;

namespace LGFrame.BehaviorTree
{
    public interface TickNode
    {
        TickNode ParentNode { get; set; }
        BTResult State { get; }
        BTResult Tick();
        void Clear();
        string TreeStruct(int layer);
    }
}
