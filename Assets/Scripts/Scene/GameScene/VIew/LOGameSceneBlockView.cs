using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Utils;
using LO.Model;

namespace LO.View
{
    public class LOGameSceneBlockView : MonoBehaviour
    {
        [SerializeField]
        Transform m_Transform;

        [SerializeField]
        public List<LOGameSceneAreaView> ChildArea = new List<LOGameSceneAreaView>();

        [SerializeField]
        float m_AreaPos = 0.3f;

        ILOGameBlockModel m_Model;

        public void Init(ILOGameBlockModel model, Vector3 pos)
        {
            m_Model = model;
            m_Transform.SetLocalPosition(pos);
            InitArea();
        }

        void InitArea()
        {
            Vector2[] posMap = new Vector2[4]
            {
                new Vector2(m_AreaPos, m_AreaPos),
                new Vector2(m_AreaPos, -m_AreaPos),
                new Vector2(-m_AreaPos, m_AreaPos),
                new Vector2(-m_AreaPos, -m_AreaPos)
            };

            for (int i = 0; i < 4; i++)
            {
                ChildArea[i].Init(m_Model.ChildArea[i], posMap[i]);
            }
        }

        public void HandleGameTimeChange(float gameTime)
        {
            foreach (var area in ChildArea)
            {
                area.HandleGameTimeChange(gameTime);
            }
        }
    }
}
