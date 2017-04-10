using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;using UniRx;namespace LGFrame.Toolkit{
    public abstract class UnityComponentPool<T> : ObjectPool<T>
        where T : Component
    {
        T prefab;
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

        public UnityComponentPool(string name,T prefab, Transform hierarchyParent) : base(name)
        {
            this.prefab = prefab;
            this.hierachgParent = hierarchyParent;
        }

        public UnityComponentPool(string name, T prefab, Transform hierarchyParent, int maxCount) : base(name)
        {
            this.prefab = prefab;
            this.hierachgParent = hierarchyParent;
            base.maxPoolCount = maxCount;
        }

        protected override T CreateInstance()
        {
            var temp = GameObject.Instantiate<T>(prefab);

            temp.name += base.InstancesCount + 1;
            return temp;
        }
    }
}
