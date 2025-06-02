using UnityEngine;

public class Punchsoundeffect : MonoBehaviour
{
    [SerializeField] private AudioClip[] punchSounds;
    [SerializeField] private AudioClip[] bonePopSounds;
    [SerializeField] private AudioClip[] KnucklesSounds;


    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayPunchSound()
    {
        if (punchSounds.Length == 0)
        {
            Debug.LogWarning("No sound effects assigned to Punchsoundeffect.");
            return;
        }
        int randomIndex = Random.Range(0, punchSounds.Length);
        audioSource.clip = punchSounds[randomIndex];
        audioSource.Play();
    }

    public void PlayBonePopSound()
    {
        if (bonePopSounds.Length == 0)
        {
            Debug.LogWarning("No sound effects assigned to BonePopSound.");
            return;
        }
        int randomIndex = Random.Range(0, bonePopSounds.Length);
        audioSource.clip = bonePopSounds[randomIndex];
        audioSource.Play();
    }

    public void PlayKnucklesSound()
    {
        if (KnucklesSounds.Length == 0)
        {
            Debug.LogWarning("No sound effects assigned to KnucklesSound.");
            return;
        }
        int randomIndex = Random.Range(0, KnucklesSounds.Length);
        audioSource.clip = KnucklesSounds[randomIndex];
        audioSource.Play();
    }
}
