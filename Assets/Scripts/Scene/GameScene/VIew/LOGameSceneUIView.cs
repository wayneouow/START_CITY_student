using System.Collections;
using System.Collections.Generic;
using LO.Model;
using UnityEngine;

namespace LO.View
{
    public class LOGameSceneUIView : MonoBehaviour
    {
        public LOGameCardListView CardListView;
        public LOGameStateView StateView;
        public LOGameTestView TestView;

        public void Init(LOGameModel gameModel)
        {
            CardListView.Init(gameModel);
            StateView.Init(gameModel);
            TestView.Init(gameModel);
        }
    }
}
