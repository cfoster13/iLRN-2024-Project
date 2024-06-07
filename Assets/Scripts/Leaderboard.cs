using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    private List<PlayerScore> leaderboard = new List<PlayerScore>();
    private string filePath;

    public GameObject scoreLoader;
    public GameObject leaderboardUI;
    public InputField input;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/leaderboard.txt";
        LoadLeaderboard();
    }

    public void AddScore(string playerName, int score)
    {
        leaderboard.Add(new PlayerScore(playerName, score));
        leaderboard.Sort((x, y) => y.score.CompareTo(x.score));
        SaveLeaderboard();
    }

    private void SaveLeaderboard()
    {
        //Sorting out positons
        var newLeaderboard = leaderboard.OrderBy(player => player.score).ToList();
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (PlayerScore playerScore in leaderboard)
            {
                writer.WriteLine(playerScore.playerName + "," + playerScore.score);
            }
        }
    }

    private void LoadLeaderboard()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                string playerName = parts[0];
                int score = int.Parse(parts[1]);
                leaderboard.Add(new PlayerScore(playerName, score));
            }
            leaderboard.Sort((x, y) => y.score.CompareTo(x.score));
        }
    }

    public void EnterScore()
    {
        string playerName = input.text;
        input.gameObject.SetActive(false);
        leaderboardUI.gameObject.SetActive(true);

        AddScore(playerName, scoreLoader.GetComponent<ScoreSaver>().points);
    }

    public List<PlayerScore> GetLeaderboard()
    {
        return leaderboard;
    }
}

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int score;

    public PlayerScore(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}
