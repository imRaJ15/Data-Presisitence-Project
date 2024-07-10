using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    public TMP_Text scoreListText;

    public string _currentPlayerName;

    private List<Player> players = new List<Player>();

    private string saveFilePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Path.Combine(Application.persistentDataPath, "path/to/PlayerData.json");
            Debug.Log("Save file path: " + saveFilePath);
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
        players.Add(new Player(name, initialScore));
        Debug.Log("Added player: " + name + " with score: " + initialScore);
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
        string json = JsonUtility.ToJson(playerData);
        Debug.Log("Saving data: " + json); // Debug log for saving data
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            Debug.Log("Loading data: " + json);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            if (playerData != null && playerData.players != null)
            {
                players = playerData.players;
                Debug.Log("Loaded players count: " + players.Count);
            }
            else
            {
                Debug.LogWarning("Player data or player list is null");
                players = new List<Player>();
            }
        }
        else
        {
            Debug.LogWarning("Save file not found, initializing new player list");
            players = new List<Player>();
        }
    }
}