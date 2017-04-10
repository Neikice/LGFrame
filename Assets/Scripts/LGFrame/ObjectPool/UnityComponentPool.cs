﻿using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;using UniRx;namespace LGFrame.Toolkit{
    public class UnityComponentPool<T> : ObjectPool<T>
        where T: Component
    {
        [SerializeField]
        Transform hierachgParent;

        protected override void AfterSpawn(T instance)
        {
            Observable.NextFrame().Subscribe(_ =>
            {
                instance.gameObject.SetActive(true);

                instance.transform.SetParent(this.hierachgParent);
            });
        }

        protected override void AfterDespawn(T instance)
        {
            instance.gameObject.SetActive(false);
        }

        public UnityComponentPool(string name, T prefab, Transform hierarchyParent) : base(name)
        {
            base.prefab = prefab;
            this.hierachgParent = hierarchyParent;
        }

        public UnityComponentPool(string name, T prefab, Transform hierarchyParent, int maxCount) : base(name)
        {
            base.prefab = prefab;
            this.hierachgParent = hierarchyParent;
            base.maxPoolCount = maxCount;
        }

        protected override T CreateInstance()
        {
            var temp = GameObject.Instantiate<T>(prefab);

           // var temp1 = GameObject.Instantiate(prefab);

            temp.name += base.InstancesCount + 1;
            return temp;
        }
    }

    public class GameObjectPool : ObjectPool<GameObject>
    {
        [SerializeField]
        Transform hierachgParent;

        protected override void AfterSpawn(GameObject instance)
        {
            Observable.NextFrame().Subscribe(_ =>
            {
                instance.gameObject.SetActive(true);

                instance.transform.SetParent(this.hierachgParent);
            });
        }

        protected override void AfterDespawn(GameObject instance)
        {
            instance.gameObject.SetActive(false);
        }

        public GameObjectPool(string name, GameObject prefab, Transform hierarchyParent) : base(name)
        {
            base.prefab = prefab;
            this.hierachgParent = hierarchyParent;
        }

        public GameObjectPool(string name, GameObject prefab, Transform hierarchyParent, int maxCount) : base(name)
        {
            base.prefab = prefab;
            this.hierachgParent = hierarchyParent;
            base.maxPoolCount = maxCount;
        }

        protected override GameObject CreateInstance()
        {
            var temp = GameObject.Instantiate<GameObject>(prefab);

            temp.name += base.InstancesCount + 1;
            return temp;
        }
    }
}