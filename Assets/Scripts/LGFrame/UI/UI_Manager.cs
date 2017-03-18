using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
namespace LGFrame
{
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager Instance;

        Dictionary<int, UIBase> groupCurrent;
        Dictionary<string, UIBase> UITransforms;
        
        #region RectTransforms
        //UIMessage
        [SerializeField]
        RectTransform UIMessageParent;
        [SerializeField]
        RectTransform UIMessage_StrMessage;

        #endregion
        #region UnityEvent
        // Use this for initialization
        void Start()
        {
            UI_Manager.Instance = this;
            
            var temp_UIBaseList = this.GetComponentsInChildren<UIBase>();
            this.groupCurrent = new Dictionary<int, UIBase>();
            this.UITransforms = new Dictionary<string, UIBase>(temp_UIBaseList.Length);

            for (int i = 0; i < temp_UIBaseList.Length; i++)
            {
                var UIName = temp_UIBaseList[i].name;
                if (!this.UITransforms.ContainsKey(UIName))
                    this.UITransforms.Add(UIName, temp_UIBaseList[i]);
                this.tickUIOnIntial(temp_UIBaseList[i]);
            }
        }

        #endregion

        #region UIMethod
        public void tickUIUnityEvent(string UI_Name)
        {
            this.tickUI(UI_Name);
        }

        void tickUIOnIntial(UIBase ui)
        {
            if (!ui.IsShow)
            {
                ui.SetActive(ui.IsShow);
                return;
            }

            this.tickUI(ui.name);
        }

        //void
        public UIBase tickUI(string UI_Name)
        {
            Debug.Log("tick UI  Name = " + UI_Name);
            UIBase temp;
            if (UITransforms.TryGetValue(UI_Name, out temp))
            {
                if (groupCurrent.ContainsKey(temp.GroupId))
                {
                    groupCurrent[temp.GroupId].SetActive(false);
                    groupCurrent[temp.GroupId] = temp;
                    groupCurrent[temp.GroupId].SetActive(true);
                }
                else
                {
                    groupCurrent.Add(temp.GroupId, temp);
                    temp.SetActive(true);
                }
            }
            return temp;
        }

        public UIBase tickUI(UIBase UI)
        {
            if (UI == null) return null;

            var tempUI = this.tickUI(UI.name);
            // Debug.Log("tempUI name =" + tempUI.name); 
            if (tempUI == null)
            {
                Debug.Log("UI name =" + UI.name);
                this.UITransforms.Add(UI.name, UI);
                tempUI = this.tickUI(UI.name);
            }
            return tempUI;
        }

        //---------------UIMessage-----------
        public void ShowMessage(string text, Vector3 position)
        {

            //var temp = GamePoolManager.GameObjects.Spawn(UIMessage_StrMessage,
            //    Camera.main.WorldToScreenPoint(position),
            //    Quaternion.identity,
            //    UIMessageParent);
            GameObject temp = new GameObject();
            var temptext = temp.GetComponent<Text>();
            temptext.text = text;
        }

        public void LoadScence(string name)
        {
            this.gameObject.SetActive(false);
        }
        public void LoadMainScence()
        {
           // this.sceneManger.LoadMainScence();
        }
        #endregion
    }
}