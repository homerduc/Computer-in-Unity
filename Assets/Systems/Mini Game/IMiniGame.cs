using UnityEngine;

public interface IMiniGame
{
    RenderTexture RenderTextureTarget { get; } // Where the game is rendered
    string GameName { get; }
    void Initialize();
    void Update();
    void Cleanup();
    void OnInteraction(string message); // Interact with other games/components
}
