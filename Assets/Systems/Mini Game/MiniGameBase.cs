using UnityEngine;

// Common methods, attributes, and shared code for all mini-games.
// Unlike the IMiniGame interface, this abstract class can provide default implementations and shared logic.
public abstract class MiniGameBase : MonoBehaviour, IMiniGame
{
    [SerializeField] protected Camera gameCamera;
    [SerializeField] protected RenderTexture renderTexture;

    public abstract string GameName { get; }

    // By default uses the RenderTexture defined in the editor, but can be overriden
    public virtual RenderTexture RenderTextureTarget => renderTexture;
    public bool IsInitialized { get; protected set; }
    public bool IsActive { get; set; } = true;

    public virtual void Initialize()
    {
        SetupRenderTexture();
        IsInitialized = true;
    }

    protected virtual void SetupRenderTexture()
    {
        if (renderTexture == null && gameCamera != null)
        {
            renderTexture = new RenderTexture(854, 480, 24); // 480p
        }
        gameCamera.targetTexture = renderTexture;
    }

    public virtual void Cleanup()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }

    public abstract void Update();
    public abstract void OnInteraction(string message);

}
