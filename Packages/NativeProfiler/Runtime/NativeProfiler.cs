using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;

public class NativeRuntimeProfiler : INativeRuntimeProfilerReadable
{
    private static readonly Lazy<NativeRuntimeProfiler> instance;

    public static INativeRuntimeProfilerReadable Singleton => instance.Value;

    static NativeRuntimeProfiler()
    {
        instance = new Lazy<NativeRuntimeProfiler>(() => new NativeRuntimeProfiler());
    }

    private readonly INativeRuntimeProfilerReadable statsReader;

    private NativeRuntimeProfiler()
    {
        INativeRuntimeProfilerFactory factory = new
#if UNITY_IOS
        iOSNativeRuntimeProfilerFactory();
#elif UNITY_ANDROID
        AndroidNativeRuntimeStatsFactory();  
#else
        throw new NotSupportedException();
#endif

        statsReader = factory.GetInstance();
    }

    void INativeRuntimeProfilerReadable.ReadCpuStats(out NativeCpuStats32 stats) => statsReader.ReadCpuStats(out stats);

    void INativeRuntimeProfilerReadable.ReadGpuStats(out NativeGpuStats stats) => statsReader.ReadGpuStats(out stats);
        
    void INativeRuntimeProfilerReadable.ReadMemoryStats(out NativeMemoryStats stats) => statsReader.ReadMemoryStats(out stats);

    void INativeRuntimeProfilerReadable.ReadDeviceInfo(out NativeDeviceInfo info) => statsReader.ReadDeviceInfo(out info);

    void INativeRuntimeProfilerReadable.Begin() => statsReader.Begin();

    void INativeRuntimeProfilerReadable.End() => statsReader.End();
} 
