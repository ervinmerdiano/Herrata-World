using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstep : MonoBehaviour
{
    private AudioSource footstepSource;
    private PlayerMovement player;
    private PlayerHealth playerHealth;

    [Header("Footstep Settings")]
    [Range(0f, 1f)] public float volume = 0.3f;
    [Range(1f, 2f)] public float runPitchMultiplier = 1.05f; // seberapa cepat langkah saat lari
    private float basePitch = 0.95f;

    void Start()
    {
        // Ambil audio source & script player
        footstepSource = GetComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();
        player = GetComponent<PlayerMovement>();

        // Pastikan loop dan tidak main dari awal
        footstepSource.loop = true;
        footstepSource.playOnAwake = false;
        footstepSource.volume = volume;
        footstepSource.pitch = basePitch;
    }

    void Update()
    {
        if (player == null) return;

        // Jika player sudah mati, hentikan footstep dan jangan coba Play()
        if (playerHealth != null && playerHealth.IsDead)
        {
            if (footstepSource.isPlaying)
                footstepSource.Stop();
            return;
        }

        bool isMoving = player.IsMoving();
        bool isRunning = player.IsRunning();

        // Ubah kecepatan playback sesuai kondisi
        float targetPitch = isRunning ? runPitchMultiplier : basePitch;
        footstepSource.pitch = targetPitch;

        bool audioEnabled = footstepSource != null && footstepSource.enabled;
        // Mainkan atau hentikan audio sesuai gerakan
        if (isMoving)
        {
            if (!footstepSource.isPlaying)
                footstepSource.Play();
        }
        else
        {
            if (footstepSource.isPlaying)
                footstepSource.Pause();
        }
    }
}
