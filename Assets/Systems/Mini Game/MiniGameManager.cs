using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public Dictionary<string, IMiniGame> activeMiniGames = new();

    void Start()
    {
        // Initialize all minigames
        foreach (var game in activeMiniGames.Values)
        {
            game.Initialize();
        }
    }

    void Update()
    {
        // Update all minigames
        foreach (var game in activeMiniGames.Values)
        {
            game.Update();
        }
    }

    public void RegisterMiniGame(IMiniGame game)
    {
        if (activeMiniGames.ContainsKey(game.GameName))
        {
            Debug.LogWarning($"Game {game.GameName} already registered!");
            return;
        }

        activeMiniGames[game.GameName] = game;
        game.Initialize();
    }

    public void UnregisterMiniGame(string gameName)
    {
        if (activeMiniGames.ContainsKey(gameName))
        {
            activeMiniGames[gameName].Cleanup();
            activeMiniGames.Remove(gameName);
        }
    }
    
    public IMiniGame GetMiniGame(string gameName)
    {
        activeMiniGames.TryGetValue(gameName, out var game);
        return game;
    }
}
