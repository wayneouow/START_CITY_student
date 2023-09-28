using UnityEngine;

namespace LO.Utils
{
    public static class LOUIExtension
    {
        #region RectTransform

        public static float GetWidth(this RectTransform rect)
        {
            return rect.sizeDelta.x;
        }

        public static float GetHeight(this RectTransform rect)
        {
            return rect.sizeDelta.y;
        }

        public static void SetWidth(this RectTransform rect, float width)
        {
            var sizeDelta = rect.sizeDelta;
            sizeDelta.x = width;
            rect.sizeDelta = sizeDelta;
        }

        public static void SetHeight(this RectTransform rect, float height)
        {
            var sizeDelta = rect.sizeDelta;
            sizeDelta.y = height;
            rect.sizeDelta = sizeDelta;
        }

        public static void SetAnchoredPosition(this RectTransform rect, float newX, float newY)
        {
            rect.anchoredPosition = new Vector2(newX, newY);
        }

        public static void SetAnchoredPositionX(this RectTransform rect, float newX)
        {
            var pos = rect.anchoredPosition;
            pos.x = newX;
            rect.anchoredPosition = pos;
        }

        public static void SetAnchoredPositionY(this RectTransform rect, float newY)
        {
            var pos = rect.anchoredPosition;
            pos.y = newY;
            rect.anchoredPosition = pos;
        }

        public static void SetAnchoredPositionOffsetX(this RectTransform rect, float offsetX)
        {
            var pos = rect.anchoredPosition;
            pos.x += offsetX;
            rect.anchoredPosition = pos;
        }

        public static void SetAnchoredPositionOffsetY(this RectTransform rect, float offsetY)
        {
            var pos = rect.anchoredPosition;
            pos.x += offsetY;
            rect.anchoredPosition = pos;
        }

        #endregion

        #region CanvasGroup

        public static void SetInteract(this CanvasGroup canvasGroup, bool active)
        {
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }

        public static void SetInteract(this CanvasGroup canvasGroup, bool active, float alpha)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.SetInteract(active);
        }

        #endregion
    }
}
