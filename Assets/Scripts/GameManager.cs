using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public GameObject dialogBox;
    public GameObject dialogText;
    public GameObject startButton;
    public GameObject creditsButton;
    public GameObject backButton;
    public GameObject backgroundImage;

    public GameObject canvas;
    public GameObject events;

    private int currentLevel = 0;

    public TextMeshProUGUI menuText;
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI healthText;
    private int health = 1000;
 
    private Coroutine dialogCo;

    private int fishCount;
    private int i;
    private int roundCount = 0;
    private int fishKillCount;
    private bool roundStart = false;

    public GameObject jellyfish;
    public float distance;

    private string title;


    // Start is called before the first frame update
    void Start()
    {
        title = menuText.text;
    }


    // Update is called once per frame
    void Update()
    {


        if ((fishCount <= roundCount) && roundStart){
            roundStart = false;
            NextRound();
            Debug.Log(roundStart);
        }

        if (health <= 0) GameOver();
    }


    void Awake()
	{
        if(Instance == null)
		{
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
             
		}
		else
		{
            Destroy(gameObject);
            Destroy(canvas);
            Destroy(events);
		}
	}

    private void ChangeLevel()
    {
        if (currentLevel == 0)
        {
            startButton.SetActive(false);
            creditsButton.SetActive(false);
            menuText.text = "";
            healthText.text = "Health: " + health;
            StartCoroutine(LoadYourAsyncScene(true, "SampleScene"));
            currentLevel++;
            roundStart = true;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        StartCoroutine(LoadYourAsyncScene(false, "MainMenu"));
        menuText.text = title;
        healthText.text = "";
        health = 3;
        startButton.SetActive(true);
        creditsButton.SetActive(true);
    }

    public void StartButton()
    {
        ChangeLevel();
    }

    public void CreditsButton()
    {
        menuText.text = "Credits";
        startButton.SetActive(false);
        creditsButton.SetActive(false);
        creditsText.gameObject.SetActive(true);
        backButton.SetActive(true);
    }

    public void BackButton()
    {
        menuText.text = title;
        startButton.SetActive(true);
        creditsButton.SetActive(true);
        backButton.SetActive(false);
        creditsText.gameObject.SetActive(false);
    }

    public void StartDialog(string text)
	{
        dialogBox.SetActive(true);
        dialogCo = StartCoroutine(TypeText(text));
	}

    public void HideDialog()
	{
        dialogBox.SetActive(false);
        if (dialogCo != null)
        {
            StopCoroutine(dialogCo);
        }
	}

    IEnumerator TypeText(string text)
	{
        dialogText.GetComponent<TextMeshProUGUI>().text = "";
        foreach(char c in text.ToCharArray())
		{
            dialogText.GetComponent<TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(0.01f);
		}
	}

    IEnumerator ColorLerp(Color endValue, float duration)
	{
        float time = 0;
        Image sprite = backgroundImage.GetComponent<Image>();
        Color startValue = sprite.color;

        while(time < duration)
		{
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
		}
        sprite.color = endValue;
	}

    IEnumerator LoadYourAsyncScene(bool lerp, string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        if (lerp) {StartCoroutine(ColorLerp(new Color(0, 0, 0, 0), 2));}
        else StartCoroutine(ColorLerp(new Color(1, 1, 1, 1), 2)); // reverse
    }


    public void IncFishKillCount()
    {
        fishKillCount++;
    }

    public void DecFishCount()
    {
        fishCount--;
    }

    public void IncRoundCount()
    {
        roundCount++;
    }

    public int ReturnRound()
    {
        return roundCount;
    }

    public string ReturnFishKillCount()
    {
        return fishKillCount.ToString();
    }

    public void DecHealth()
    {
        health--;
        healthText.text = "Health: " + health;
        Debug.Log("Health: " + health);
    }


    public void NextRound()
    {
        roundCount++;
        Debug.Log("Round: " + roundCount);
        fishCount = 3 * roundCount;
        i = 0;
        StartCoroutine(SpawnJelly(fishCount));
    }


    IEnumerator SpawnJelly(int fishCount)
    {
        for (i=i; i < fishCount; i++)
        {
            Debug.Log("spawning jelly");

            GameObject fish = Instantiate(jellyfish);
            //https://answers.unity.com/questions/759542/get-coordinate-with-angle-and-distance.html
            Vector2 pos = new Vector2();
            float a = UnityEngine.Random.Range(0, 2f) * Mathf.PI;
            pos.x = Mathf.Sin(a) * distance;
            pos.y = Mathf.Cos(a) * distance + 10;

            fish.transform.position = pos;

            yield return new WaitForSeconds(1f);
        }
        roundStart = true;
        Debug.Log("Number of Jellies: " + fishCount);
    }


}
