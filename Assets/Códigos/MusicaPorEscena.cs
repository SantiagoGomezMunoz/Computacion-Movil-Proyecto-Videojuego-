using UnityEngine;

public class MusicaPorEscena : MonoBehaviour
{
    public AudioClip musicaFondo;

    private void Start()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = musicaFondo;
        audio.loop = true;
        audio.playOnAwake = false;
        audio.volume = 0.5f;
        audio.Play();
    }
}