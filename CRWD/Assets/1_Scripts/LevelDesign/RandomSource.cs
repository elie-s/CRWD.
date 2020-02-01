using UnityEngine;

namespace CRWD
{
    public class RandomSource : MonoBehaviour
    {
        [SerializeField] private AudioSource source = default;
        [SerializeField] private AudioClip[] clips = default;

        public void Play()
        {
            if (!source || clips.Length == 0) return;

            if (source.isPlaying) source.Stop();
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }
}