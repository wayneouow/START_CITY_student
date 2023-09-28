using LO.Model;
using LO.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LO.View
{
    public interface ILOBuildInfoViewDelegate
    {
        void OnActionButtonClick();
        void OnCancelButtonClick();
    }

    public class LOBuildInfoView : MonoBehaviour
    {
        ILOBuildInfoViewDelegate m_ViewDelegate;

        [SerializeField]
        GameObject m_RootObj;

        [SerializeField]
        RectTransform m_BgRect;

        [SerializeField]
        RectTransform m_HintRect;

        [SerializeField]
        Text m_InfoText;

        [SerializeField]
        Text m_HintText;

        [SerializeField]
        Button m_ActionBtn;

        [SerializeField]
        Text m_ActionBtnText;

        [SerializeField]
        Button m_CancelBtn;

        [SerializeField]
        Text m_CancelBtnText;

        static readonly float PADDING = 20;
        static readonly float HINT_PADDING = 10;
        static readonly float SCALE = 10000;

        public void Init(ILOBuildInfoViewDelegate viewDelegate)
        {
            m_ViewDelegate = viewDelegate;
        }

        public void SetActive(bool active, string text = "")
        {
            m_RootObj.SetActive(active);
            m_InfoText.text = text;
            m_InfoText.rectTransform.SetWidth(m_InfoText.preferredWidth);
            m_InfoText.rectTransform.SetHeight(m_InfoText.preferredHeight);
        }

        public void SetActionBtn(bool active, string text = "")
        {
            m_ActionBtn.gameObject.SetActive(active);
            m_ActionBtnText.text = text;
        }

        public void SetCancelBtn(bool active, string text = "")
        {
            m_CancelBtn.gameObject.SetActive(active);
            m_CancelBtnText.text = text;
        }

        public void SetHintText(bool active, string text = "")
        {
            m_HintRect.gameObject.SetActive(active);
            m_HintText.text = text;
            m_HintRect.SetHeight(m_HintText.preferredHeight + 2 * HINT_PADDING);
        }

        public void ShowBuildingInfo(LOBuildingModelBase buildingModel)
        {
            var infoText = buildingModel.GetInfoText();
            this.SetActive(true, infoText);
            this.SetActionBtn(
                buildingModel.CanMovePeopleOut(),
                buildingModel.GetMovePeopleOutText()
            );
            this.SetCancelBtn(false);
            this.SetHintText(false);
        }

        public void HideBuildingInfo()
        {
            this.SetActive(false);
        }

        public void ShowMoveOutUI(LOBuildingModelBase buildingModel)
        {
            var infoText = buildingModel.GetMovingOutInfoText();
            this.SetActive(true, infoText);
            this.SetActionBtn(false);
            var hintText = buildingModel.GetMovingOutHintText();
            this.SetHintText(true, hintText);
        }

        public void ShowMoveInUI(LOBuildingModelBase buildingModel)
        {
            var infoText = buildingModel.GetMovingInInfoText();
            this.SetActive(true, infoText);
            this.SetActionBtn(true, "確定");
            this.SetCancelBtn(true, "取消");
            this.SetHintText(false);
        }

		#region Unity button handler

        public void HandleActionButtonClick()
        {
            m_ViewDelegate.OnActionButtonClick();
        }

        public void HandleCancelButtonClick()
        {
            m_ViewDelegate.OnCancelButtonClick();
        }

		#endregion
    }
}
