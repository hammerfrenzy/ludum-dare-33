using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Animator))]
public class Lamp : MonoBehaviour {

    public MonsterPlatform[] Platforms;
    public float OnTime = 2;
    public float OffTime = 2;

    Light lampLight;
    Animator animator;
    float timer = 0;
    bool isOn = false;

    void Awake()
    {
        if (Platforms == null) Platforms = new MonsterPlatform[0];
    }

	// Use this for initialization
	void Start () {
        lampLight = GetComponentInChildren<Light>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        float compareTime = isOn ? OnTime : OffTime;

        if (timer >= compareTime)
        {
            isOn = !isOn;
            if (isOn) TurnOn();
            else TurnOff();
            timer = 0;
        }
        else if (!isOn && timer > compareTime / 2)
        {
            TurnPartial();
        }
	}

    public void TurnOn()
    {
        animator.Play("lampOn");
        lampLight.enabled = true;
        for (int x = 0; x < Platforms.Length; x++)
        {
            MonsterPlatform platform = Platforms[x];
            platform.SetCanBeRevealed(false);
            platform.Hide();
        }
    }

    public void TurnOff()
    {
        animator.Play("lampOff");
        lampLight.enabled = false;
        for (int x = 0; x < Platforms.Length; x++)
        {
            MonsterPlatform platform = Platforms[x];
            platform.SetCanBeRevealed(true);
        }
    }

    public void TurnPartial()
    {
        animator.Play("lampPartial");
    }
}
