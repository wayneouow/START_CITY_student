
using System.Collections.Generic;
using LO.Meta;

namespace LO.Model
{
	public interface ILODisasterGroupModelBase
	{
		float OccurrenceTime { get; }
		List<ILODisasterModelBase> DisasterList { get; }
	}

	public class LODisasterGroupModelBase : ILODisasterGroupModelBase
	{
		public static ILODisasterGroupModelBase Create(LODisasterSequenceUnit disasterSequenceUnit)
		{
			return new LODisasterGroupModelBase(disasterSequenceUnit);
		}

		private LODisasterGroupMeta m_Meta;
		private float m_OccurrenceTime;
		public float OccurrenceTime { get => m_OccurrenceTime; }
		public List<ILODisasterModelBase> DisasterList { get; private set; }

		protected LODisasterGroupModelBase(LODisasterSequenceUnit disasterSequenceUnit)
		{
			m_Meta = disasterSequenceUnit.DisasterGroup;
			m_OccurrenceTime = disasterSequenceUnit.OccurrenceTime;
			DisasterList = new List<ILODisasterModelBase>();
			foreach (var disasterMeta in m_Meta.DisasterList)
			{
				DisasterList.Add(LODisasterModelBase.Create(disasterMeta));
			}
		}
	}
}