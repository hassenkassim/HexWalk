using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour{

	public static AudioClip[] audios;

	public static AudioClip rotationMusik;
	public static AudioClip[] levelMusik;
	public static AudioClip gameoverMusik;
	public static AudioClip menuMusik;
	public static AudioClip splashMusik;

	public void Start () {
		loadMusic ();
	}

	public void loadMusic(){
		levelMusik = new AudioClip [10];

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

	}

	public static void playSound(AudioClip sound, Vector3 sourcePos, float vol){
		AudioSource.PlayClipAtPoint (sound, sourcePos, vol);
	}

	public static void playLevelMusic(int number){
		Vector3 pos = new Vector3 (Gameplay.player.getPosition ().x, Gameplay.player.getPosition ().y, Gameplay.player.getPosition ().z); 
		playSound (levelMusik[number], pos, 10.0f);
	}

	public static void playMenuMusic(){
		Vector3 pos = new Vector3 (LevelPlay.gamePosition.x, 0, LevelPlay.gamePosition.y); 
		playSound (menuMusik, pos, 10.0f);
	}

	public static void playSplashMusic(){
		Vector3 pos = new Vector3 (LevelPlay.gamePosition.x, 0, LevelPlay.gamePosition.y); 
		playSound (splashMusik, pos, 10.0f);

	}

	public static void playRotationSound(string SceneName){
		if (SceneName == "GameScene") {
			Vector3 pos = new Vector3 (Gameplay.player.getPosition ().x, Gameplay.player.getPosition ().y, Gameplay.player.getPosition ().z); 
			playSound (rotationMusik, pos, 10.0f);
		} else if (SceneName == "LevelScene") {
			Vector3 pos = new Vector3 (LevelPlay.gamePosition.x, 0, LevelPlay.gamePosition.y); 
			playSound (rotationMusik, pos, 10.0f);
		}
	}

	public static void playGameoverMusic(){
		Vector3 pos = new Vector3 (Gameplay.player.getPosition ().x, Gameplay.player.getPosition ().y, Gameplay.player.getPosition ().z); 
		playSound (gameoverMusik, pos, 10.0f);
	}
}
