using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI nameInput;
    private string playerName;
    private static MenuManager instance;

    public Score highScore = new Score("", 0);

    public string PlayerName { get => playerName; set => playerName = value; }

    public void Awake()
    {
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

    public void StartNew()
    {
        SceneManager.LoadScene(1);
        Debug.Log(PlayerName);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
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
