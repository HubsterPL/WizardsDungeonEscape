using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool pause = true;
    public bool ongoingGame = false;
    #region singleton
    public static GameManager Instance {
        get {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                    Debug.LogError("There is no GameManager!");
            }
            return _instance;
        }
    }
    private static GameManager _instance;
    #endregion
    public GameSettings Settings {
        get {
            return settings;
        }
    }
    [SerializeField] GameSettings settings;

    [SerializeField] RectTransform victoryScreen;
    [SerializeField] RectTransform defeatScreen;
    [SerializeField] RectTransform guiScreen;
    [SerializeField] TMPro.TMP_Text newHighscore;
    [SerializeField] RectTransform levelSelectionScreen;
    [SerializeField] RectTransform pauseMessage;

    internal void ManaRegenCooldown()
    {
        manaRechargeTickAccumulation = -2f;
    }

    [SerializeField] LevelData currentLevel;
    [SerializeField] PlayerHealthBar hpBar;
    [SerializeField] Player player;

    const float manaRechargeTickDuration = .5f;
    float manaRechargeTickAccumulation = 0f;
    const float overflowDecayTickDuration = .25f;
    float overflowDecayTickAccumulation = 0f;

    public int PickupCountOnLevel = 0;
    public int PickupCount = 0;
    public int EnemyCountOnLevel = 0;
    public int EnemyCount = 0;


#region functions Unity
    public void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            if (currentLevel != null)
            {
                Game.Score.SetMana(currentLevel.manaLimit);
            }
            
        }
    }

    private void Update()
    {
        if (ongoingGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPause(!pause);
                PauseMenu(pause);
            }

        }
        if (!pause)
        {
            Game.Score.AddTime(Time.deltaTime);

            manaRechargeTickAccumulation += Time.deltaTime;
            overflowDecayTickAccumulation += Time.deltaTime;

            if (manaRechargeTickAccumulation > manaRechargeTickDuration) 
            {
                manaRechargeTickAccumulation -= manaRechargeTickDuration;
                if (Game.Score.GetMana() < Game.Score.MaxMana)
                    Game.Score.AddMana(1);
            }

            if (overflowDecayTickAccumulation > overflowDecayTickDuration)
            {
                overflowDecayTickAccumulation -= overflowDecayTickDuration;
                if (Game.Score.GetMana() > Game.Score.MaxMana)
                    Game.Score.AddMana(-1);
            }

        }
    }
#endregion
    void DeactivateCanvases()
    {
        victoryScreen.gameObject.SetActive(false);
        defeatScreen.gameObject.SetActive(false);
        levelSelectionScreen.gameObject.SetActive(false);
        guiScreen.gameObject.SetActive(false);
    }
    public void LevelSelection()
    {
        DeactivateCanvases();
        levelSelectionScreen.gameObject.SetActive(true);
    }
    public void Victory()
    {
        DeactivateCanvases();
        bool newHigh = TrySetHighscore(currentLevel, Game.Score.GetScore());

        if (newHigh)
            newHighscore.text = "NEW! Highscore: "+GetHighscore(currentLevel).ToString();
        else
            newHighscore.text = "Highscore: " + GetHighscore(currentLevel).ToString();

        newHighscore.gameObject.SetActive(true);
        victoryScreen.gameObject.SetActive(true);
        pause = true;
        ongoingGame = false;
    }
    public void Defeat()
    {
        DeactivateCanvases();
        defeatScreen.gameObject.SetActive(true);
        pause = true;
        ongoingGame = false;
    }
    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }
    public void LoadLevel(LevelData level)
    {
        PickupCountOnLevel = 0;
        PickupCount = 0;
        EnemyCountOnLevel = 0;
        EnemyCount = 0;

        SetPause(false);
        DeactivateCanvases();
        guiScreen.gameObject.SetActive(true);
        currentLevel = level;
        SceneManager.LoadScene(currentLevel.levelSceneAssetName);
        Game.Score.SetMana(currentLevel.manaLimit);
        Game.Score.MaxMana = currentLevel.manaLimit;
        Game.Score.SetTimeScorePool(currentLevel.timeReward);
        Game.Score.SetEnemies(0);
        Game.Score.SetPickup(0);
        Game.Score.ResetTime();

        ongoingGame = true;
    }
    
    public void SetPause(bool val)
    {
        pause = val;
        if (val)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void PauseMenu(bool val)
    {
        levelSelectionScreen.gameObject.SetActive(val);
        pauseMessage.gameObject.SetActive(val);
    }

    public void RegisterPlayer(Player player)
    {
        this.player = player;
        hpBar.SetSource(player.GetDamageController());
    }
    public Player GetPlayer()
    {
        return player;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// Records highscore if given score is greater than given level's highscore.
    /// Returns true if new highscore is set.
    /// </summary>
    /// <returns></returns>
    public bool TrySetHighscore(LevelData level, int score)
    {
        string key = level.levelName;
        if (PlayerPrefs.HasKey(key))
        {
            int levelHighscore = PlayerPrefs.GetInt(key);
            if (levelHighscore > score)
                return false;
        }
        PlayerPrefs.SetInt(key, score);
        return true;
    }

    public int GetHighscore(LevelData level)
    {
        if (PlayerPrefs.HasKey(level.levelName))
        {
            return PlayerPrefs.GetInt(level.levelName);
        }
        return 0;
    }
}
