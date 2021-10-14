using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI nameInput;
    public TextMeshProUGUI highScoreText;
    private string playerName;
    private static MenuManager instance;
    private string filePath;

    private Score highScore;

    public string PlayerName { get => playerName; set => playerName = value; }
    public Score HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            SaveHighScore();
        }
    }

    public void Awake()
    {
        filePath = Application.persistentDataPath + "/highScore.json";
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        LoadHighScore();
        highScoreText.text = GetHighScoreText();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        Debug.Log(PlayerName);
    }

    public void Exit()
    {
        SaveHighScore();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public string GetHighScoreText()
    {
        return "Best score : " + HighScore.name + " : " + HighScore.points;
    }

    private void SaveHighScore()
    {
        string json = JsonUtility.ToJson(HighScore);
        File.WriteAllText(filePath, json);
    }

    private void LoadHighScore()
    {
        
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            HighScore = JsonUtility.FromJson<Score>(json);
        }
        else
        {
            highScore = new Score("None", 0);
        }
    }

    [System.Serializable]
    public class Score
    {
        public string name;
        public int points;

        public Score(string name, int score)
        {
            this.name = name;
            this.points = score;
        }
    }
}
