using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class NativeRuntimeDataObject : ScriptableObject
{
    [SerializeField]
    public NativeCpuStats32 CpuStats;
    [SerializeField]
    public NativeMemoryStats MemoryStats;
    [SerializeField]
    public NativeGpuStats GpuStats;

    [SerializeField]
    public string Data; 
    
    void OnDestroy()
    { }

    void OnDisable()
    { }

    void OnEnable()
    { }

    public void Update()
    {
        Data = ToString();
    }
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine(CpuStats.ToString());
        builder.AppendLine(MemoryStats.ToString());
        builder.AppendLine(GpuStats.ToString());

        return builder.ToString();
    }
}
