using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordScript : MonoBehaviour
{
    [SerializeField] private Text _gameOverScoreText;

    private InterfaceScript inter;
    private int score;
    private int record;

    void Awake()
    {
        inter = FindObjectOfType<InterfaceScript>();
        record = PlayerPrefs.GetInt("Record");
    }

    /// <summary>
    /// Реализация выставленя рекорда
    /// </summary>
    public void Record()
    {
        score = inter.ScoreNumber();
        if (score <= record)
        {
            _gameOverScoreText.text = $"Score: {score}";
        }
        else if (score > record)
        {
            record = score;
            PlayerPrefs.SetInt("Record", record);
            _gameOverScoreText.text = $"New High Score: {record}";
        }
    }
}
