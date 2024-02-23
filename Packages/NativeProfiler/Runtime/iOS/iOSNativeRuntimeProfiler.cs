using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

using Debug = UnityEngine.Debug;

public class iOSNativeRuntimeProfiler : INativeRuntimeProfilerReadable
{
        #region native_methods__declared_C_interface

        [DllImport("__Internal", EntryPoint = "__start_profiling__", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Begin();

        [DllImport("__Internal", EntryPoint = "__stop_profiling__", CallingConvention = CallingConvention.Cdecl)]
        private static extern void End();

        [DllImport("__Internal", EntryPoint = "__read_device_info__", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ReadDeviceInfo();

        [DllImport("__Internal", EntryPoint = "__read_cpu_stats_32__", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt32 ReadCpuStats32();

        [DllImport("__Internal", EntryPoint = "__read_gpu_stats__", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ReadGpuStats();

        [DllImport("__Internal", EntryPoint = "__read_vm_stats__", CallingConvention = CallingConvention.Cdecl)]
        private static extern UInt64 ReadMemoryStats();

        #endregion

        void INativeRuntimeProfilerReadable.ReadCpuStats(out NativeCpuStats32 stats)
        {
#if UNITY_EDITOR
                stats = new NativeCpuStats32((uint)Time.frameCount % 200);
#else
                var result32 = ReadCpuStats32();
                stats = new NativeCpuStats32(result32);
#endif
        }

        void INativeRuntimeProfilerReadable.ReadGpuStats(out NativeGpuStats stats)
        {
#if UNITY_EDITOR
                stats = new NativeGpuStats(Time.frameCount % 100);
#else
                var allocatedMemory = ReadGpuStats();
                stats = new NativeGpuStats(allocatedMemory);
#endif
        }

        void INativeRuntimeProfilerReadable.ReadMemoryStats(out NativeMemoryStats stats)
        {
#if UNITY_EDITOR
                stats = new NativeMemoryStats((uint)Time.frameCount % 200);
#else
                var result = ReadMemoryStats();
                stats = new NativeMemoryStats(result);
#endif
        }

        void INativeRuntimeProfilerReadable.ReadDeviceInfo(out NativeDeviceInfo info)
        {
                info = default(NativeDeviceInfo);

#if UNITY_EDITOR
                //throw new NotImplementedException();
#else
                ReadDeviceInfo();
#endif
        }

        void INativeRuntimeProfilerReadable.Begin()
        {
#if UNITY_EDITOR
                //throw new NotImplementedException();
#else
                Begin();
#endif

        }

        void INativeRuntimeProfilerReadable.End()
        {
#if UNITY_EDITOR
                //throw new NotImplementedException();
#else
                End();
#endif

        }

        public iOSNativeRuntimeProfiler()
        { }


}
