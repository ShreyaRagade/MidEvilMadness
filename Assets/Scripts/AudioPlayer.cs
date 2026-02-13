using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip soundEffectClip; // Public variable to drag your AudioClip into the Inspector

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // Always good practice to check
        {
            Debug.LogError("AudioSource component not found on this GameObject!");
        }
    }

    // Call this function when you want to play the sound
    public void PlaySoundEffect()
    {
        // Option 1: To play a single, non-looping sound effect without interrupting others
        if (soundEffectClip != null)
        {
            audioSource.PlayOneShot(soundEffectClip);
        }
        // Option 2: To play a sound using the default clip assigned in the Inspector, or to play looping music
        // audioSource.Play();
    }
}
