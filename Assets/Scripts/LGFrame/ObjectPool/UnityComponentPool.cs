using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.Toolkit
{
    public class UnityComponentPool<T> : ObjectPool<T>
        where T : Component
    {
        public Vector3 SpawnWorldPoint;
        Transform hierachgParent;

        protected override void AfterSpawn(T instance)
        {
            instance.transform.SetParent(this.hierachgParent);
            Observable.NextFrame().Subscribe(_ =>
            {
                instance.gameObject.SetActive(true);

            });
        }

        protected override void AfterDespawn(T instance)
        {
            instance.gameObject.SetActive(false);
        }

        public UnityComponentPool(string name,T prefab, Transform hierarchyParent) : base(name,prefab)
        {
            this.hierachgParent = hierarchyParent;
            this.SpawnWorldPoint = Vector3.zero;
        }

        public UnityComponentPool(string name, T prefab, Transform hierarchyParent, int maxCount ) 
            : base(name , prefab,maxCount)
        {
            this.hierachgParent = hierarchyParent;
            this.SpawnWorldPoint = Vector3.zero;
        }

        public UnityComponentPool(string name, T prefab, Transform hierarchyParent, int maxCount,Vector3 spawnWorldPoint)
            :this(name,prefab,hierarchyParent,maxCount)
        {
            this.SpawnWorldPoint = spawnWorldPoint;
        }

        protected override T CreateInstance()
        {
            var temp = GameObject.Instantiate<T>(this.prefab,this.SpawnWorldPoint,Quaternion.identity);

            temp.name += base.InstancesCount + 1;
            return temp;
        }
    }

}
