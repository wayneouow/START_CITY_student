using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LO.Utils
{
    public static class LOUtils
    {
        public static List<T> ClearList<T>(List<T> list) where T : UnityEngine.Object
        {
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    GameObject.Destroy(item);
                }
            }

            return new List<T>();
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            return list.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public static String GetTimeStringFromSecond(float timeInSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
            string timeString = string.Format(
                "{0:00}:{1:00}:{2:00}",
                (int)timeSpan.TotalHours,
                timeSpan.Minutes,
                timeSpan.Seconds
            );
            return timeString;
        }

        public static void ClampImageSize(Image image, float maxWidth, float maxHeight)
        {
            // 獲取圖片的大小
            float imageWidth = image.sprite.rect.width;
            float imageHeight = image.sprite.rect.height;

            // 計算縮放比例
            float scaleX = Mathf.Clamp(maxWidth / imageWidth, 0, 1);
            float scaleY = Mathf.Clamp(maxHeight / imageHeight, 0, 1);
            float scale = Mathf.Min(scaleX, scaleY);

            // 設置縮放比例
            image.rectTransform.localScale = new Vector3(scale, scale, 1);
        }

        public static void ClampImageSize(
            Image image,
            float maxWidth,
            float minWidth,
            float maxHeight,
            float minHeight
        )
        {
            // 獲取圖片的大小
            float imageWidth = image.sprite.rect.width;
            float imageHeight = image.sprite.rect.height;

            // 計算縮放比例
            float scaleX = minWidth / imageWidth;
            float scaleY = minHeight / imageHeight;
            float scale = Mathf.Max(
                Mathf.Min(scaleX, scaleY),
                Mathf.Min(Mathf.Max(scaleX, scaleY), maxWidth / imageWidth, maxHeight / imageHeight)
            );

            // 設置縮放比例
            image.rectTransform.localScale = new Vector3(scale, scale, 1);
        }
    }

    public static class LOMath
    {
        /// <summary>
        /// 內差
        /// </summary>
        public static float Map(float value, float maxA, float minA, float maxB, float minB)
        {
            return (value - minA) * (maxB - minB) / (maxA - minA) + minB;
        }
    }
}
