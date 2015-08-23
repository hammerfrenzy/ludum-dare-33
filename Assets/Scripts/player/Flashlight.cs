using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceBehind()
    {
        transform.localPosition = new Vector3(-.25f, 0, 0);
    }

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
    }
}
