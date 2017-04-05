using System.Collections;using System.Collections.Generic;using UnityEngine;using LGFrame.Toolkit;using System;

public class LogOnePool : UnityComponentPool<LogOne>{
    [SerializeField]
    protected Transform deactiveParent;
    public Transform DeactiveParent { get { return this.deactiveParent; } }

    public LogOnePool(LogOne prefab, Transform hierarchyParent , Transform deactiveParent) : base(prefab, hierarchyParent)
    {
        this.deactiveParent = deactiveParent;
    }

    public LogOnePool(LogOne prefab, Transform hierarchyParent, Transform deactiveParent,int maxCount) : base(prefab, hierarchyParent, maxCount)
    {
        this.deactiveParent = deactiveParent;
    }

    protected override void AfterDespawn(LogOne instance)
    {
        if(this.deactiveParent != null)
            instance.transform.SetParent(this.deactiveParent);

        base.AfterDespawn(instance);
    }
}