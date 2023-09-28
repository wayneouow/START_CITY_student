using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Event;
using LO.Meta;
using LO.View;

namespace LO.Model
{
	public interface ILOFunctionalModelBase
	{
		/// <summary>
		/// returns a boolean value indicating whether the effect execution was successful or not.
		/// </summary>
		bool DoEffect(ILODemoSceneAreaView effectAreaView);
	}

	public class LOFunctionalModelBase : ILOFunctionalModelBase
	{
		public LOFunctionalCardMeta CardMeta;

		// TODO : refactor add functional model
		public static LOFunctionalModelBase CreateModel(LOFunctionalCardMeta cardMeta)
		{
			switch (cardMeta.FunctionCardType)
			{
				case FunctionCardType.Demolish:
					return LODemolishModel.Create(cardMeta);
				case FunctionCardType.Repair:
					return LORepairModel.Create(cardMeta);
				case FunctionCardType.Damper:
					return LODamperModel.Create(cardMeta);
				default:
					return new LOFunctionalModelBase(cardMeta);
			}
		}

		protected LOFunctionalModelBase(LOFunctionalCardMeta cardMeta)
		{
			CardMeta = cardMeta;
		}

		public virtual bool DoEffect(ILODemoSceneAreaView effectAreaView)
		{
			return true;
		}
	}
}
