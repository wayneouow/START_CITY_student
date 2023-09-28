using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.Model
{
    public interface ILOGameBlockModel
    {
        List<ILOGameAreaModel> ChildArea { get; }
        int Index { get; }

        // method
    }

    public class LOGameBlockModel : ILOGameBlockModel
    {
        public List<ILOGameAreaModel> ChildArea { get; private set; }
        public int Index { get; private set; }

        public static ILOGameBlockModel Create(int index)
        {
            return new LOGameBlockModel(index);
        }

        private LOGameBlockModel(int index)
        {
            this.Index = index;
            this.ChildArea = new List<ILOGameAreaModel>();

            InitChild();
        }

        void InitChild()
        {
            for (int i = 0; i < 4; i++)
            {
                var newAreaModel = LOGameAreaModel.Create(i, this);
                this.ChildArea.Add(newAreaModel);
            }
        }
    }
}
