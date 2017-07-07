using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {
	
	RectTransform rt;
	//********** 開始 **********//
	public GameObject unityChan; //ユニティちゃん
	public GameObject explosion; //爆発アニメーション
	public Text gameOverText; //ゲームオーバーの文字
	private bool gameOver = false; //ゲームオーバー判定
	//********** 終了 **********//
	void Start ()
	{
		rt = GetComponent<RectTransform>();
	}
	//********** 開始 **********//
	void Update ()
	{
		//ライフが0以下になった時、
		if (rt.sizeDelta.y <= 0) {
			//ゲームオーバー判定がfalseなら爆発アニメーションを生成
			//GameOverメソッドでtrueになるので、1回のみ実行
			if (gameOver == false) {
				Instantiate (explosion, unityChan.transform.position + new Vector3 (0, 1, 0), unityChan.transform.rotation);
			}
			//ゲームオーバー判定をtrueにし、ユニティちゃんを消去
			GameOver ();
		}
		//ゲームオーバー判定がtrueの時、
		if (gameOver) {
			//ゲームオーバーの文字を表示
			gameOverText.enabled = true;
			//画面をクリックすると
			if (Input.GetMouseButtonDown (0)) {
				//タイトルへ戻る
				Application.LoadLevel("Title");
			}
		}
	}
	//********** 終了 **********//
	
	public void LifeDown (int ap)
	{
		rt.sizeDelta -= new Vector2 (0,ap);
	}
	
	public void LifeUp (int hp)
	{
		rt.sizeDelta += new Vector2 (0,hp);
		
		if (rt.sizeDelta.y > 240f) {
			rt.sizeDelta = new Vector2 (51f, 240f);
		}
	}
	
	//********** 開始 **********//
	public void GameOver ()
	{
		gameOver = true;
		Destroy(unityChan);
	}
	//********** 終了 **********//
}