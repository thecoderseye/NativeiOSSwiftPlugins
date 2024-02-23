using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

using UnityEngine;
using System.Text;
using System.Drawing;

[Flags]
public enum CpuStatsFlags : UInt32
{
    User = 0x000000ff,
    System = 0x0000ff00,
    Idle = 0x00ff0000,
    Nice = 0xff000000,
}

[Flags]
public enum MemoryStatsFlags : UInt64
{
    Free = 0x000000000000ffff,
    Active = 0x00000000ffff0000,
    Inactive = 0x0000ffff00000000,
    Compressed = 0xffff000000000000,
}

public struct NativeDeviceInfo
{
    public int PhysicalCoresCount;
    public int LogicalCoresCount;
}

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct NativeCpuStats64
{
    public Int16 UserCpu;
    public Int16 SystemCpu;
    public Int16 IdleCpu;
    public Int16 NiceCpu;

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Cpu usage");
        builder.AppendLine($"\tUser {UserCpu}%");
        builder.AppendLine($"\tSystem {SystemCpu}%");
        builder.AppendLine($"\tIdle {IdleCpu}%");
        builder.AppendLine($"\tNice {NiceCpu}%");

        return builder.ToString();
    }
}

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public struct NativeCpuStats32
{
    [FieldOffset(0)]
    public UInt32 Value;

    [FieldOffset(0)]
    //[MarshalAs(UnmanagedType.I2, SizeConst = 2)]
    public Byte UserCpu;

    [FieldOffset(1)]
    //[MarshalAs(UnmanagedType.I2, SizeConst = 2)]
    public Byte SystemCpu;

    [FieldOffset(2)]
    //[MarshalAs(UnmanagedType.I2, SizeConst = 2)]
    public Byte IdleCpu;

    [FieldOffset(3)]
    //[MarshalAs(UnmanagedType.I2, SizeConst = 2)]
    public Byte NiceCpu;

    public NativeCpuStats32(UInt32 value)
    {
        UserCpu = 0;
        SystemCpu = 0;
        IdleCpu = 0;
        NiceCpu = 0;

        Value = value;
    }
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Cpu usage");
        builder.AppendLine($"\tUser {UserCpu}%");
        builder.AppendLine($"\tSystem {SystemCpu}%");
        builder.AppendLine($"\tIdle {IdleCpu}%");
        builder.AppendLine($"\tNice {NiceCpu}%");

        return builder.ToString();
    }
}

public struct NativeGpuStats
{
    public int AllocatedMemory;

    public NativeGpuStats(int allocatedMemory)
    {
        AllocatedMemory = allocatedMemory;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Gpu usage:");
        builder.AppendLine($"\tAllocated Memory {AllocatedMemory:#,#} MB");
        
        return builder.ToString();
    }
}

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public struct NativeMemoryStats
{
    [FieldOffset(0)]
    public UInt64 Value;

    [FieldOffset(0)]
    public UInt16 Free;

    [FieldOffset(2)]
    public UInt16 Active;

    [FieldOffset(4)]
    public UInt16 Inactive;

    [FieldOffset(6)]
    public UInt16 Compressed;

    public NativeMemoryStats(UInt64 packedStats)
    {
        Free = 0;
        Active = 0;
        Inactive = 0;
        Compressed = 0;

        Value = packedStats;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Memory Stats:");
        builder.AppendLine($"\tFree: {Free:#,#} MB");
        builder.AppendLine($"\tActive: {Active:#,#} MB");
        builder.AppendLine($"\tInactive: {Inactive:#,#} MB");
        builder.AppendLine($"\tCompressed: {Compressed:#,#} MB");

        return builder.ToString();
    }
}
