using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;using UniRx;using LGFrame.Toolkit;namespace LGFrame.Toolkit{
    public class SpawnTool : MonoBehaviour
    {
        [SerializeField]
        List<poolSetting> Pool;

        public Transform Tfff;

        [Serializable]
        struct poolSetting
        {
            public string poolName;
            public Transform prefab;
            public Transform parent;
            public int maxCount;
            public int preLoadCount;
        }

        private void Start()
        {
            Debug.Log("Pool Awake");
            for (int i = 0; i < Pool.Count; i++)
            {
                var temp = this.Pool[i];
                var pool = new GameObjectPool(temp.poolName, temp.prefab.gameObject, temp.parent, temp.maxCount);
                Debug.LogFormat("poolName = {0}, maxCount = {1}, preLoadCount = {2}", temp.poolName, temp.maxCount, temp.preLoadCount);
                //pool.PreloadAsync(temp.preLoadCount);
                pool.PreLoad(temp.preLoadCount);

            }
        }


    }
}
