using UnityEngine;
using UniRx;
using System.Collections.Generic;

namespace LGFrame
{
    public class UILinkOther : UIBase 
    {
        [SerializeField]
        List<UIBase> linkUIBases;
        [SerializeField]
        List<Transform> linkTransforms;

        public override void SetActive(bool active)
        {
            base.SetActive(active);
            for (int i = 0; i < this.linkUIBases.Count; i++)
            {
                UI_Manager.Instance.tickUI(linkUIBases[i]);    
            }

            for (int i = 0; i < this.linkTransforms.Count; i++)
            {
                this.linkTransforms[i].gameObject.SetActive(active);
            }
        }

    }
}