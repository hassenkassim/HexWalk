using UnityEngine;
using System.Collections;
using UnityEngine.UI;



/*
 * This class controls the player and the collisions with other gameobjects
 * */
public class PlayerController : MonoBehaviour {
	public float rotationPeriod = 0.25f;		// 隣に移動するのにかかる時間
	public float sideLength = 1f;			// Cubeの辺の長さ

	bool isRotate = false;					// Cubeが回転中かどうかを検出するフラグ
	float directionX = 0;					// 回転方向フラグ
	float directionZ = 0;					// 回転方向フラグ

	Vector3 startPos;						// 回転前のCubeの位置
	float rotationTime = 0;					// 回転中の時間経過
	float radius;							// 重心の軌道半径
	Quaternion fromRotation;				// 回転前のCubeのクォータニオン
	Quaternion toRotation;					// 回転後のCubeのクォータニオン

	AudioClip rotationSound = null;

	static int score;

	Text scoreText;


	// Use this for initialization
	void Start () {
		radius = sideLength * Mathf.Sqrt (2f) / 2f;
		rotationSound = (AudioClip)Resources.Load("jump");

		// score value
		score = 0;
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	
	}

	// Update is called once per frame
	void Update () {

		float x = 0;
		float y = 0;

		x = Input.GetAxisRaw ("Horizontal");
		if (x == 0) {
			y = Input.GetAxisRaw ("Vertical");
		}

		//check if move is allowed
		if (checkOutside (x,y)) {
			Debug.Log ("Player out of Gamefield, No Rotation possible!");
			return;
		}


		if ((x != 0 || y != 0) && !isRotate) {

			if (x != 0) {
				Gameplay.player.setGamePosition (new Vector2 (Gameplay.player.getGamePosition ().x + x, Gameplay.player.getGamePosition ().y));
			} else if (y != 0) {
				Gameplay.player.setGamePosition (new Vector2 (Gameplay.player.getGamePosition ().x, Gameplay.player.getGamePosition ().y + y));
			}
			Debug.Log ("Player Position: " + Gameplay.player.getGamePosition());

			directionX = -x;																// 回転方向セット (x,yどちらかは必ず0)
			directionZ = y;																// 回転方向セット (x,yどちらかは必ず0)
			startPos = transform.position;												// 回転前の座標を保持
			fromRotation = transform.rotation;											// 回転前のクォータニオンを保持
			transform.Rotate (directionZ * 90, 0, directionX * 90, Space.World);		// 回転方向に90度回転させる
			toRotation = transform.rotation;											// 回転後のクォータニオンを保持
			transform.rotation = fromRotation;											// CubeのRotationを回転前に戻す。（transformのシャローコピーとかできないんだろうか…。）
			rotationTime = 0;															// 回転中の経過時間を0に。
			isRotate = true;															// 回転中フラグをたてる。
		}


	}
		
	void FixedUpdate() {

		if (isRotate) {

			rotationTime += Time.fixedDeltaTime;									// 経過時間を増やす
			float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod);			// 回転の時間に対する今の経過時間の割合

			// 移動
			float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio);					// 回転角をラジアンで。
			float distanceX = -directionX * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));		// X軸の移動距離。 -の符号はキーと移動の向きを合わせるため。
			float distanceY = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin (45f * Mathf.Deg2Rad));						// Y軸の移動距離
			float distanceZ = directionZ * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));			// Z軸の移動距離
			transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);						// 現在の位置をセット

			// 回転
			transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio);		// Quaternion.Lerpで現在の回転角をセット（なんて便利な関数）

			// 移動・回転終了時に各パラメータを初期化。isRotateフラグを下ろす。
			if (ratio == 1) {
				isRotate = false;
				directionX = 0;
				directionZ = 0;
				rotationTime = 0;
			}
				
		}
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.collider.gameObject.CompareTag("Field")) {

			//Alles was ich brauche bekomme ich durch unsere Gameplay Klasse. Egal was ich brauche, ich gehe zuerst ins Gameplay rein, dann entweder Gamefield, 
			//oder Player oder Path, je nachdem was ich brauche und dann rufe ich den jeweiligen Getter auf!
			Vector2 platePos = Gameplay.player.getGamePosition ();//dazu gehe ich in unser Gameplay->Player->getGamePosition
			Field field = Gameplay.gamefield.getField((int)platePos.x, (int) platePos.y);
			int pointer = Gameplay.pathfinder.pointer;
			if(field.getColor().Equals(Color.green)) return;
			if (field.getColor ().Equals (Color.blue)) {
				Gameplay.pathfinder.pointer = - 1;
				print ("WON!");
				LevelManager.levelUp ();

			}

			if (Gameplay.pathfinder.path[pointer].Equals(platePos)) {
				Gameplay.pathfinder.pointer++;
				field.setColor (Color.green);
				AudioSource.PlayClipAtPoint (rotationSound, transform.position, 10f);
				score = score + 1;
				displayScore ();
			} else {
				field.setColor (Color.red);
				print ("GAMEOVER!");
				GameOver.displayGameover ();

			}




			//coll.gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
			//AudioSource.PlayClipAtPoint (rotationSound, transform.position, 10f);
			//Vector2 platePos = new Vector2(Input.coll.x , Input.coll.y);
			//if (Pathfinder.path.Contains (platePos) == true) {
			//	AudioSource.PlayClipAtPoint (rotationSound, transform.position, 10f);
			//}
		}
	}


	void OnCollisionExit(Collision collisionInfo) {
		//collisionInfo.gameObject.GetComponent<Renderer> ().material.color = Color.black;
	}


	bool checkOutside(float x, float y){
		if (x == -1) {
			if (Gameplay.player.getGamePosition ().x == 0)
				return true;
		} else if (x == 1) {
			if (Gameplay.player.getGamePosition ().x == Gameplay.gamefield.width - 1)
				return true;
		} else if (y == -1) {
			if (Gameplay.player.getGamePosition ().y == 0)
				return true;
		} else if (y == 1) {
			if (Gameplay.player.getGamePosition ().y == Gameplay.gamefield.height - 1)
				return true;
		}
		return false;
	}

	public void displayScore(){
		scoreText.text = "Score: " + score.ToString ();
	}

	public static int getScore(){
		return score;
	}
}
