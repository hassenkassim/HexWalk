using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour{

	public static AudioClip[] levelMusik;
	public static AudioClip menuMusik;
	public static AudioClip gameoverMusik;
	public static AudioClip splashMusik;
	public static AudioClip rotationMusik;
	public static AudioSource audioSource;

	public void Start () {
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		levelMusik = new AudioClip [12];
		loadMusic ();
	}

	public void loadMusic(){

		menuMusik = (AudioClip)Resources.Load ("MenuMusik");
		gameoverMusik = (AudioClip)Resources.Load ("GameoverMusik");
		rotationMusik = (AudioClip)Resources.Load ("RotationsMusik");
		splashMusik = (AudioClip)Resources.Load ("SplashMusik");

		levelMusik [0] = (AudioClip)Resources.Load ("LevelMusik1");
		levelMusik [1] = (AudioClip)Resources.Load ("LevelMusik2");
		levelMusik [2] = (AudioClip)Resources.Load ("LevelMusik3");
		levelMusik [3] = (AudioClip)Resources.Load ("LevelMusik4");
		levelMusik [4] = (AudioClip)Resources.Load ("LevelMusik5");
		levelMusik [5] = (AudioClip)Resources.Load ("LevelMusik1");
		levelMusik [6] = (AudioClip)Resources.Load ("LevelMusik2");
		levelMusik [7] = (AudioClip)Resources.Load ("LevelMusik3");
		levelMusik [8] = (AudioClip)Resources.Load ("LevelMusik4");
		levelMusik [9] = (AudioClip)Resources.Load ("LevelMusik5");
		levelMusik [10] = (AudioClip)Resources.Load ("LevelMusik5");
		levelMusik [11] = (AudioClip)Resources.Load ("LevelMusik5");
	}

	public static void playLevelMusic(int number){
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		audioSource.clip = levelMusik[number];
		audioSource.loop = true;
		audioSource.Play ();
	}

	public static void playMenuMusic(){
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		audioSource.clip = menuMusik;
		audioSource.loop = true;
		audioSource.Play ();	

	}

	public static void playSplashMusic(){
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		audioSource.clip = splashMusik;
		audioSource.volume = 10.0f;
		audioSource.Play ();

	}

	public static void playRotationSound(string SceneName){
		if (SceneName == "GameScene") {
			Vector3 pos = new Vector3 (Gameplay.cam.transform.position.x, Gameplay.cam.transform.position.y, Gameplay.cam.transform.position.z); 
			playSound (rotationMusik, pos, 10.0f);
		} else if (SceneName == "LevelScene") {
			Vector3 pos = new Vector3 (LevelPlay.cam.transform.position.x, LevelPlay.cam.transform.position.z, LevelPlay.cam.transform.position.y); 
			playSound (rotationMusik, pos, 10.0f);
		}
	}

	public static void playGameoverMusic(){
		Vector3 pos = new Vector3 (Gameplay.cam.transform.position.x, Gameplay.cam.transform.position.y, Gameplay.cam.transform.position.z); 
		playSound (gameoverMusik, pos, 10.0f);
	}

	public static void stopMusic(){
		audioSource.Stop();
	}

	public static void playSound(AudioClip sound, Vector3 sourcePos, float vol){
		AudioSource.PlayClipAtPoint (sound, sourcePos, vol);
	}
}
