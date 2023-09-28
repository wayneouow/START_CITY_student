using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Model
{
    public interface ILOGameAreaModel
    {
        ILOGameBlockModel ParentBlock { get; }
        int Index { get; }
    }

    public class LOGameAreaModel : ILOGameAreaModel
    {
        public ILOGameBlockModel ParentBlock { get; private set; }
        public int Index { get; private set; }

        public static ILOGameAreaModel Create(int index, ILOGameBlockModel parentBlock)
        {
            return new LOGameAreaModel(index, parentBlock);
        }

        private LOGameAreaModel(int index, ILOGameBlockModel parentBlock)
        {
            this.Index = index;
            this.ParentBlock = parentBlock;
        }
    }
}
