using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//線を引くスクリプト
public class DrawPhysicsLine : MonoBehaviour
{

	public GameObject linePrefab;
	public float lineLength = 0.2f;//線の最小の長さ。指定した数値以上の距離をドラッグしないと絵画しないようにする。数値を大きくすると分かりやすい。
	public float lineWidth = 0.1f;//線の幅を指定。0.1以外にすると線に切れ目が見えてしまうので、0.1で固定にする。

	private Vector3 touchPos;

	//いっぱい複製したオブジェクトを空のオブジェクトにまとめる
	public GameObject summarize_inkPre;
	GameObject summarize_ink;
	Rigidbody2D rigid;
	private bool end_painting;

	//線を引く際のエフェクト
	public GameObject effectPre;
	GameObject effect;

	//線全体を格納する空のオブジェクト指定
	public GameObject Line_drop;

	//操作説明シーンで、線がかけたかどうか判定処理
	public bool painting_termination=false;
	private float draw_time;//クリックしただけで成功と判断させないようにするため

	//画面を一種タップするようにしたら、小さい丸のボジェクトを作成する
	public GameObject smallInkPre;
	GameObject smallInk;
	Vector3 endPos;

	void Start(){
	}

	void Update (){
		drawLine ();
	}

	//絵画処理
	void drawLine(){

		//クリックが始まったら呼ばれる
		if(Input.GetMouseButtonDown(0))
		{
			touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//マウスの場所を格納
			touchPos.z=0;

			summarize_ink = (GameObject)Instantiate (summarize_inkPre,transform.position,Quaternion.identity);//インクをまとめるオブジェクト作成
			summarize_ink.transform.parent=gameObject.transform;//インクをまとめるオブジェクトはこのスクリプトがアタッチされてるオブジェクトの子要素にする。
			rigid = summarize_ink.GetComponent<Rigidbody2D> ();//まとめるオブジェクト作成するたびにRigidbody取得
			rigid.bodyType = RigidbodyType2D.Kinematic;//線をまとめる空のオブジェクトの重力状態を戻す
		}
		//クリックしてる最中に呼ばれる
		if(Input.GetMouseButton(0))
		{
			Vector3 startPos = touchPos;
			endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//一瞬しかタップしなかった時の処理をしたいので、勝手に上で宣言した
			endPos.z=0;

			if((endPos-startPos).magnitude > lineLength){
				GameObject obj = Instantiate(linePrefab, transform.position, transform.rotation) as GameObject;
				obj.transform.position = (startPos+endPos)/2;
				obj.transform.right = (endPos-startPos).normalized;

				obj.transform.localScale = new Vector3( (endPos-startPos).magnitude, lineWidth , lineWidth );

				//obj.transform.parent = this.transform;このスクリプトをアタッチしたオブジェクトを親にする
				summarize_ink.transform.parent=Line_drop.transform;
				obj.transform.parent = summarize_ink.transform;//オブジェクトを単体にするとRigidbodyにするとバラバラになって落ちるのでまとめるため

				touchPos = endPos;

				//線を引く際のエフェクト生成
				effect=(GameObject)Instantiate(effectPre,obj.transform.position,Quaternion.identity);

				//描いてる時間を測定
				draw_time+=Time.deltaTime;//クリックしただけで成功と判断させないようにするため
			}
		}

		//絵画が終了したら、線全体に重力を加える
		if (Input.GetMouseButtonUp (0)) {
			rigid.bodyType = RigidbodyType2D.Dynamic;//絵画が終了したタイミングで重力を加える
			if (draw_time > 0.01f) {//クリックしただけで成功と判断させないようにするため
				painting_termination = true;
				draw_time = 0;
			} else {//一瞬だけタップした時に、小さい丸のオブジェクトを作成する処理
				smallInk=(GameObject)Instantiate(smallInkPre,endPos,Quaternion.identity);
			}
		}
	}
}