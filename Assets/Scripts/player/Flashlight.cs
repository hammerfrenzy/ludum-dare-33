using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    public Light flashlightLight;

    bool flipped = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        flipped = gameObject.transform.parent.localScale.x == -1;
	}

    public void PlaceBehind()
    {
        Reset();
        transform.localPosition = new Vector3(-.25f, 0, 0);
    }

    public void PlaceBelow()
    {
        Reset();
        transform.localPosition = new Vector3(-0.107f, -0.21f, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 72.43233f);
        if (flipped) 
        {
            flashlightLight.transform.localRotation = Quaternion.Euler(-34.1f, 90, 180);
        }
    }

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        float yRotation = flipped ? 270 : 90;
        flashlightLight.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
