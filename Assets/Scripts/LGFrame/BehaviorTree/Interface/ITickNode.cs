using System;
using System.Collections.Generic;

namespace LGFrame.BehaviorTree
{
    public interface ITickNode
    {
        ITickNode RootNode { get; }
        ITickNode ParentNode { get; set; }
        List<ITickNode> ChildrenNotes { get; }
        void AddChild(ITickNode node);
        ITickNode Contain(params ITickNode[] list);
        BTResult State { get; set; }
        BTResult Tick();

        bool CheckEnd();
        void Clear();
        string TreeStruct(int layer);
    }
}
