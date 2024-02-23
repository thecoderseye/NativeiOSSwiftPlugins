using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class AndroidNativeRuntimeProfiler : INativeRuntimeProfilerReadable
{
    void INativeRuntimeProfilerReadable.ReadCpuStats(out NativeCpuStats32 stats)
    {
        throw new System.NotImplementedException();
    }

    void INativeRuntimeProfilerReadable.ReadGpuStats(out NativeGpuStats stats)
    {
        throw new System.NotImplementedException();
    }

    void INativeRuntimeProfilerReadable.ReadMemoryStats(out NativeMemoryStats stats)
    {
        throw new System.NotImplementedException();
    }

    void INativeRuntimeProfilerReadable.ReadDeviceInfo(out NativeDeviceInfo info)
    {
        throw new System.NotImplementedException();
    }

    void INativeRuntimeProfilerReadable.Begin()
    {
        throw new System.NotImplementedException();
    }

    void INativeRuntimeProfilerReadable.End()
    {
        throw new System.NotImplementedException();
    }
}
