using UnityEngine;
using UniRx;
using System.Collections;

namespace LGFrame
{
    public class UIBase : MonoBehaviour
    {
        [SerializeField]
        protected int groupId;
        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        [SerializeField]
        protected bool isShow = false;
        public bool IsShow { get { return this.isShow; } private set { this.isShow = value; } }

        public virtual void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }

        public virtual void Start()
        {

        }

    }
}