using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iOSNativeRuntimeProfilerFactory : INativeRuntimeProfilerFactory
{
    INativeRuntimeProfilerReadable INativeRuntimeProfilerFactory.GetInstance() => new iOSNativeRuntimeProfiler();
}
