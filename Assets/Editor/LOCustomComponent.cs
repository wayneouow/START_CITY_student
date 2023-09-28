using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LO.Editor
{
    public static class LOCustomComponent
    {
        public static string FONT_PATH =
            "Assets/AddressableResource/Font/TaipeiSansTCBeta-Regular.ttf";

        public static GameObject CreateCustomGameObject(string name, params Type[] components)
        {
            GameObject customGameObject = new GameObject(name, components);

            // 將物件添加到指定的 Hierarchy 中
            GameObject selectedGameObject = Selection.activeGameObject;
            if (selectedGameObject != null)
            {
                customGameObject.transform.SetParent(selectedGameObject.transform);
                var rect = customGameObject.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero;
            }

            Selection.activeGameObject = customGameObject;

            return customGameObject;
        }

        [MenuItem("GameObject/城市遊戲/UI/text")]
        public static void CreateText()
        {
            var text = CreateCustomGameObject("text", typeof(CanvasRenderer), typeof(Text))
                .GetComponent<Text>();
            var font = AssetDatabase.LoadAssetAtPath<Font>(FONT_PATH);
            text.font = font;
            text.raycastTarget = false;
            text.lineSpacing = 1.4f;
            text.supportRichText = false;
        }

        public static void SetCustomFont(GameObject obj)
        {
            var text = obj.GetComponent<Text>();

            if (text == null)
            {
                return;
            }

            var font = AssetDatabase.LoadAssetAtPath<Font>(FONT_PATH);
            text.font = font;
        }

        [MenuItem("GameObject/城市遊戲/Update/設定單選黑體")]
        public static void SetCustomFontToSelect()
        {
            TraverseChildToSetCustomFont(Selection.activeGameObject);
        }

        [MenuItem("GameObject/城市遊戲/Update/設定多選黑體")]
        public static void SetCustomFontsToSelects()
        {
            foreach (var obj in Selection.gameObjects)
            {
                TraverseChildToSetCustomFont(obj);
            }
        }

        static void TraverseChildToSetCustomFont(GameObject obj)
        {
            SetCustomFont(obj);
            foreach (Transform child in obj.transform)
            {
                TraverseChildToSetCustomFont(child.gameObject);
            }
        }
    }
}
