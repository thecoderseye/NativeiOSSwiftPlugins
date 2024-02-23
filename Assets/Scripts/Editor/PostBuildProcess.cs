using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Text;
using System;
using System.Runtime.InteropServices;

public static class PostBuildProcess
{

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        switch (buildTarget)
        {
            case BuildTarget.iOS:
                using (var postbuild = new iOSPostBuild(buildTarget, buildPath))
                    postbuild.Execute();
                break;

            case BuildTarget.Android:
            default:
                throw new NotSupportedException();
        }
    }


    private class iOSPostBuild : IDisposable
    {
        void IDisposable.Dispose()
        {
            pbx.WriteToFile(pathToProject);
        }

        private readonly PBXProject pbx;
        private readonly string pathToProject;

        public iOSPostBuild(BuildTarget buildTarget, string buildPath)
        {
            pathToProject = PBXProject.GetPBXProjectPath(buildPath);
            //  $"{buildPath}/Unity-iPhone.xcodeproj/project.pbxproj";
            pbx = new PBXProject();
            pbx.ReadFromFile(pathToProject);
        }

        public void Execute()
        {
            var guids = new List<string>();
            var mainTargetGuid = pbx.GetUnityMainTargetGuid();
            guids.Add(mainTargetGuid);

            var frameworkTargetGuid = pbx.GetUnityFrameworkTargetGuid();
            guids.Add(frameworkTargetGuid);

            pbx.SetBuildProperty(guids, "ENABLE_BITCODE", "NO");
        }
    }
}
