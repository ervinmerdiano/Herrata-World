using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource fantasy;
    public AudioSource ocean;

    [Range(0f, 1f)] public float bgVolume = 0.2f;

    void Start()
    {
        fantasy.volume = bgVolume;
        ocean.volume = bgVolume;
        fantasy.loop = true;
        ocean.loop = true;
        fantasy.Play();
        ocean.Play();
    }

    void Update()
    {
        fantasy.volume = bgVolume;
        ocean.volume = bgVolume;
    }
}
