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
    public GameObject backgroundImage;

    public GameObject canvas;
    public GameObject events;

    private int currentLevel = 0;

    public TextMeshProUGUI menuText;

 
    private Coroutine dialogCo;

    private int fishCount;
    private int roundCount = 1;
    private int fishKillCount;

    public GameObject jellyfish;

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {


        if (fishCount <= roundCount){
            IncRound();
        }

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
		}
	}

    private void ChangeLevel()
    {
        if (currentLevel == 0)
        {
            startButton.SetActive(false);
            menuText.text = "";
            StartCoroutine(LoadYourAsyncScene(true, "SampleScene"));
            currentLevel++;
        }
    }

    public void StartButton()
    {
        ChangeLevel();
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

    }


    public void IncFishKillCount()
    {
        fishKillCount++;
    }

    public void IncRound()
    {
        roundCount++;
        nextRound();
    }

    public int ReturnRound()
    {
        return roundCount;
    }

    public string ReturnFishKillCount()
    {
        return fishKillCount.ToString();
    }


    public void nextRound()
    { 
        fishCount = 3 * roundCount;
        StartCoroutine(SpawnJelly(fishCount));
    }


    IEnumerator SpawnJelly(int fishCount)
    {
        for (int i = 0; i < fishCount; i++)
        {
            GameObject fish = Instantiate(jellyfish);
            //https://answers.unity.com/questions/759542/get-coordinate-with-angle-and-distance.html
            Vector2 pos = new Vector2();
            float dist = 28f;
            float a = UnityEngine.Random.Range(0, 2f) * Mathf.PI;
            pos.x = Mathf.Sin(a) * dist;
            pos.y = Mathf.Cos(a) * dist + 10;

            fish.transform.position = pos;

            yield return new WaitForSeconds(.1f);
        }
    }


}
