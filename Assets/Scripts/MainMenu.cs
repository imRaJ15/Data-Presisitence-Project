using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    public TMP_Text scoreListText;

    public string _currentPlayerName;

    public List<Player> players = new List<Player>();

    private string saveFilePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadPlayerData();
        SortPlayersByScore();
        DisplayScores();
    }

    void OnApplicationQuit()
    {
        SavePlayerData();
    }

    public void AddPlayer(string name, int initialScore)
    {
        Player existingPlayer = players.Find(p => p.Name == name);
        if (existingPlayer != null)
        {
            existingPlayer.Score = initialScore;
        }
        else
        {
            players.Add(new Player(name, initialScore));
        }
        SortPlayersByScore();
        DisplayScores();
    }

    public void SortPlayersByScore()
    {
        if (players == null) return;
        players.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));
    }

    public void UpdateScore(string name, int newScore)
    {
        Player player = players.Find(p => p.Name == name);
        if (player != null)
        {
            player.Score = newScore;
            SortPlayersByScore();
            DisplayScores();
        }
        else
        {
            Debug.LogWarning("Player not found: " + name);
        }
    }

    public void DisplayScores()
    {
        if (scoreListText == null || players == null) return;

        scoreListText.text = "";

        foreach (Player player in players)
        {
            scoreListText.text += player.Name + ": " + player.Score + "\n";
        }
    }

    public void AssignScoreListText(TMP_Text textComponent)
    {
        scoreListText = textComponent;
        DisplayScores();
    }

    public void SavePlayerData()
    {
        if (players == null) return;

        PlayerData playerData = new PlayerData(players);
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            if (playerData != null && playerData.players != null)
            {
                players = playerData.players;
            }
            else
            {
                players = new List<Player>();
            }
        }
        else
        {
            players = new List<Player>();
        }
    }
}