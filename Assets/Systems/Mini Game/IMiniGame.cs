using UnityEngine;

public interface IMiniGame
{
    string GameName { get; }
    RenderTexture RenderTarget { get; } // Where the game is rendered
    void Initialize();
    void Update();
    void OnInteraction(string action); // Interact with other games/components
}
