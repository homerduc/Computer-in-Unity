using UnityEngine;

public class testminigamescript : MiniGameBase
{
    public override string GameName => "TestGame";

    public override void Initialize()
    {
        base.Initialize();
        // Ajoutez quelques cubes colorés pour tester
    }

    public override void Update()
    {
        // Faites tourner quelque chose pour voir que ça marche
    }

    public override void OnInteraction(string message)
    {

    }
}
