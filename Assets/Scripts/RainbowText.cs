using UnityEngine;
using System.Collections;

public class RainbowText : MonoBehaviour {

    public float speed = .25f;

    GUIText text;
    float time = 0;
    int lastRandom = 0;
	// Use this for initialization
	void Start () {
        text = GetComponent<GUIText>();
	}

    public void TextTriggered()
    {
        StartCoroutine(TimedDisable());
    }

    IEnumerator TimedDisable(float offTime = 2f)
    {
        float time = 0;
        while (time < offTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        text.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!text.enabled) return; 

        time += Time.deltaTime;
        if (time > speed)
        {
            text.color = RandomColor();
            time = 0;
        }
	}

    // hardcoding this for time reasons
    Color RandomColor()
    {
        Color color = Color.black;
        
        int random;
        do
        {
            random = Random.Range(0, 5);
        } while (random == lastRandom);
        lastRandom = random;

        if (random == 0)
        {
            color = Color.blue;
        }
        if (random == 1)
        {
            color = Color.red;
        }
        if (random == 2)
        {
            color = Color.green;
        }
        if (random == 3)
        {
            color = Color.yellow;
        }
        if (random == 4)
        {
            color = Color.magenta;
        }
        if (random == 5)
        {
            color = Color.cyan;
        }

        return color;
    }
}
