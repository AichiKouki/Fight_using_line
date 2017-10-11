using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkController : MonoBehaviour {

	//Transform parent;
	LINEController lineController;

	// Use this for initialization
	void Start () {
		lineController=transform.parent.GetComponent<LINEController>();//親オブジェクトのLINEControllerを取得
		//Debug.Log (parent.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "AttackObject") {
			Destroy (other.gameObject);
			lineController.Deleting_all_child_elements ();
		}
	}
}
