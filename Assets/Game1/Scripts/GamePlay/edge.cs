using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edge : MonoBehaviour {
    public GameObject deadFx;
    void OnCollisionEnter2D(Collision2D hit)
    {
        var fx = Instantiate(deadFx, hit.transform.position, Quaternion.identity);
        ParticleSystem particle = fx.GetComponent<ParticleSystem>();
        particle.startColor = Manager.ins.player.color[Manager.ins.playerIndex];
        Manager.ins.Lose();
        StartCoroutine(Camera.main.GetComponent<CamShake>().shake(0.15f));
        Destroy(hit.gameObject);
    }
}
