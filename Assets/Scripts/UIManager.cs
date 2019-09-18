using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int ballHit;
    Jumper jumper;

    void Start()
    {
        ballHit = PlayerPrefs.GetInt("ballHit");
        jumper = FindObjectOfType<Jumper>();
        OnPause();
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        UpdateHitLabel();
    }

    public void UpdateHitLabel()
    {
        Transform labelTransform = gameObject.transform.GetChild(1);
        if (labelTransform)
        {
            Text labetText = labelTransform.gameObject.GetComponent<Text>();
            if (labetText)
            {
                labetText.text = "Ball Hit: " + ballHit;
            }

        }
    }

    public void IncBallHit()
    {
        ballHit++;
        PlayerPrefs.SetInt("ballHit", ballHit);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public static void ChangePlatformColor(GameObject platform)
    {
        SpriteRenderer spriteRenderer = platform.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            float red = Random.Range(0, 101) / 100f;
            float green = Random.Range(0, 101) / 100f;
            float blue = Random.Range(0, 101) / 100f;
            spriteRenderer.color = new Color(red, green, blue);
        }
    }

    public void ChangePlanet(Planet planet)
    {
        Camera.main.backgroundColor = planet.skyColor;
        Time.timeScale = 1;
        jumper.OnChangeGravity(planet.gravity);
    }
}
