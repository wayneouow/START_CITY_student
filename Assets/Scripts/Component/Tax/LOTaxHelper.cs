using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Model;
using LO.Meta;

namespace LO.Helper
{
    public interface ILOTaxHelperDelegate
    {
        void TaxOccur(ILOTaxModel tax);
    }

    public interface ILOTaxHelper
    {
        void CheckNextTaxOccur(float gameTime);
        void Reset();
    }

    public class LOTaxHelper : ILOTaxHelper
    {
        public static LOTaxHelper Create(
            LOTaxSequenceMeta taxSequenceMeta,
            ILOTaxHelperDelegate helperDelegate
        )
        {
            return new LOTaxHelper(taxSequenceMeta, helperDelegate);
        }

        ILOTaxHelperDelegate m_TaxHelperDelegate;
        List<KeyValuePair<float, ILOTaxModel>> m_TaxSequence;

        private int m_NextIndex = 0;

        private LOTaxHelper(LOTaxSequenceMeta taxSequenceMeta, ILOTaxHelperDelegate helperDelegate)
        {
            m_TaxSequence = new List<KeyValuePair<float, ILOTaxModel>>();
            foreach (var taxMeta in taxSequenceMeta.TaxList)
            {
                var taxModel = LOTaxModel.Create(taxMeta);
                m_TaxSequence.Add(
                    new KeyValuePair<float, ILOTaxModel>(taxModel.OccurrenceTime, taxModel)
                );
            }
            m_TaxSequence.Sort((x, y) => x.Key.CompareTo(y.Key));

            m_TaxHelperDelegate = helperDelegate;
        }

        public void Reset()
        {
            m_NextIndex = 0;
        }

        public void CheckNextTaxOccur(float gameTime)
        {
            if (m_NextIndex >= m_TaxSequence.Count)
            {
                return;
            }
            if (gameTime >= m_TaxSequence[m_NextIndex].Value.OccurrenceTime)
            {
                m_TaxHelperDelegate.TaxOccur(m_TaxSequence[m_NextIndex].Value);
                m_NextIndex += 1;
            }
        }
    }
}
