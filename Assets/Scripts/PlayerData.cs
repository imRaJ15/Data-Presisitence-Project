using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public List<Player> players;

    public PlayerData(List<Player> players)
    {
        this.players = players;
    }
}
