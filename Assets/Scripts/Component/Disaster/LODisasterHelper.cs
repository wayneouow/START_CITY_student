using System.Collections.Generic;
using LO.Meta;
using LO.Model;

namespace LO.Helper
{
	public interface ILODisasterHelperDelegate
	{
		void DisasterGroupOccur(ILODisasterGroupModelBase disasterGroup);
	}

	public interface ILODisasterHelper
	{
		void CheckNextDisasterOccur(float gameTime);
		void Reset();
	}

	public class LODisasterHelper : ILODisasterHelper
	{
		public static LODisasterHelper Create(
			LODisasterSequenceMeta disasterSequenceMeta,
			ILODisasterHelperDelegate helperDelegate
		)
		{
			return new LODisasterHelper(disasterSequenceMeta, helperDelegate);
		}

		ILODisasterHelperDelegate m_DisasterHelperDelegate;
		List<KeyValuePair<float, ILODisasterGroupModelBase>> m_DisasterSequence;

		private int m_NextDisasterIndex = 0;

		private LODisasterHelper(
			LODisasterSequenceMeta disasterSequenceMeta,
			ILODisasterHelperDelegate helperDelegate
		)
		{
			m_DisasterSequence = new List<KeyValuePair<float, ILODisasterGroupModelBase>>();
			foreach (var disasterGroupMeta in disasterSequenceMeta.DisasterGroupList)
			{
				var disasterGroup = LODisasterGroupModelBase.Create(disasterGroupMeta);
				m_DisasterSequence.Add(
					new KeyValuePair<float, ILODisasterGroupModelBase>(
						disasterGroupMeta.OccurrenceTime,
						disasterGroup
					)
				);
			}
			m_DisasterSequence.Sort((x, y) => x.Key.CompareTo(y.Key));

			m_DisasterHelperDelegate = helperDelegate;
		}

		public void Reset()
		{
			m_NextDisasterIndex = 0;
		}

		public void CheckNextDisasterOccur(float gameTime)
		{
			if (m_NextDisasterIndex >= m_DisasterSequence.Count)
			{
				return;
			}
			if (gameTime >= m_DisasterSequence[m_NextDisasterIndex].Value.OccurrenceTime)
			{
				m_DisasterHelperDelegate.DisasterGroupOccur(
					m_DisasterSequence[m_NextDisasterIndex].Value
				);
				m_NextDisasterIndex += 1;
			}
		}
	}
}
