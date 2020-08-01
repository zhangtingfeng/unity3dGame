using UnityEngine;
using System.Collections;

public class NewUserAnimation : MonoBehaviour {

    public Animator animationNew;
    private bool boolifshow = false;

    public void showanimator() {
        boolifshow = !boolifshow;
        Debug.Log(boolifshow);
        if (boolifshow)
        {
            animationNew.Play("NewUserIN");
        }
        else {
            animationNew.Play("NewUserOut");

        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
