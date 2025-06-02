using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] public AudioMixer AudioMixer;

    [SerializeField] PlayerEntity playerEntity;
    [SerializeField] OpponentEntity opponentEntity;

    [SerializeField] float maxVolume;
    [SerializeField] float minVolume;

    [SerializeField] float transitionSpeed;

    private float currentNikaVolume;
    private float currentEnemyVolume;
    private float currentTrainingVolume;
    private float targetNikaVolume;
    private float targetEnemyVolume;
    private float targetTrainingVolume;

    private bool trainingMode = true;

    void Start()
    {
        AudioMixer.GetFloat("NikaVolume", out currentNikaVolume);
        AudioMixer.GetFloat("EnemyVolume", out currentEnemyVolume);
        AudioMixer.GetFloat("TrainingVolume", out currentTrainingVolume);

        targetNikaVolume = currentNikaVolume;
        targetEnemyVolume = currentEnemyVolume;
        targetTrainingVolume = minVolume;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            trainingMode = !trainingMode;
        }

        if (trainingMode)
        {
            targetTrainingVolume = maxVolume;
            targetNikaVolume = minVolume;
            targetEnemyVolume = minVolume;
        }
        else
        {
            targetTrainingVolume = minVolume;
            FightTrack();
        }

        currentNikaVolume = Mathf.Lerp(currentNikaVolume, targetNikaVolume, Time.deltaTime * transitionSpeed);
        currentEnemyVolume = Mathf.Lerp(currentEnemyVolume, targetEnemyVolume, Time.deltaTime * transitionSpeed);
        currentTrainingVolume = Mathf.Lerp(currentTrainingVolume, targetTrainingVolume, Time.deltaTime * transitionSpeed);

        AudioMixer.SetFloat("NikaVolume", currentNikaVolume);
        AudioMixer.SetFloat("EnemyVolume", currentEnemyVolume);
        AudioMixer.SetFloat("TrainingVolume", currentTrainingVolume);
    }

    private void FightTrack()
    {
        float playerRatio = playerEntity.health / playerEntity.maxHP;
        float opponentRatio = opponentEntity.health / opponentEntity.maxHP;

        if (playerRatio >= opponentRatio)
        {
            // Player is winning
            targetNikaVolume = maxVolume;
            targetEnemyVolume = minVolume;
        }
        else
        {
            // Player is losing
            targetNikaVolume = minVolume;
            targetEnemyVolume = maxVolume;
        }
    }
}
