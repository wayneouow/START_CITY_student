using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LO.Meta;

namespace LO
{
    public class LOAudio : MonoBehaviour
    {
        public static LOAudio Instance;

        public LOAudioListMeta AudioListMeta;
        public Dictionary<string, AudioClip> AudioDic = new Dictionary<string, AudioClip>();
        public Transform AudioRoot;
        public GameObject AudioPrefab;

        List<LOAudioObj> m_WaitObjList = new List<LOAudioObj>();

        private void Awake()
        {
            Instance = this;

            foreach (var audio in AudioListMeta.AudioList)
            {
                AudioDic.Add(audio.name, audio);
            }
        }

        public void PutBack(LOAudioObj audioObj)
        {
            m_WaitObjList.Add(audioObj);
        }

        void CreateNewAudioObj()
        {
            var newAudioObj = GameObject
                .Instantiate(AudioPrefab, AudioRoot)
                .GetComponent<LOAudioObj>();
            PutBack(newAudioObj);
        }

        public float PlaySoundEffect(string audioName)
        {
            if (!AudioDic.ContainsKey(audioName))
            {
                return 0;
            }

            var audioClip = AudioDic[audioName];

            if (m_WaitObjList.Count == 0)
            {
                CreateNewAudioObj();
            }

            var audioObj = m_WaitObjList[0];
            m_WaitObjList.RemoveAt(0);
            audioObj.Play(audioClip);

            return audioClip.length;
        }

        public float PlayBtnActive()
        {
            return PlaySoundEffect("btn-active");
        }

        public float PlayBtnBack()
        {
            return PlaySoundEffect("btn-back");
        }

        public float PlayGameDisasterStart()
        {
            return PlaySoundEffect("game-disaster-start");
        }

        public float PlayGameStart()
        {
            return PlaySoundEffect("game-start");
        }

        public float PlayGameTax()
        {
            return PlaySoundEffect("game-tax");
        }

        public float PlayTabSwitch()
        {
            return PlaySoundEffect("tab-switch");
        }

        public float PlayGameCardClick()
        {
            return PlaySoundEffect("game-card-click");
        }
    }
}
