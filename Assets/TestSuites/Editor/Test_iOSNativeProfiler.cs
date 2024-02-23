using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test_iOSNativeProfiler
{
    // A Test behaves as an ordinary method
    [Test]
    public void Should_Success__When_Create_New_iOSNativeProfiler()
    {
        var instance = new iOSNativeRuntimeProfiler();
        Assert.That(instance, Is.Not.Null);
    }

    [Test]
    public void Should_Success__When_Create_New_Factory()
    {
        var factory = new iOSNativeRuntimeProfilerFactory();
        Assert.That(factory, Is.Not.Null);
        Assert.That(factory, Is.InstanceOf<INativeRuntimeProfilerFactory>());
    }

    [Test]
    public void Should_Return_iOSNativeRuntimeProfiler__When_Factory_GetInstance()
    {
        INativeRuntimeProfilerFactory factory = new iOSNativeRuntimeProfilerFactory();
        Assert.That(factory, Is.Not.Null);

        var statsReader = factory.GetInstance();
        Assert.That(statsReader, Is.Not.Null);
        Assert.That(statsReader, Is.InstanceOf<iOSNativeRuntimeProfiler>());
    }

    [Test]
    public void Should_Throws_Exception__When_Read_Stats()
    {
        INativeRuntimeProfilerFactory factory = new iOSNativeRuntimeProfilerFactory();
        Assert.That(factory, Is.Not.Null);

        var statsReader = factory.GetInstance();
        Assert.That(statsReader, Is.Not.Null);

        Assert.That(() => statsReader.ReadCpuStats(out NativeCpuStats32 stats), Throws.Exception.TypeOf<NotImplementedException>());
        Assert.That(() => statsReader.ReadGpuStats(out NativeGpuStats stats), Throws.Exception.TypeOf<NotImplementedException>());
        Assert.That(() => statsReader.ReadMemoryStats(out NativeMemoryStats stats), Throws.Exception.TypeOf<NotImplementedException>());
    }
}
