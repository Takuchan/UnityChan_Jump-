using UnityEngine;
using System.Collections;
//********** 開始 **********//
using UnityEngine.UI;
//********** 開始 **********//

public class PlayerScript : MonoBehaviour {
	
	public float speed = 4f;
	public float jumpPower = 700;
	public LayerMask groundLayer;
	public GameObject mainCamera;
	public GameObject bullet;
	public LifeScript lifeScript;
	
	private Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool isGrounded;
	private Renderer renderer;
	//********** 開始 **********//
	private bool gameClear = false; //ゲームクリアーしたら操作を無効にする
	private bool Time = false;
	public Text clearText; //ゲームクリアー時に表示するテキスト
	//********** 終了 **********//
	
	void Start () {
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		renderer = GetComponent<Renderer>();
	}
	
	void Update ()
	{
		isGrounded = Physics2D.Linecast (
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			groundLayer);
		//********** 開始 **********//
		//ジャンプさせない
		if (!gameClear) {
			//********** 終了 **********//
			if (Input.GetKeyDown ("space")) {
				if (isGrounded) {
					anim.SetTrigger ("Jump");
					isGrounded = false;
					rigidbody2D.AddForce (Vector2.up * jumpPower);
				}
			}
			//********** 開始 **********//
		}
		//********** 終了 **********//
		
		float velY = rigidbody2D.velocity.y;
		bool isJumping = velY > 0.1f ? true : false;
		bool isFalling = velY < -0.1f ? true : false;
		anim.SetBool ("isJumping", isJumping);
		anim.SetBool ("isFalling", isFalling);
		//********** 開始 **********//
		//弾を打たせない、ゲームオーバーにさせない
		if (!gameClear) {
			//********** 終了 **********//
			if (Input.GetKeyDown ("left ctrl")) {
				anim.SetTrigger ("Shot");
				Instantiate (bullet, transform.position + new Vector3 (0f, 1.2f, 0f), Quaternion.identity);
			}
			
			if (gameObject.transform.position.y < Camera.main.transform.position.y - 8) {
				lifeScript.GameOver ();
			}
			//********** 開始 **********//
		}
		//********** 終了 **********//
	}
	
	void FixedUpdate ()
	{
		//********** 開始 **********//
		//左右に移動させない　MainCameraを動かさない
		if (!gameClear) {
			//********** 終了 **********//
			float x = Input.GetAxisRaw ("Horizontal");
			if (x != 0) {
				rigidbody2D.velocity = new Vector2 (x * speed, rigidbody2D.velocity.y);
				Vector2 temp = transform.localScale;
				temp.x = x;
				transform.localScale = temp;
				anim.SetBool ("Dash", true);
				if (transform.position.x > mainCamera.transform.position.x - 4) {
					Vector3 cameraPos = mainCamera.transform.position;
					cameraPos.x = transform.position.x + 4;
					mainCamera.transform.position = cameraPos;
				}
				Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
				Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
				Vector2 pos = transform.position;
				pos.x = Mathf.Clamp (pos.x, min.x + 0.5f, max.x);
				transform.position = pos;
			} else {
				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				anim.SetBool ("Dash", false);
			}
			//********** 開始 **********//
		} else {
			//クリアーテキストを表示
			clearText.enabled = true;

			//アニメーションは走り
			anim.SetBool ("Dash", true);
			//右に進み続ける
			rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
			//5秒後にタイトル画面へ戻るCallTitleメソッドを呼び出す
			Invoke("CallTitle", 5);
		}
		//********** 終了 **********//
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (!gameClear) {
			if (col.gameObject.tag == "Enemy") {
				StartCoroutine ("Damage");
			}
		}
	}
	
	IEnumerator Damage ()
	{
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		int count = 10;
		while (count > 0){
			renderer.material.color = new Color (1,1,1,0);
			yield return new WaitForSeconds(0.05f);
			renderer.material.color = new Color (1,1,1,1);
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		gameObject.layer = LayerMask.NameToLayer("Player");
	}
	//********** 開始 **********//
	void OnTriggerEnter2D (Collider2D col)
	{
		//タグがClearZoneであるTriggerにぶつかったら
		if (col.tag == "ClearZone") {
			//ゲームクリアー
			gameClear = true;
		}
	}
	
	void CallTitle ()
	{
		//タイトル画面へ
		Application.LoadLevel("main2");
	}
	//********** 終了 **********//
}