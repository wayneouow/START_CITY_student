using UnityEngine;
using LO.Utils;
using System.Collections.Generic;
using LO.Event;

namespace LO.View
{
    public interface ILODemoSceneCityViewDelegate : ILODemoSceneAreaViewDelegate { }

    public interface ILODemoSceneCityViewEventDispatcher
    {
        event LODisasterEvent OnDisasterOccur;
    }

    public class LODemoSceneCityView : MonoBehaviour
    {
        [SerializeField]
        RectTransform m_RootRect;

        [SerializeField]
        GameObject m_BlockPrefab;

        [SerializeField]
        int m_BlockRows; // TODO : add in meta

        [SerializeField]
        int m_BlockCols;

        [SerializeField]
        Vector2 m_Axis1;
        Vector2 m_Axis2;

        ILODemoSceneCityViewDelegate m_ViewDelegate;

        List<LODemoSceneAreaView> m_AreaViews;

        void Start()
        {
            m_Axis2 = m_Axis1 * new Vector2(-1, 1);
        }

        public void Init(
            ILODemoSceneCityViewDelegate viewDelegate,
            ILODemoSceneCityViewEventDispatcher eventDispatcher
        )
        {
            m_ViewDelegate = viewDelegate;
            eventDispatcher.OnDisasterOccur += HandleDisasterOccur;
            InitBlockPosition();
        }

        void InitBlockPosition()
        {
            m_AreaViews = new List<LODemoSceneAreaView>();

            var offset = ((m_BlockRows - 1) * m_Axis2 + (m_BlockCols - 1) * m_Axis1) / 2;

            for (int i = 0; i < m_BlockRows; i++)
            {
                for (int j = 0; j < m_BlockCols; j++)
                {
                    Vector2 position = j * m_Axis1 + i * m_Axis2 - offset;
                    GameObject g = Instantiate(m_BlockPrefab, m_RootRect);
                    g.GetComponent<RectTransform>().anchoredPosition = position;
                    LODemoSceneAreaView areaView = g.GetComponentInChildren<LODemoSceneAreaView>();
                    areaView.ViewDelegate = m_ViewDelegate;
                    areaView.PositionIndex = i * m_BlockCols + j;
                    m_AreaViews.Add(areaView);
                }
            }

            SetNeighbors();
        }

        void SetNeighbors()
        {
            for (int i = 0; i < m_BlockRows; i++)
            {
                for (int j = 0; j < m_BlockCols; j++)
                {
                    var neighbors = new List<ILODemoSceneAreaView>();
                    for (int m = i - 1; m <= i + 1; m++)
                    {
                        for (int n = j - 1; n <= j + 1; n++)
                        {
                            if (m < 0 || n < 0 || m >= m_BlockRows || n >= m_BlockCols)
                            {
                                continue;
                            }
                            neighbors.Add(m_AreaViews[m * m_BlockCols + n]);
                        }
                    }
                    m_AreaViews[i * m_BlockCols + j].Neighbors3x3 = neighbors;
                }
            }
        }

        void HandleDisasterOccur(int position, float damage)
        {
            LOAudio.Instance.PlayGameDisasterStart();
            if (position < 0 || position >= m_AreaViews.Count)
            {
                return;
            }
            m_AreaViews[position].DoDamage(damage);
        }

        public bool AreAreasNeighbors(int pos1, int pos2)
        {
            if (pos1 == pos2 + 1 || pos1 == pos2 - 1)
            {
                return pos1 / m_BlockCols == pos2 / m_BlockCols;
            }
            if (pos1 == pos2 + m_BlockCols || pos1 == pos2 - m_BlockCols)
            {
                return true;
            }
            return false;
        }
    }
}
