using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public interface INativeRuntimeProfilerReadable 
{
    void Begin();
    void End();
    void ReadDeviceInfo(out NativeDeviceInfo info);
    void ReadCpuStats(out NativeCpuStats32 stats);
    void ReadGpuStats(out NativeGpuStats stats);
    void ReadMemoryStats(out NativeMemoryStats stats);
}
