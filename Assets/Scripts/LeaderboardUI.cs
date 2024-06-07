using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class LeaderboardUI : MonoBehaviour
{
    public Text leaderboardText;
    public Leaderboard leaderboardManager;

    private void Start()
    {
        leaderboardManager = FindFirstObjectByType<Leaderboard>();
        UpdateLeaderboardUI();
    }

    public void UpdateLeaderboardUI()
    {
        StringBuilder sb = new StringBuilder();
        List<PlayerScore> leaderboard = leaderboardManager.GetLeaderboard();

        int counter = 1;
        foreach (PlayerScore playerScore in leaderboard)
        {
            sb.AppendLine(counter + ")" + playerScore.playerName + "        -        " + playerScore.score);
            counter++;
        }

        leaderboardText.text = sb.ToString();
    }
}
