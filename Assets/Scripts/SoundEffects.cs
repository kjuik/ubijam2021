using UnityEngine;

namespace Assets.Scripts
{
    public class SoundEffects : MonoBehaviour
    {
        public static SoundEffects Instance;

        private void Awake()
        {
            Instance = this;
        }

        public AudioSource chopSource;

        public void PlayChop()
        {
            chopSource.Play();
        }
    }
}