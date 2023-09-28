using LO.Meta;
using LO.Model.Server;
using UnityEngine;

namespace LO.Model
{
    public interface ILOCardModelBase
    {
        // property
        LOCardType CardType { get; }
        string Name { get; }
        string NameKey { get; }
        string Desc { get; }
        string DescKey { get; }
        string ImageName { get; }
        Sprite CardImage { get; }
        Sprite[] GameImage { get; }
        string PrefabName { get; }
        int CardIndex { get; }
        int Number { get; }

        LOCardBasicProperties[] BasicProperties { get; }

        // methods
        void CheckTech();
        ILOCardModelBase Copy();
    }

    public class LOCardModelBase : ILOCardModelBase
    {
        public static string GetTypeName(LOCardType cardType)
        {
            // TODO : update fake translation
            switch (cardType)
            {
                case LOCardType.Building:
                    return "建築卡";
                case LOCardType.Production:
                    return "生產卡";
                case LOCardType.Functional:
                    return "功能卡";
                case LOCardType.Other:
                    return "其他";
                default:
                    return "";
            }
        }

        public static ILOCardModelBase CreateFake(LOCardMetaBase meta)
        {
            var fakeServerData = LOCardServerModel.Create(meta.Identity, 100);

            return Create(meta, fakeServerData);
        }

        public static ILOCardModelBase Create(LOCardMetaBase meta, ILOCardServerModel serverData)
        {
            return new LOCardModelBase(meta, serverData);
        }

        public LOCardType CardType
        {
            get => m_Meta.CardType;
        }
        public string Name
        {
            get => m_Meta.Name;
        }
        public string NameKey
        {
            get => m_Meta.NameKey;
        }
        public string Desc
        {
            get => m_Meta.Desc;
        }
        public string DescKey
        {
            get => m_Meta.DescKey;
        }
        public string ImageName
        {
            get => m_Meta.ImageName;
        }
        public Sprite CardImage
        {
            get => m_Meta.CardImage;
        }
        public Sprite[] GameImage
        {
            get => m_Meta.GameImage;
        }
        public string PrefabName
        {
            get => m_Meta.PrefabName;
        }
        public int CardIndex
        {
            get => m_Meta.CardIndex;
        }
        public int Number { get; private set; }

        public LOCardBasicProperties[] BasicProperties
        {
            get => m_Meta.BasicProperties;
        }

        LOCardMetaBase m_Meta;

        protected LOCardModelBase(LOCardMetaBase meta, ILOCardServerModel serverData)
        {
            m_Meta = meta;

            // NOTE : server data
            Number = serverData.Number;
        }

        /// <summary>
        /// Check the card upgrade by tech tree
        /// </summary>
        public virtual void CheckTech() { }

        public ILOCardServerModel GetServerModel()
        {
            return LOCardServerModel.Create(m_Meta.Identity, Number);
        }

        public virtual ILOCardModelBase Copy()
        {
            return LOCardModelBase.Create(m_Meta, GetServerModel());
        }
    }
}
