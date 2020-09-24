using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    public int maxLives = 3;

    private int _currentLife;

    public Text livesText;

    private void Start()
    {
        _currentLife = maxLives;
        livesText.text = _currentLife.ToString();
    }

    public void PlayerKilled()
    {
        _currentLife--;
        if (_currentLife == 0)
        {
            GameController.Instance.GameOver();
        }
        else
        {
            //Player.instance.Respawn();
            StartCoroutine(DoSpawnAfterDelay());
            livesText.text = _currentLife.ToString();
        }
    }

    IEnumerator DoSpawnAfterDelay()
    {
        Player.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        Player.instance.Respawn();
        Player.instance.gameObject.SetActive(true);
    }
}
