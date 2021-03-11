using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    private GameObject exclusion;
    private GameObject spawner;

    public GameObject jellyfish;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<GameObject>();
        exclusion = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextRound()
    {
        int roundCount = 1; //get round count from Game Manager?
        int fishCount = 3 * roundCount;
        StartCoroutine(SpawnJelly(fishCount));
    }

    IEnumerator SpawnJelly(int fishCount)
    {
        for (int i=0; i < fishCount; i++)
        {
            GameObject fish = Instantiate(jellyfish);
            //https://answers.unity.com/questions/759542/get-coordinate-with-angle-and-distance.html
            Vector2 pos = new Vector2();
            float dist = 28f;
            float a = Random.Range(0, 2f) * Mathf.PI;
            pos.x = Mathf.Sin(a) * dist;
            pos.y = Mathf.Cos(a) * dist;

            fish.transform.position = pos;

            yield return new WaitForSeconds(.1f);
        }
    }
}
