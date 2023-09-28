using LO.Model;
using UnityEngine;
using UnityEngine.UI;
using LO.Utils;

namespace LO.View {

    public interface ILOUIDeckCardViewDelegate {

        void OnSelect(int index);
    }

    public interface ILOUIDeckCardView {

        void UpdateView(ILOUIDeckCardViewModel viewModel, Vector2 pos, ILOUIDeckCardViewDelegate viewDelegate);
    }

    public class LOUIDeckCardView : MonoBehaviour, ILOUIDeckCardView {

        public const int CARD_WIDTH = 200;
        public const int CARD_HEIGHT = 250;

        [SerializeField] RectTransform m_RectTrans;
        [SerializeField] Image m_CardImg;
        [SerializeField] Text m_CardNameText;
        [SerializeField] Text m_CardNumText;

        ILOUIDeckCardViewModel m_ViewModel;
        ILOUIDeckCardViewDelegate m_ViewDelegate;

        public void UpdateView(ILOUIDeckCardViewModel viewModel, Vector2 pos, ILOUIDeckCardViewDelegate viewDelegate) {

            m_ViewModel = viewModel;
            m_RectTrans.anchoredPosition = pos;
            m_ViewDelegate = viewDelegate;

            m_CardNameText.text = m_ViewModel.DisplayName;
            m_CardNumText.text = m_ViewModel.Number.ToString();
            LOAddressable.Load<Sprite>(m_ViewModel.ImageName, (sprite) => {

                m_CardImg.sprite = sprite;
            });
        }
    }
}