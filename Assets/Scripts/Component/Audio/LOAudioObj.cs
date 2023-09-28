using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LO
{
    public class LOAudioObj : MonoBehaviour
    {
        [SerializeField]
        AudioSource m_AudioSource;

        public void Play(AudioClip audioClip)
        {
            m_AudioSource.clip = audioClip;
            m_AudioSource.Play();

            StartCoroutine(WaitPutBack(audioClip.length));
        }

        IEnumerator WaitPutBack(float time)
        {
            yield return new WaitForSeconds(time);
            m_AudioSource.Stop();
            LOAudio.Instance.PutBack(this);
        }
    }
}
