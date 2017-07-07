using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeScript : MonoBehaviour {
	private float time = 60;

	//********** 追記 **********// 

	public GameObject gameOverText;
	//********** 追記 **********// 

	void Start () {
		//********** 追記 **********// 
		gameOverText.SetActive(false);
		//********** 追記 **********// 
		GetComponent<Text>().text = ((int)time).ToString();
	}

	void Update ()
	{
		//timer減少処理
		if (time <= 0) {
			Input.ResetInputAxes ();
		}
	
		time -= Time.deltaTime;
		//********** 追記 **********// 
		if (time < 0) {
			StartCoroutine("GameOver");
		}
		//********** 追記 **********// 
		if (time < 0) time = 0;
		GetComponent<Text> ().text = ((int)time).ToString ();

	}

	//********** 追記 **********// 
	IEnumerator GameOver () {
		gameOverText.SetActive(true);
	

		yield return new WaitForSeconds(2.0f);
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("title");
		}
	}

}


