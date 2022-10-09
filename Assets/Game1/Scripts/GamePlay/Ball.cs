using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;



public class Ball : MonoBehaviour {

	public static Ball ins;

	public GameObject basePoint;

	public GameObject ropePrefab;

	public GraphicManager graphicManager;

	public Text scoreText, highscoreText;

	public GameObject pausePanel;

	public int score = 0;
	public int highscore = 0;

	public bool isConnected = false;
	bool moving = false;

	public float intensity;

	public bool isReleased = false;

    float checkPoint = 0;	

	Rigidbody2D body;
    

    public Color[] colorList;
		
	
	// Use this for initialization
	void Awake () {
		body = GetComponent<Rigidbody2D> ();
		ins = this;
		Debug.Log(graphicManager.settingData.isHigh);

		SetLight(graphicManager.GetGraphicState());

		Manager.OnSettingSaved += (s, e) => SetLight(graphicManager.GetGraphicState());
		
    }
	void SetLight(bool state)
    {
		GetComponent<Light>().enabled = state;
		transform.parent.GetComponent<Light>().enabled = state;
		GameObject.Find("Base (1)").GetComponentInChildren<Light>().enabled = state;
	}
	Color randomcolor(){
		int index = Random.Range (0, colorList.Length - 1);
        
		return  colorList [index];
	}

	// Update is called once per frame
	void FixedUpdate () {
		this.GetComponent<SpriteRenderer> ().sprite = Manager.ins.currentPlayerImg.sprite;
        this.GetComponent<Light>().color = Manager.ins.player.color[Manager.ins.playerIndex];
        this.GetComponent<TrailRenderer>().colorGradient = Manager.ins.player.gradient[Manager.ins.playerIndex];
		this.GetComponent<ParticleSystem>().startColor = Manager.ins.player.color[Manager.ins.playerIndex];

		if (moving) {
			moveLeft (basePoint.gameObject);
			if (base.transform.position.x <= -6) {
				moving = false;
                isReleased = false;
				basePoint.transform.position = new Vector2 (-6, basePoint.transform.position.y);
			}
		}
		highscore = PlayerPrefs.GetInt ("Highscore", 0);
		scoreText.text = score.ToString ();
		highscoreText.text = highscore.ToString ();

        if (Input.GetKeyDown(KeyCode.Space))
        {
			GameObject obj = GameObject.Find("Base (1)(Clone)");
			movingTarget m = obj.GetComponent<movingTarget>();

			m.speed = 0;
			Destroy(m);
        }
	}
	void OnTriggerEnter2D(Collider2D target){
		if (target.tag != "edge") {
			body.velocity = new Vector2 (0, 0);

			var t = target.transform.parent.GetComponent<movingTarget>();
			target.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			Destroy(t); 
			//Destroy(GetComponent<Rotation>());

            score++;
            if (score > highscore)
                PlayerPrefs.SetInt("Highscore", score);
			if (score % 10 == 0)
			{
				checkPoint = score / 10;
				
			}

			Connect (target.gameObject);

            var addRotate = target.gameObject.transform.parent.gameObject.AddComponent<Rotation>();
			addRotate.speed = 140f;
            addRotate.speed += 30f * checkPoint;

			transform.localScale = new Vector2 (0.27f, 0.4037074f);
			float angle = transform.rotation.z;
			transform.rotation = Quaternion.AngleAxis (-angle, Vector3.forward);

			float y = Random.Range(-2, 2f);
			float x = Random.Range (6, 7.6f);

			var newRope = Instantiate (ropePrefab, new Vector2 (x, y), Quaternion.identity);
			newRope.GetComponentInChildren<Light>().enabled = GraphicManager.ins.settingData.isHigh;

			if (checkPoint % 2 == 0)
			{
				movingTarget mover;
				try
				{
					mover = newRope.GetComponent<movingTarget>();
					mover.speed = -(checkPoint / 2);
				}
				catch
				{
					mover = newRope.AddComponent<movingTarget>();
					mover.speed = -(checkPoint / 2);

				}
			}

			Vector2 scaleRope = newRope.transform.Find("Rope").localScale;
            scaleRope.y -= checkPoint * 0.03f;
            newRope.transform.Find("Rope").localScale = scaleRope;
            
			Color newCol = randomcolor ();
			
			newRope.transform.Find("Rope").gameObject.GetComponent<SpriteRenderer> ().color = newCol;

			newRope.transform.Find("Rope").GetComponent<Light>().color = newRope.transform.Find("Rope").gameObject.GetComponent<SpriteRenderer>().color;

			target.tag = "edge";

            var spawn = Instantiate(Manager.ins.player.connectEffect.gameObject, transform.position, Quaternion.identity);
            spawn.transform.SetParent(target.gameObject.transform.parent);
            spawn.GetComponent<ParticleSystem>().startColor = target.gameObject.GetComponent<SpriteRenderer>().color;
            spawn.GetComponent<ParticleSystem>().Play();
                
		}

	}
	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "edge") {
			
		}
	}
	public void Release (){
		this.gameObject.AddComponent<Rotation>().speed = 200f;
		gameObject.transform.parent = null;//Make the ball indie from its parent
		isReleased = true;
		body.gravityScale = 1.3f;
		GameObject bottom = basePoint.transform.Find("Bottom").gameObject;
		//Identify normal vector
		Vector2 intoMiddle = bottom.transform.position - basePoint.transform.position;
		Vector2 normal = new Vector2 (-intoMiddle.y, intoMiddle.x);
		//Move the ball
		body.velocity = normal * intensity;
	}
	void Connect (GameObject newRope){
		//Remove rotation component
        Destroy(this.gameObject.GetComponent<Rotation>());
		
		isConnected = true;
		//Destroy old rope
		Destroy (basePoint.gameObject);
		//identify new basePoint
		basePoint = newRope.transform.parent.gameObject;
		transform.SetParent (newRope.transform);
		//Stop the ball

		body.gravityScale = 0;
		//Enabling rotation of newRope
		moving = true;
	}
	void moveLeft(GameObject g){
		g.transform.position -= new Vector3 (0.5f ,0,0);
	}
}



