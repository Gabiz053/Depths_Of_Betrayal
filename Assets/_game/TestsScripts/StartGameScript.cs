using System;
using UnityEngine;

[Serializable]
public class StartGameScript : StartGame
{
    public GameObject suelo; // Make suelo a public parameter
    private Vector3 initialPosition;

    void Start()
    {
        // Avisa a los demas
    }

    public override void GameStart()
    {
        suelo.SetActive(false);
    }
}
