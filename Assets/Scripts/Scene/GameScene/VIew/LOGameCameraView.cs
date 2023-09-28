using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO.View
{
    public class LOGameCameraView : MonoBehaviour
    {
        [SerializeField]
        Vector2 m_PosVLimit = new Vector2(0, 5);

        [SerializeField]
        Vector2 m_PosHLimit = new Vector2(-3, 3);

        [SerializeField]
        float m_MoveSpeed = 1;

        [SerializeField]
        Transform m_CameraTrans;

        private void FixedUpdate()
        {
            // UP
            if (Input.GetKey(KeyCode.W))
            {
                UpdatePosV(-m_MoveSpeed * Time.deltaTime);
            }
            // DOWN
            if (Input.GetKey(KeyCode.S))
            {
                UpdatePosV(m_MoveSpeed * Time.deltaTime);
            }
            // LEFT
            if (Input.GetKey(KeyCode.A))
            {
                UpdatePosH(m_MoveSpeed * Time.deltaTime);
            }
            // RIGHT
            if (Input.GetKey(KeyCode.D))
            {
                UpdatePosH(-m_MoveSpeed * Time.deltaTime);
            }
        }

        void UpdatePosH(float offset)
        {
            var pos = m_CameraTrans.position;
            pos.x = Mathf.Clamp(pos.x + offset, m_PosHLimit.x, m_PosHLimit.y);
            m_CameraTrans.position = pos;
        }

        void UpdatePosV(float offset)
        {
            var pos = m_CameraTrans.position;
            pos.y = Mathf.Clamp(pos.y + offset, m_PosVLimit.x, m_PosVLimit.y);
            m_CameraTrans.position = pos;
        }
    }
}
