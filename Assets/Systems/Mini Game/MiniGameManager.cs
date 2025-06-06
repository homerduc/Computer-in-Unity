using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public Dictionary<string, IMiniGame> miniGames = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterMiniGame(string id, IMiniGame game)
    {
        miniGames[id] = game;
    }
}
