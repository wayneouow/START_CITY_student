using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Utils
{
    public static class LOExtension
    {
        public static void SetLocalPosition(this Transform trans, float newX, float newY)
        {
            trans.localPosition = new Vector2(newX, newY);
        }

        public static void SetLocalPosition(this Transform trans, Vector3 pos)
        {
            trans.localPosition = pos;
        }

        public static void SetLocalPosition(
            this Transform trans,
            float newX,
            float newY,
            float newZ
        )
        {
            trans.localPosition = new Vector3(newX, newY, newZ);
        }

        public static void SetLocalPositionX(this Transform trans, float newX)
        {
            var pos = trans.localPosition;
            pos.x = newX;
            trans.localPosition = pos;
        }

        public static void SetLocalPositionY(this Transform trans, float newY)
        {
            var pos = trans.localPosition;
            pos.y = newY;
            trans.localPosition = pos;
        }

        public static void SetLocalPositionOffsetX(this Transform trans, float offsetX)
        {
            var pos = trans.localPosition;
            pos.x += offsetX;
            trans.localPosition = pos;
        }

        public static void SetLocalPositionOffsetY(this Transform trans, float offsetY)
        {
            var pos = trans.localPosition;
            pos.x += offsetY;
            trans.localPosition = pos;
        }
    }
}
