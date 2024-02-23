using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using System.Diagnostics.Tracing;
using System;

public class NativeRuntimeStatsView : MonoBehaviour
{
    [SerializeField]
    private UIDocument feDocument;

    [SerializeField]
    private ParticleSystem particle;

    private VisualElement frontEnd;

    private Button buttonStartTracking;
    private Button buttonStopTracking;

    private MultiColumnTreeView treeView;

    private Label profileDataView;

    // Start is called before the first frame update
    void Start()
    {
        frontEnd = feDocument.rootVisualElement;
        Assert.IsNotNull(frontEnd);

        treeView = frontEnd.Q<MultiColumnTreeView>("TreeView");
        profileDataView = frontEnd.Q<Label>("ProfileDataView");

        buttonStartTracking = frontEnd.Q<Button>("StartTracking");
        buttonStartTracking.SetEnabled(true);
        buttonStartTracking.clickable.clickedWithEventInfo += OnClicked;
        
        buttonStopTracking = frontEnd.Q<Button>("StopTracking");
        buttonStopTracking.SetEnabled(false);
        buttonStopTracking.clickable.clickedWithEventInfo += OnClicked;

        NativeRuntimeProfiler.Singleton.Begin();

        NativeRuntimeProfiler.Singleton.ReadDeviceInfo(out NativeDeviceInfo info);
        NativeRuntimeProfiler.Singleton.ReadCpuStats(out NativeCpuStats32 cpuStats);
        NativeRuntimeProfiler.Singleton.ReadGpuStats(out NativeGpuStats gpuStats);
        NativeRuntimeProfiler.Singleton.ReadMemoryStats(out NativeMemoryStats memStats);
    }

    private void OnClicked(EventBase @event)
    {
        var eventSource = @event.target as Button;
        if(eventSource == buttonStartTracking)
        {
            buttonStartTracking.SetEnabled(false);
            buttonStopTracking.SetEnabled(true);

            particle.Play();

            var profiler = new NativeRuntimeProfilerListener(profileDataView);
            profiler.Begin();
        }
        else if(eventSource == buttonStopTracking)
        {
            buttonStartTracking.SetEnabled(true);
            buttonStopTracking.SetEnabled(false);

            particle.Stop();

            var profiler = new NativeRuntimeProfilerListener(profileDataView);
            profiler.End();
        }
        else 
        {
            throw new InvalidOperationException();
        }
    }
}
