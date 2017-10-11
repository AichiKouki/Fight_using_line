using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//描いた線は、ボスの攻撃の時のみ消えてしまう処理
public class LINEController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Deleting_all_child_elements(){
		// すべての子オブジェクトを取得
		foreach ( Transform n in gameObject.transform ){
			GameObject.Destroy(n.gameObject);
		}
	}
}
