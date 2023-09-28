using System.Collections;
using System.Collections.Generic;
using LO.Event;
using UnityEngine;

namespace LO.Helper
{
	public class LOTimer : MonoBehaviour
	{
		public LOFloatEvent DemoTimeEvent;
		public float TotalTime = 0;
		public float ElapsedTime = 0;
		public float TickPeriod = 0.5f;
		public float TimeRate = 1f;

		bool m_IsPause = false;

		// Start is called before the first frame update
		void Start()
		{

			TotalTime = 0;
		}

		// Update is called once per frame
		void Update()
		{
			if (!m_IsPause)
			{
				var deltaTime = Time.deltaTime * TimeRate;
				TotalTime += deltaTime;
				ElapsedTime += deltaTime;
				if (ElapsedTime > TickPeriod)
				{
					ElapsedTime = 0;
					DemoTimeEvent.Invoke(TotalTime);
				}
			}
		}

		public void Restart()
		{
			TotalTime = 0;
			ElapsedTime = 0;
			DemoTimeEvent.Invoke(TotalTime);
		}

		public void Pause()
		{
			m_IsPause = true;
		}
	}
}

