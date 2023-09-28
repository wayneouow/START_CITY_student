using System.Collections;
using System.Collections.Generic;
using LO.Meta;
using UnityEngine;

namespace LO.Model
{
    public class LOGameCardModel
    {
        public static LOGameCardModel Create(LOCardMetaBase cardMeta)
        {
            return new LOGameCardModel(cardMeta);
        }

        public LOCardMetaBase CardMeta;

        private LOGameCardModel(LOCardMetaBase cardMeta)
        {
            CardMeta = cardMeta;
        }
    }
}
