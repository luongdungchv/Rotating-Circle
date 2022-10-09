using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour {
    public float mag;
    public IEnumerator shake(float duration)
    {
        Vector3 pos = transform.position;
        float elapse = 0;
        while (elapse < duration)
        {
            float x = Random.Range(-mag, mag);
            float y = Random.Range(-mag, mag);
            Vector3 newPos = pos + new Vector3(x, y, pos.z);
            transform.position = newPos;
            elapse += Time.deltaTime;
            yield return null;

        }
        transform.position = pos;
    }
}
