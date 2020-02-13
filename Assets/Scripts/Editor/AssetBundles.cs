using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
using System.IO;
#endif

public class AssetBundles : MonoBehaviour {

    [MenuItem("AssetBundles/Build For Android")]
    static void BuildBundlesAndroid()
    {
        if (!Directory.Exists("Assets/StreamingAssets/Android"))
            Directory.CreateDirectory("Assets/StreamingAssets/Android");
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/Android/", BuildAssetBundleOptions.None, BuildTarget.Android);
    }

    [MenuItem("AssetBundles/Build For Windows")]
    static void BuildBundlesWindows()
    {
        if (!Directory.Exists("Assets/StreamingAssets/Windows"))
            Directory.CreateDirectory("Assets/StreamingAssets/Windows");
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/Windows/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
