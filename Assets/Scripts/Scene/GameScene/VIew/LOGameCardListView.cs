using System.Collections;
using System.Collections.Generic;
using LO.Model;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public class LOGameCardListView : MonoBehaviour
    {
        [SerializeField]
        RectTransform m_CardRoot;

        [SerializeField]
        List<LOGameCardView> m_CardList = new List<LOGameCardView>();

        const float CARD_WIDTH = 200;
        const float CARD_SPACE = 10;

        public void Init(LOGameModel gameModel)
        {
            m_CardList = new List<LOGameCardView>();
            gameModel.OnGameCardDrew += HandleGameCardDrew;
        }

        public void RemoveCard(LOGameCardView cardView)
        {
            m_CardList.Remove(cardView);
            GameObject.Destroy(cardView.gameObject);

            UpdateAllCardPosX();
        }

        public void HandleGameCardDrew()
        {
            var card = GameObject
                .Instantiate(LOGameSceneFinder.Instance.PrefabMeta.GameCardPrefab, m_CardRoot)
                .GetComponent<LOGameCardView>();
            var cardModel = LOGameApplication.Instance.GameController.GameModel.CurrentCardModel;

            card.UpdateView(cardModel, this);

            m_CardList.Add(card);
            UpdateAllCardPosX();
        }

        void UpdateAllCardPosX()
        {
            var startPosX = -((m_CardList.Count - 1) * (CARD_WIDTH + CARD_SPACE) / 2);

            for (int i = 0; i < m_CardList.Count; i++)
            {
                m_CardList[i].UpdateCardPosX(startPosX + i * (CARD_WIDTH + CARD_SPACE));
            }
        }
    }
}
