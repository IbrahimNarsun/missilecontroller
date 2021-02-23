using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{

    public Text score;
    public Text gameOverText;
    public GameObject gameOverPopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void levelFail()
    {
        gameOverPopup.SetActive(true);
        gameOverText.text = GameSceneManager.Instance.score.ToString();
    }

    public void updateScore()
    {
        score.text = GameSceneManager.Instance.score.ToString();
    }
}
