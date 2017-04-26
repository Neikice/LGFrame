using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace LGFrame.Toolkit
{
    public abstract class ObjectPool
    {
        public readonly string name;

        public ObjectPool(string name)
        {
            this.name = name;
            this.AddDictionary();
        }

        void AddDictionary()
        {
            PoolDictionary.Pool.Add(this.name, this);
        }

    }

    public abstract class ObjectPool<T> : ObjectPool
    {
        protected readonly T prefab;
        protected readonly List<T> inactiveQueue;
        protected readonly Queue<T> deactiveQueue;
        protected readonly int maxPoolCount;

        public int MaxPoolCount { get { return this.maxPoolCount; } }
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

        public int DeactiveQueue
        {
            get
            {
                if (this.deactiveQueue == null) return 0;
                return this.deactiveQueue.Count;
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



        public ObjectPool(string name, T prefab, int maxPoolCount = int.MaxValue) : base(name)
        {
            this.inactiveQueue = new List<T>();
            this.deactiveQueue = new Queue<T>();
            this.prefab = prefab;
            this.maxPoolCount = maxPoolCount;
        }


        /// <summary>
        /// Create instance when needed.
        /// </summary>
        /// <returns></returns>
        protected abstract T CreateInstance();

        public T Create()
        {
            T instance = this.CreateInstance();
            this.SpawnAction(instance);
            Observable.IntervalFrame(3).Take(1).Subscribe(_ => this.Despawn(instance));
            return instance;
        }

        protected void SpawnAction(T instance)
        {
            this.AfterSpawn(instance);
            this.inactiveQueue.Add(instance);
        }
        
        protected void DespawnAction(T instance)
        {
            this.AfterDespawn(instance);
            this.Remove(instance);
            this.deactiveQueue.Enqueue(instance);
        }
        public T Spawn()
        {

            T instance = default(T);
            if (this.deactiveQueue.Count > 0)
                instance = this.deactiveQueue.Dequeue();
            else
            {
                if (this.InstancesCount >= this.MaxPoolCount)
                {
                    instance = this.inactiveQueue[0];
                    this.Despawn(instance);
                    this.inactiveQueue.Remove(instance);
                    instance = this.deactiveQueue.Dequeue();
                }
                else
                {
                    instance = this.CreateInstance();
                }
            }

            this.SpawnAction(instance);
            return instance;
        }

        public void Despawn(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");

            //if ((this.inactiveQueue.Count + 1) == this.MaxPoolCount)
            //{
            //    throw new InvalidOperationException("Reached Max PoolSize");
            //}
            
            this.DespawnAction(instance);
        }

        public void Remove(T instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");

            if (this.inactiveQueue.Contains(instance))
                this.inactiveQueue.Remove(instance);
        }

        protected abstract void AfterSpawn(T instance);

        protected abstract void AfterDespawn(T instance);
        /// <summary>
        /// Move inactiveQueue Objects to deactiveQueue;
        /// </summary>
        public void Clear()
        {
            while (this.inactiveQueue.Count != 0)
            {
                var instance = this.inactiveQueue[0];
                this.Despawn(instance);
            }
        }

        public void PreLoad(int preLoadCount)
        {
            MainThreadDispatcher.StartUpdateMicroCoroutine(this.intialPreLoad(preLoadCount));
        }

        IEnumerator intialPreLoad(int preLoadCount)
        {
            while (this.InstancesCount < preLoadCount)
            {
                var requireCount = preLoadCount - this.InstancesCount;
                if (requireCount <= 0) break;

                var createCount = this.MaxPoolCount != 0 ? Math.Min(requireCount, this.MaxPoolCount) : requireCount;

                for (int i = 0; i < createCount; i++)
                {
                    var instance = this.Create();
                                      
                }

                yield return null;
            }

           
        }


        public IObservable<Unit> PreloadAsync(int preLoadCount)
        {
            return Observable.FromMicroCoroutine<Unit>((observer, cancel) => this.PreloadCore(preLoadCount, this.MaxPoolCount, observer, cancel));
            //return Observable.FromMicroCoroutine()
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

    }
}
