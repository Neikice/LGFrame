using UnityEngine;
using System.Collections.Generic;

namespace LGFrame
{
    public class ScriptableObjectsSubAssetSOs<T> :ScriptableObject where T:ScriptableObject
    {
        [DisplayScriptableObjectProperties]
        public List<T> List;
    }
}
