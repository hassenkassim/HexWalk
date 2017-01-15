using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour{

	public static AudioClip[] levelMusik;
	public static AudioClip menuMusik;
	public static AudioClip gameoverMusik;
	public static AudioClip splashMusik;
	public static AudioClip rotationMusik;
	public static AudioClip winSound;
	public static AudioSource audioSource;
	public static float volume;
	public static SoundManager instance;

	void Awake() {
		
		instance = this;

		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		levelMusik = new AudioClip [12];
		loadMusic ();

		if (PlayerPrefs.GetInt ("SoundOn", 1) == 1) {
			AudioListener.pause = false;
		} else {
			AudioListener.pause = true;
		}

	}

	public void Update () {
		
	}

	public void loadMusic(){

		menuMusik = (AudioClip)Resources.Load ("MenuMusik");
		gameoverMusik = (AudioClip)Resources.Load ("GameoverMusik");
		rotationMusik = (AudioClip)Resources.Load ("RotationSound");
		splashMusik = (AudioClip)Resources.Load ("SplashMusik");
		winSound = (AudioClip)Resources.Load ("GoodLevel");

		levelMusik [0] = (AudioClip)Resources.Load ("LevelMusik5");
		levelMusik [1] = (AudioClip)Resources.Load ("LevelMusik2");
		levelMusik [2] = (AudioClip)Resources.Load ("LevelMusik1");
		levelMusik [3] = (AudioClip)Resources.Load ("LevelMusik4");
		levelMusik [4] = (AudioClip)Resources.Load ("LevelMusik3");
		levelMusik [5] = (AudioClip)Resources.Load ("LevelMusik1");
		levelMusik [6] = (AudioClip)Resources.Load ("LevelMusik2");
		levelMusik [7] = (AudioClip)Resources.Load ("LevelMusik3");
		levelMusik [8] = (AudioClip)Resources.Load ("LevelMusik4");
		levelMusik [9] = (AudioClip)Resources.Load ("LevelMusik5");
		levelMusik [10] = (AudioClip)Resources.Load ("LevelMusik3");
		levelMusik [11] = (AudioClip)Resources.Load ("LevelMusik1");
	}

	public static void playLevelMusic(int number){

		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		audioSource.clip = levelMusik [number];
		audioSource.loop = true;
		instance.StartCoroutine (fadeInMusic (audioSource, 2.0f));	
		//audioSource.Play ();

	}

	public static void playMenuMusic(){
		
		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		audioSource.clip = menuMusik;
		audioSource.loop = true;
		instance.StartCoroutine (fadeInMusic (audioSource, 2.0f));
		//audioSource.Play ();	

	}

	public static void playSplashMusic(){

		audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		audioSource.clip = splashMusik;
		instance.StartCoroutine (fadeInMusic (audioSource, 2.0f));
		//audioSource.volume = 10.0f;
		//audioSource.Play ();
	}

	public static void playWinSound(){
		Vector3 pos = new Vector3 (Gameplay.cam.transform.position.x, Gameplay.cam.transform.position.y, Gameplay.cam.transform.position.z); 
		playSound (winSound, pos, 10.0f);
	}

	public static void stopMusicSmoothly(){
		//audioSource = GameObject.Find ("Audio Source").GetComponent<AudioSource> ();
		//instance.StartCoroutine (fadeOutMusic (audioSource, 0.8f));
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

	static IEnumerator fadeOutMusic(AudioSource source, float speed)
	{
		float t = 10.0f;
		while (t > 0.0f) {
			t -= Time.deltaTime/speed;
			source.volume = t;
			yield return new WaitForSeconds(0);
		}
		source.volume = 0.0f;
		source.Stop ();
		//source.loop = false;
	}

	static IEnumerator fadeInMusic(AudioSource source, float speed)
	{
		source.volume = 0.0f;
		source.Play ();
		//clip.loop = true;
		float t = 0.0f;
		while (t < 10.0f) {
			//print("AudioVolume: "+source.volume);
			t += Time.deltaTime/speed;
			source.volume = t;
			yield return new WaitForSeconds(0);
		}
	}
}
