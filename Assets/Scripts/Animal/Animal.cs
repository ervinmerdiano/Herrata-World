using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Animal : MonoBehaviour
{
    [Header("Animal Settings")]
    public string animalSoundName = "MOO";
    public AudioSource soundSource;

    [HideInInspector]
    public bool canPlaySound = true; // flag supaya gak replay duluan

    private void Awake()
    {
        if (soundSource == null)
        {
            soundSource = GetComponent<AudioSource>();
            if (soundSource == null)
                soundSource = GetComponentInChildren<AudioSource>();
        }
    }

    public void PlaySound()
    {
        if (soundSource == null || !canPlaySound) return;

        canPlaySound = false;
        soundSource.Play();
        Debug.Log(animalSoundName);

        // reset flag setelah suara selesai
        StartCoroutine(ResetSoundFlag());
    }

    private System.Collections.IEnumerator ResetSoundFlag()
    {
        yield return new WaitForSeconds(soundSource.clip.length);
        canPlaySound = true;
    }
}
