using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
	
	public int healPoint = 20;
	//********** 開始 **********//
	//Prefab化するとInspectorから指定できないためprivate化
	private LifeScript lifeScript;
	
	void Start ()
	{
		//HPタグの付いているオブジェクトのLifeScriptを取得
		lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
	}
	//********** 終了 **********//
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "UnityChan") {
			lifeScript.LifeUp(healPoint);
			Destroy(gameObject);
		}
	}
}