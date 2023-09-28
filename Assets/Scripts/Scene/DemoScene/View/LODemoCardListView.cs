using UnityEngine;
using LO.Event;
using LO.Model;
using System.Collections.Generic;

namespace LO.View
{
    public interface ILODemoCardListViewEventDispatcher
    {
        event LOBoolEvent OnCardDrew;
    }

    public interface ILODemoCardListViewDataProvider
    {
        LOGameCardModel CurrentCardModel { get; }
    }

    public interface ILODemoCardListViewDelegate : ILODemoCardViewDelegate { }

    public interface ILODemoCardListView
    {
        void Init(
            ILODemoCardListViewEventDispatcher eventDispatcher,
            ILODemoCardListViewDataProvider dataProvider,
            ILODemoCardListViewDelegate viewDelegate
        );
        void RemoveCard(LODemoCardView cardView);
    }

    public class LODemoCardListView : MonoBehaviour, ILODemoCardListView
    {
        [SerializeField]
        RectTransform m_CardRoot;

        [SerializeField]
        List<LODemoCardView> m_CardList = new List<LODemoCardView>();

        const float CARD_WIDTH = 200;
        const float CARD_SPACE = 10;

        ILODemoCardListViewDataProvider m_DataProvider;
        ILODemoCardListViewDelegate m_ViewDelegate;

        public void Init(
            ILODemoCardListViewEventDispatcher eventDispatcher,
            ILODemoCardListViewDataProvider dataProvider,
            ILODemoCardListViewDelegate viewDelegate
        )
        {
            m_DataProvider = dataProvider;
            m_ViewDelegate = viewDelegate;
            m_CardList = new List<LODemoCardView>();
            eventDispatcher.OnCardDrew += HandleGameCardDrew;
        }

        public void RemoveCard(LODemoCardView cardView)
        {
            m_CardList.Remove(cardView);
            GameObject.Destroy(cardView.gameObject);
            // TODO: This can be refined by set visible false without destroy

            UpdateAllCardPosX(true);
        }

        public void Restart()
        {
            foreach (var carView in m_CardList)
            {
                GameObject.Destroy(carView.gameObject);
            }
            m_CardList = new List<LODemoCardView>();
        }

        public void HandleGameCardDrew(bool animation)
        {
            var card = GameObject
                .Instantiate(LODemoSceneFinder.Instance.PrefabMeta.DemoCardPrefab, m_CardRoot)
                .GetComponent<LODemoCardView>();
            var cardModel = m_DataProvider.CurrentCardModel;

            card.UpdateView(cardModel);
            card.ViewDelegate = m_ViewDelegate;

            m_CardList.Add(card);
            UpdateAllCardPosX(animation, true);
        }

        // Known issue: Using a card triggers both remove card and drawing a new card. This causes two animations to be triggered, resulting in a shaking effect.
        void UpdateAllCardPosX(bool animation, bool hasNew = false)
        {
            var startPosX = -((m_CardList.Count - 1) * (CARD_WIDTH + CARD_SPACE) / 2);

            for (int i = 0; i < m_CardList.Count; i++)
            {
                float newPosX = startPosX + i * (CARD_WIDTH + CARD_SPACE);
                if (i == m_CardList.Count - 1 && hasNew)
                {
                    // If it's the last card and there's a new card to be added, show the fly-in animation
                    m_CardList[i].CardFlyIn(newPosX, animation);
                    continue;
                }

                // Otherwise, update the card's position with the basic update animation
                m_CardList[i].UpdateCardPosX(newPosX, animation);
            }
        }
    }
}
