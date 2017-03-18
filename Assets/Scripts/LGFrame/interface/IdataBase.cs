using System.Collections.Generic;

namespace LGFrame
{
    public interface IdataBase<Tkey,TValue>
    {
        Dictionary<Tkey, TValue> GetMap();

        TValue this[Tkey key] { get;}
    }
}
