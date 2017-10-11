using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineNoGravity : MonoBehaviour
{

	public GameObject linePrefab;
	public float lineLength = 0.2f;
	public float lineWidth = 0.1f;

	private Vector3 touchPos;

	//線を引く際のエフェクト
	public GameObject effectPre;
	GameObject effect;

	//線を全体を格納する空のオブジェクト指定
	public GameObject Line_way;

	//操作説明シーンで、線がかけたかどうか判定処理
	public bool painting_termination=false;
	private float draw_time;

	//いっぱい複製したオブジェクトを空のオブジェクトにまとめる
	public GameObject summarize_ink_NoGravityPre;
	GameObject summarize_ink_NoGravity;

	void Start(){

	}

	void Update (){
		drawLine ();
	}

	//会がスタートから終了までの処理全般
	void drawLine(){

		if(Input.GetMouseButtonDown(0))
		{
			touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			touchPos.z=0;
			Debug.Log ("線の絵画をスタート");
			summarize_ink_NoGravity = (GameObject)Instantiate (summarize_ink_NoGravityPre,transform.position,Quaternion.identity);
			summarize_ink_NoGravity.transform.parent=gameObject.transform;//インクをまとめるオブジェクトはこのスクリプトがアタッチされてるオブジェクトの子要素にする。
		}

		//線を絵画している最中の処理
		if(Input.GetMouseButton(0))
		{

			Vector3 startPos = touchPos;
			Vector3 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			endPos.z=0;

			if((endPos-startPos).magnitude > lineLength){
				GameObject obj = Instantiate(linePrefab, transform.position, transform.rotation) as GameObject;
				obj.transform.position = (startPos+endPos)/2;
				obj.transform.right = (endPos-startPos).normalized;

				obj.transform.localScale = new Vector3( (endPos-startPos).magnitude, lineWidth , lineWidth );

				summarize_ink_NoGravity.transform.parent = Line_way.transform;
				obj.transform.parent = summarize_ink_NoGravity.transform;
	
				touchPos = endPos;

				//線を引く際のエフェクト生成
				effect=(GameObject)Instantiate(effectPre,obj.transform.position,Quaternion.identity);

				//描いてる時間を測定
				draw_time+=Time.deltaTime;

			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (draw_time > 0.01f) {
				painting_termination = true;
			}
		}
	}//drawLine
}
