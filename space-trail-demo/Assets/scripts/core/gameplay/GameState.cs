using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameState 
{
    public string currentLevelReference;
    public player playerReference = null;
    public static GameState instance { get; private set; }
    public LEVELS levelState = new LEVELS();
    private bool ready = false;
    public string LAST_LEVEL = "";
    public string STORY_LINE_CHOSEN = "";

    public GameState()
    {
        Debug.unityLogger.Log("GameState Instantiated");
    }

    public static GameState getGameState()
    {
        if (GameState.instance == null)
        {
            GameState.instance = new GameState();
        }
        return GameState.instance;
    }

    public static void setGameState(GameState nState)
    {
        GameState.instance = nState;
    }

    public bool gameStateReady()
    {
        return this.ready;
    }

    public void setReady()
    {
        this.ready = true;
    }

    public void notReady()
    {
        this.ready = false;
    }

    [Serializable]
    public class LevelState
    {
        public bool completed { get; set; } = false;
        public Dictionary<string, bool> eventToggles; 
    }

    [Serializable]
    public class LEVELS
    {
        public LevelState LECTURE_HALL = new LevelState();
        public LevelState HALLWAY;
        public LevelState TA_OFFICE;

        public LEVELS()
        {
            this.initTAOfficeState();
            this.initHallwayState();
        }

        private void initTAOfficeState()
        {
            this.TA_OFFICE = new LevelState();
            this.TA_OFFICE.eventToggles = new Dictionary<string, bool> {
                {"gotLabKit", false }
            };
        }

        private void initHallwayState()
        {
            this.HALLWAY = new LevelState();
            this.HALLWAY.eventToggles = new Dictionary<string, bool>
            {
                {"firstTimeLoad",true}
            };
        }
    }
}
