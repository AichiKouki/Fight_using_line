using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	//てきはダメージを受けるとチカチカする処理
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	private float blackValue = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//衝突判定
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Line") {//線がぶつかった時だけ処理
			//Debug.Log ("ユーザーが描いた線にぶつかった");
			StartCoroutine ("Flashing");//チカチカコルーチン処理開始
		}
	}

	//線にぶつかったら、このキャラクターを地下チアkさせる(ダメージ演出)
	IEnumerator Flashing(){
		for(int i=1;i<5;i++){
			if (i % 2 == 0) blackValue = 255;
			else blackValue = 0;
		spriteRenderer.color=new Color(255.0f/255,255.0f/255,255.0f/255,blackValue/255);//カラーの部分のAの部分を変更してチカチカを実現している
			yield return new WaitForSeconds (0.1f);
		}

		Destroy (gameObject);//攻撃を食らったら削除される
	}
}
