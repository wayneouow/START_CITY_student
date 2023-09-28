using UnityEngine;

namespace LO.Meta
{
    public enum LOCardType
    {
        Building = 0,
        Production = 1,
        Functional = 2,
        Hospital,
        Other,
    }

    [System.Serializable]
    public class LOCardBasicProperties
    {
        public float HP;
        public float BuildingDuration;
        public float Cost;
        public float RiskOfDisaster;
        public float RiskOfLocation;
    }

    // [CreateAssetMenu(fileName = "card meta", menuName = "城市遊戲/card meta")]
    public class LOCardMetaBase : ScriptableObject
    {
        [Header("Base")]
        public string Identity;

        [Space]
        public LOCardType CardType;

        [Space]
        public string Name;
        public string NameKey;

        [Space]
        public string Desc;
        public string DescKey;

        [Space]
        public string ImageName; // level 1, 2, 3 -> ${ImageName}-1, ${ImageName}-2, ${ImageName}-3

        [Header("遊戲內卡牌上的圖片")]
        public Sprite CardImage;
        public Vector2 ImageSize;

        [Header("遊戲內場景中的圖片，三個等級需要三張圖")]
        public Sprite[] GameImage;
        public string PrefabName;
        public int CardIndex;

        [Header("被災害摧毀時的圖片")]
        public Sprite DestroyedImage;

        [Space]
        public LOCardBasicProperties[] BasicProperties = new LOCardBasicProperties[3];

        // NOTE : this function is for editor to update meta, do not call this in production
        public void EditorUpdateMeta()
        {
            var size = CardImage.rect.size;
            ImageSize = size;

            for (int i = 0; i < GameImage.Length; i++)
            {
                GameImage[i] = CardImage;
            }
        }
    }
}
