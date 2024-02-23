using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public partial class NativeRuntimeProfilerListener
{
    private readonly Label dataView;
    private readonly NativeRuntimeDataObject dataSource;
    public NativeRuntimeProfilerListener(Label viewer)
    {
        this.dataView = viewer;
        dataSource = ScriptableObject.CreateInstance<NativeRuntimeDataObject>();
    }

    public void Begin()
    {
        var behaviour = GameObject.FindFirstObjectByType<Behaviour>();
        Assert.IsNull(behaviour);

        var gameObject = new GameObject("__native_runtime_profiler__");
        behaviour = gameObject.AddComponent<Behaviour>();

        Assert.IsNotNull(behaviour);
        behaviour.ProfileData = dataSource;
        
        //var dataObject = behaviour.ProfileData;
        dataView.dataSource = dataSource;
        dataView.SetBinding("text", new DataBinding()
        {
            dataSourcePath = new Unity.Properties.PropertyPath(nameof(NativeRuntimeDataObject.Data)),
            bindingMode = BindingMode.ToTarget
        });
    }

    public void End()
    {
        dataView.ClearBinding("text");

        var behaviour = GameObject.FindFirstObjectByType<Behaviour>();
        Assert.IsNotNull(behaviour);
        GameObject.Destroy(behaviour.gameObject);
    }
}
