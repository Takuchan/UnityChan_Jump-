using UnityEngine;
using System.Collections;

public class stage2coll : MonoBehaviour
{

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Application.LoadLevel("main2");
    }
}