using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChestTestScript : MonoBehaviour
{
	private Animator anim;
	public bool Open = false;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Open = !Open;

			string trigger = Open ? "Open" : "Close";
			Debug.Log(trigger);
			anim.SetTrigger(trigger);
		}
	}
}
