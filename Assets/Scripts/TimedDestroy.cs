using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

    public float destroyTimer;

	// Use this for initialization
	void Start () {
        StartCoroutine("Destroy");
    }

    private IEnumerator Destroy()
    {

            yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }
}
