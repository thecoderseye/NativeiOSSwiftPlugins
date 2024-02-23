using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NativeRuntimeProfilerListener //+Behaviour
{
    private class Behaviour : MonoBehaviour
    {
        public NativeRuntimeDataObject ProfileData;

        private float interval;
        private float profileTimestamp;
        private float elapsedTime;

        void OnEnable()
        {
        }

        void OnDisable()
        {
        }

        void OnDestroy()
        {
            NativeRuntimeProfiler.Singleton.End();
            GameObject.Destroy(ProfileData);
        }

        void Start()
        {
            interval = 0.5f;

            //ProfileData = ScriptableObject.CreateInstance<NativeRuntimeDataObject>();
            NativeRuntimeProfiler.Singleton.Begin();
            profileTimestamp = Time.realtimeSinceStartup;
            elapsedTime = 0.0f;
        }

        void Update()
        {
            elapsedTime += Time.realtimeSinceStartup - profileTimestamp;
            if (elapsedTime > interval)
            {
                elapsedTime = 0.0f;

                NativeRuntimeProfiler.Singleton.ReadCpuStats(out ProfileData.CpuStats);
                NativeRuntimeProfiler.Singleton.ReadMemoryStats(out ProfileData.MemoryStats);
                NativeRuntimeProfiler.Singleton.ReadGpuStats(out ProfileData.GpuStats);

                ProfileData.Update();
                // Debug.Log($"{Time.realtimeSinceStartup} {ProfileData}");
            }

            profileTimestamp = Time.realtimeSinceStartup;
        }
    }
}
