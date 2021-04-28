using System.Collections;
using UnityEngine;

namespace Ch11.Scripts
{
    [RequireComponent(typeof(AudioSource), typeof(ParticleSystem))]
    public class LightSparkling : MonoBehaviour
    {
        public uint minSparkles = 3;
        public uint maxSparkles = 10;
        public float minShortItnerval = 0.05f;
        public float maxShortInterval = 0.2f;
        public float minLongItnerval = 2f;
        public float maxLongInterval = 10f;

        
        private AudioSource _sparkSound;
        private ParticleSystem _sparkParticles;

        private void Start()
        {
            _sparkSound = GetComponent<AudioSource>();
            _sparkParticles = GetComponent<ParticleSystem>();

            StartCoroutine(Sparking());
        }

        private IEnumerator Sparking()
        {
            while (true)
            {
                for (var i = 0; i < Random.Range(minSparkles, maxSparkles); i++)
                {
                    _sparkParticles.Play();
                    _sparkSound.PlayOneShot(_sparkSound.clip);
                    yield return new WaitForSeconds(Random.Range(minShortItnerval, maxShortInterval));
                }
                yield return new WaitForSeconds(Random.Range(minLongItnerval, maxLongInterval));
            }
        }
    }
}
