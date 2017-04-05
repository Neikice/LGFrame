using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;using UniRx;namespace LGFrame.Toolkit{    public abstract class ObjectPool<T>    {        T prefab;        Queue<T> inactiveQueue;        Queue<T> deactiveQueue;        protected int maxPoolCount;        public virtual int MaxPoolCount { get { return maxPoolCount; } }

        /// <summary>
        /// Current pooled object count.
        /// </summary>
        public int InactiveCount
        {
            get
            {
                if (this.inactiveQueue == null) return 0;
                return this.inactiveQueue.Count;
            }
        }
        /// <summary>
        /// inactiveCount+deactiveCount
        /// </summary>
        public int InstancesCount
        {
            get
            {
                var inactiveCount = (this.inactiveQueue == null) ? 0 : this.inactiveQueue.Count;
                var deactiveCount = (this.deactiveQueue == null) ? 0 : this.deactiveQueue.Count;
                return inactiveCount + deactiveCount;
            }
        }

        public ObjectPool()
        {
            this.inactiveQueue = new Queue<T>();
            this.deactiveQueue = new Queue<T>();
            this.maxPoolCount = int.MaxValue;
        }

        public ObjectPool(T prefab,int maxPoolCount = int.MaxValue):this()
        {
            this.prefab = prefab;
            this.maxPoolCount = maxPoolCount;
        }


        /// <summary>
        /// Create instance when needed.
        /// </summary>
        /// <returns></returns>        protected abstract T CreateInstance();        

        public T Spawn()
        {

            T instance = default(T);
            if (this.deactiveQueue.Count > 0)
                instance = this.deactiveQueue.Dequeue();
            else
            {
                if (this.InstancesCount >= this.MaxPoolCount)
                {
                    instance = this.inactiveQueue.Peek();
                    this.Despawn(instance);
                    this.inactiveQueue.Dequeue();
                    instance = this.deactiveQueue.Dequeue();
                }
                else
                {
                    instance = this.CreateInstance();
                }
            }

            this.AfterSpawn(instance);
            this.inactiveQueue.Enqueue(instance);
            return instance;
        }        public void Despawn(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");

            if ((this.inactiveQueue.Count + 1) == this.MaxPoolCount)
            {
                throw new InvalidOperationException("Reached Max PoolSize");
            }

            this.AfterDespawn(instance);
            this.deactiveQueue.Enqueue(instance);
        }        protected abstract void AfterSpawn(T instance);        protected abstract void AfterDespawn(T instance);        /// <summary>
        /// Move inactiveQueue Objects to deactiveQueue;
        /// </summary>        public void Clear()
        {
            while (this.inactiveQueue.Count != 0)
            {
                var instance = this.inactiveQueue.Peek();
                this.Despawn(instance);
            }
        }
        public IObservable<Unit> PreloadAsync(int preLoadCount)
        {
            return Observable.FromMicroCoroutine<Unit>((observer, cancel) => this.PreloadCore(preLoadCount, this.MaxPoolCount, observer, cancel));
        }

        IEnumerator PreloadCore(int preloadCount, int threshold, IObserver<Unit> observer, CancellationToken cancellationToken)
        {
            while (this.InstancesCount < preloadCount && !cancellationToken.IsCancellationRequested)
            {
                var requireCount = preloadCount - this.InstancesCount;
                if (requireCount <= 0) break;

                var createCount = Math.Min(requireCount, threshold);

                for (int i = 0; i < createCount; i++)
                {
                    try
                    {
                        var instance = this.Spawn();
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                        yield break;
                    }
                }
                yield return null; // next frame.
            }

            observer.OnNext(Unit.Default);
            observer.OnCompleted();
        }
    }}