using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoonManager : MonoBehaviour
{
    private State state;
    private State gameOverState = new State()
    {
        FallSpeed = 0,
        FuelConsumption = 0,
        MovementSpeed = 0,
        ScoreIncrement = 0,
        PreventSpawn = true,
    };
    public State State;
    bool gameOver = false;

    public Movement player;
    public bool GodMode = false;

    private List<Modifier> Modifiers { get; set; } = new List<Modifier>();
    private List<Modifier> ForRemoval { get; set; } = new List<Modifier>();

    // Start is called before the first frame update
    void Start()
    {
        State = new State();
        AddModifier(s =>
        {
            s.FallSpeed += Time.time / 2;
            Debug.Log(Time.time);
        }, -1);
    }

    public void AddModifier(Action<State> modifier, float activeInSec)
    {
        Modifiers.Add(new Modifier()
        {
            Action = modifier,
            EndTime = activeInSec < 0 ? -1 : Time.realtimeSinceStartup + activeInSec
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("MainGameScene");
        }

        if (gameOver)
        {
            State = gameOverState;
            return;
        }
        state = new State();
        foreach (var m in Modifiers.ToArray())
        {
            m.Action(state);
            if (m.EndTime > 0 && m.EndTime < Time.realtimeSinceStartup)
                ForRemoval.Add(m);
        }

        foreach (var m in ForRemoval)
        {
            Modifiers.Remove(m);
        }
        ForRemoval.Clear();
        State = state;

    }

    public void GameOver()
    {
        if (GodMode)
            return;
        gameOver = true;
        Destroy(player.gameObject);
    }
}

public class Modifier
{
    public Action<State> Action { get; set; }
    public float EndTime { get; set; }
}

public class State
{
    public int ScoreIncrement = 10;
    public float FallSpeed = 10;
    public int FuelConsumption = 10;
    public int MovementSpeed = 20;
    public bool PreventSpawn = false;
}
