
using LO.Meta;

namespace LO.Model
{
	public interface ILODisasterModelBase
	{
		int Position { get; }
		float Damage { get; }
	}

	public class LODisasterModelBase : ILODisasterModelBase
	{
		public static ILODisasterModelBase Create(LODisasterMeta meta)
		{
			return new LODisasterModelBase(meta);

		}

		private LODisasterMeta m_Meta;

		public int Position { get => m_Meta.Position; }
		public float Damage { get => m_Meta.Damage; }

		protected LODisasterModelBase(LODisasterMeta meta)
		{

			m_Meta = meta;
		}
	}
}