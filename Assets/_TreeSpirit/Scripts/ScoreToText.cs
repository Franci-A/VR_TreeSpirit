using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreToText : MonoBehaviour
{
    [SerializeField] Text scoreText;
    private void Update()
    {
        scoreText.text = PlayerManager.Instance.Wood.ToString();
    }
}
