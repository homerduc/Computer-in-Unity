using UnityEngine;

public class testminigamescript : MiniGameBase
{
    public override string GameName => "TestGame";

    public override void Initialize()
    {
        base.Initialize();
        // Ajoutez quelques cubes color�s pour tester
    }

    public override void Update()
    {
        // Faites tourner quelque chose pour voir que �a marche
    }

    public override void OnInteraction(string message)
    {

    }
}
