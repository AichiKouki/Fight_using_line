using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackObjectController : MonoBehaviour {

	//アタックオブジェクト自動削除
	private float time_to_delete;

	//ビールを投げられた場合、ずっと跳ね返る処理のため
	Rigidbody2D rigid2D;
	// Use this for initialization
	void Start () {
		rigid2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//攻撃オブジェクトがステージにたまらないように削除
		time_to_delete+=Time.deltaTime;//攻撃オブジェクトを削除するまでの時間
		if (time_to_delete > 3) Destroy (gameObject);//攻撃オブジェクトを生成してから3秒たったら、削除する

	}
}
