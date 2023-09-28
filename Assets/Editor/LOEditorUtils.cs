using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LO.Editor {

    public static class LOEditorPath {

        public static string WithAssets(this string path) {
            return $"Assets/{path}";
        }

        // root
        public static string ADDRESSABLE_ROOT = "AddressableResource";
        // first level folder
        public static string ADDRESSABLE_IMAGE_ROOT => $"{ADDRESSABLE_ROOT}/Images";
        public static string ADDRESSABLE_META_ROOT => $"{ADDRESSABLE_ROOT}/Meta";
        public static string ADDRESSABLE_PREFAB_ROOT => $"{ADDRESSABLE_ROOT}/Prefab";
        // second level folder
        public static string CARD_META_PATH => $"{ADDRESSABLE_META_ROOT}/Cards";
    }

    public class LOEditorUtils {

        public static List<T> FindAssets<T>(params string[] paths) where T : Object {
            string[] assetGUIDs = AssetDatabase.FindAssets("t:" + typeof(T), paths);
            List<T> assets = new List<T>();

            foreach (string guid in assetGUIDs) {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                assets.Add(asset);
            }

            return assets;
        }

    }
}