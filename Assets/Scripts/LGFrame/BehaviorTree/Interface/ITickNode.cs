using System;
using System.Collections.Generic;

namespace LGFrame.BehaviorTree
{
    public interface ITickNode
    {
        ITickNode ParentNode { get; set; }
        List<ITickNode> ChildrenNotes { get; }
        void AddChild(ITickNode node);
        ITickNode Contain(params ITickNode[] list);
        BTResult State { get; }
        BTResult Tick();
        void Clear();
        string TreeStruct(int layer);
    }
}
