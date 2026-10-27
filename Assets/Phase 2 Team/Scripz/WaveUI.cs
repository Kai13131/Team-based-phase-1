
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [Header("Wave Manager")]
    public EnemySpawnerV2 waveManager;

    [Header("UI Text")]
    public TMP_Text waveText;
    public TMP_Text statusText;
    public TMP_Text countdownText;
    public TMP_Text enemiesRemainingText;
    public TMP_Text inputPromptText;

    private void Update()
    {
        if (waveManager == null)
            return;

        UpdateWaveUI();
    }

    private void UpdateWaveUI()
    {
        // Display the current wave number.
        waveText.text = "Wave " + waveManager.CurrentWaveNumber + " / " + waveManager.TotalWaves;

        enemiesRemainingText.text = "Enemies Remaining: " + waveManager.RemainingEnemies;

        switch (waveManager.CurrentState)
        {
            case EnemySpawnerV2.WaveState.Preparing:

                statusText.text = "Preparing";

                countdownText.text =
                    "Next Wave: " + Mathf.CeilToInt(Mathf.Max(0f, waveManager.Countdown)) + "s";

                inputPromptText.text =
                    "Press ENTER to Start Wave";

                break;

            case EnemySpawnerV2.WaveState.Spawning:

                statusText.text = "Wave Starting!";
                countdownText.text = "";
                inputPromptText.text = "";

                break;

            case EnemySpawnerV2.WaveState.Active:

                statusText.text = "Wave In Progress";
                countdownText.text = "";
                inputPromptText.text = "";

                break;

            case EnemySpawnerV2.WaveState.Completed:

                statusText.text = "Wave Complete!";
                countdownText.text = "";
                inputPromptText.text = "";

                break;

            case EnemySpawnerV2.WaveState.Finished:

                statusText.text = "All Waves Complete!";
                countdownText.text = "";
                inputPromptText.text = "";

                break;
        }
    }
}
