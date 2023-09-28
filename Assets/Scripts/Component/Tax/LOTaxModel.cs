using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Meta;

namespace LO.Model
{
    public interface ILOTaxModel
    {
        float OccurrenceTime { get; }
        float TaxAmount { get; }
    }

    public class LOTaxModel : ILOTaxModel
    {
        public static ILOTaxModel Create(LOTaxMeta meta)
        {
            return new LOTaxModel(meta);
        }

        LOTaxMeta m_Meta;
        public float OccurrenceTime
        {
            get => m_Meta.OccurrenceTime;
        }
        public float TaxAmount
        {
            get => m_Meta.TaxAmount;
        }

        private LOTaxModel(LOTaxMeta meta)
        {
            m_Meta = meta;
        }
    }
}
