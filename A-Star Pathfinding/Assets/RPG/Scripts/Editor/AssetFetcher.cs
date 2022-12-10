using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetFetcher : MonoBehaviour
{
    [MenuItem("Debug/Log All Scenes")]
    public static void GetAllScenes()
    {
        List<SceneAsset> sceneAssets = GetAllAssetsOfType<SceneAsset>();

        foreach (SceneAsset sceneAsset in sceneAssets)
            Debug.Log(sceneAsset.name);
    }

    [MenuItem("Debug/Log All Prefab")]
    public static void GetAllTextAssets()
    {
        LogAllAssetOfType<GameObject>();
    }

    public static void LogAllAssetOfType<T>() where T : Object
    {
        List<T> assets = GetAllAssetsOfType<T>();

        foreach (T asset in assets)
            Debug.Log(asset.name);
    }

    public static List<T> GetAllAssetsOfType<T>() where T : Object
    {
        List<T> assets = new List<T>();
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            assets.Add(asset);
        }

        return assets;
    }
}
