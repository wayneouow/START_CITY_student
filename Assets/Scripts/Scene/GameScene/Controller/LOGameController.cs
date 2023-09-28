using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Model;
using LO;
using LO.View;

namespace LO.Controller
{
    public interface ILOGameController
    {
        void StartGame();
        void SelectArea(LOGameSceneAreaView areaView);
        void SelectCard(LOGameCardView cardView);
    }

    public class LOGameController : ILOGameController
    {
        public static ILOGameController Create()
        {
            return new LOGameController();
        }

        public LOGameModel GameModel;

        LOGameSceneAreaView m_SelectedAreaView;
        LOGameCardView m_SelectedCardView;

        private LOGameController()
        {
            GameModel = LOGameModel.Create(LOGameSceneFinder.Instance.GameMeta);
        }

        public void StartGame() { }

        void UnSelectArea(bool isRest = false)
        {
            if (m_SelectedAreaView != null)
            {
                m_SelectedAreaView.HandleUnSelected();
                if (isRest)
                {
                    m_SelectedAreaView = null;
                }
            }
        }

        void UnSelectCard(bool isRest = false)
        {
            if (m_SelectedCardView != null)
            {
                m_SelectedCardView.HandleUnSelected();
                if (isRest)
                {
                    m_SelectedCardView = null;
                }
            }
        }

        void UnSelectAll()
        {
            UnSelectArea(true);
            UnSelectCard(true);
        }

        public void SelectArea(LOGameSceneAreaView areaView)
        {
            UnSelectArea();
            // TODO : refactor select logic
            if (areaView != m_SelectedAreaView)
            {
                m_SelectedAreaView = areaView;
                m_SelectedAreaView.HandleSelected();

                UpdateGameState(LOGameState.BLOCK_SELECT);
            }
            else
            {
                m_SelectedAreaView = null;
                UpdateGameState(LOGameState.DEFAULT);
            }
        }

        public void SelectCard(LOGameCardView cardView)
        {
            UnSelectCard();
            // 如果不是重複選牌的話，才可以將牌選起來。不然就會將牌放下
            if (cardView != m_SelectedCardView)
            {
                m_SelectedCardView = cardView;
                m_SelectedCardView.HandleSelected();

                UpdateGameState(LOGameState.CARD_SELECT);
            }
            else
            {
                m_SelectedCardView = null;
                UpdateGameState(LOGameState.DEFAULT);
            }
        }

        public void AddCoin(float value)
        {
            GameModel.AddCoin(value);
        }

        public void UpdatePeople(int value)
        {
            GameModel.UpdatePeople(value);
        }

        public void AddTime(float value)
        {
            value = Math.Max(value, 0);
            int intValue = (int)value;

            for (int i = 0; i < intValue; i++)
            {
                GameModel.AddTime(1);
            }
        }

        public void UpdateGameState(LOGameState newState)
        {
            var nowState = GameModel.GameState;

            // REFACTOR : 使用狀態機管理
            switch (nowState)
            {
                case LOGameState.DEFAULT:
                    break;
                case LOGameState.BLOCK_SELECT:
                    // 如果點擊地區後點擊卡片，則取消選擇的地區
                    if (newState == LOGameState.CARD_SELECT)
                    {
                        UnSelectArea(true);
                    }
                    break;
                case LOGameState.CARD_SELECT:
                    // 如果點擊卡片後點擊地區，則會觸發卡牌對地區效果。接著清空狀態
                    if (newState == LOGameState.BLOCK_SELECT)
                    {
                        m_SelectedAreaView.HandleCardSelectedToArea(m_SelectedCardView);
                        UpdateGameState(LOGameState.DEFAULT);
                        return;
                    }
                    break;
                case LOGameState.DISASTER_SELECT:
                    // 如果點擊災害後，點擊地區將會對該地區上的建築物造成傷害
                    if (newState == LOGameState.BLOCK_SELECT)
                    {
                        m_SelectedAreaView.HandleDisasterToArea();
                    }
                    return;
                default:
                    break;
            }

            // 如果回到預設狀態，為了確保狀態，取消選擇全部
            if (newState == LOGameState.DEFAULT)
            {
                UnSelectAll();
            }

            GameModel.UpdateGameState(newState);
        }

        public void DrawCard()
        {
            GameModel.DrawCard();
        }

        public bool CheckCanBuild(float cost)
        {
            return GameModel.Coin >= cost;
        }
    }
}
