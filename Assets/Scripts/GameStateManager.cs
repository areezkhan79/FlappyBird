using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public enum GameState
{
    Pause,
    Play,
    Over,
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    
    public GameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        currentState = GameState.Pause;
    }

    private void Update()
    {
        Time.timeScale = currentState == GameState.Play ? 1 : 0;
    }
}