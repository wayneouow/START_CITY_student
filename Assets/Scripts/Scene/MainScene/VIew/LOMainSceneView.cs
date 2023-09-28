using System.Collections.Generic;
using DG.Tweening;
using LO.Event;
using LO.Utils;
using UnityEngine;

namespace LO.View
{
    public class LOMainSceneView : MonoBehaviour
    {
        [SerializeField]
        LOMainReportView m_ReportView;

        public void Init()
        {
            m_ReportView.Init();
        }
    }
}
