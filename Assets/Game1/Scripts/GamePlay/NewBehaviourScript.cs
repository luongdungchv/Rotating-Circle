using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Object", menuName = "Objects")]
public class NewBehaviourScript: ScriptableObject {
	
	public List<Sprite> player = new List<Sprite>();
    public Color[] color;
   
    public Gradient[] gradient;

    public ParticleSystem connectEffect;
    public ParticleSystem deadEffect;
}
