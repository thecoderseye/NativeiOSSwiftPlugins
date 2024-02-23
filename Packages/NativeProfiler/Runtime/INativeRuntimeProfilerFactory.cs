using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INativeRuntimeProfilerFactory 
{
    INativeRuntimeProfilerReadable GetInstance();
}
