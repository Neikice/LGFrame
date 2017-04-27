using System;
using System.Collections.Generic;

namespace LGFrame.BehaviorTree
{
    public interface IDecoratorNode :ITickNode
    {
        ITickNode Decorator(ITickNode node);
    }
}
