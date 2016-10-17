using UnityEngine;
using System.Collections;

public class SoundManager {

	static AudioClip rotationSound;
	static AudioClip levelMusic;



	public SoundManager(){
		//Setup rotationSound
		rotationSound = (AudioClip)Resources.Load("jump");
		levelMusic = (AudioClip)Resources.Load ("musicMain");
	}

	public void playRotationSound(string SceneName){
		if (SceneName == "GameScene") {
			Vector3 pos = new Vector3 (Gameplay.player.getPosition ().x, Gameplay.player.getPosition ().y, Gameplay.player.getPosition ().z); 
			playSound (rotationSound, pos, 10.0f);
		} else if (SceneName == "LevelScene") {
			Vector3 pos = new Vector3 (LevelPlay.gamePosition.x, 0, LevelPlay.gamePosition.y); 
			playSound (rotationSound, pos, 10.0f);
		}
	}

	public void playLevelMusic(){
		Vector3 pos = new Vector3 (Gameplay.player.getPosition ().x, Gameplay.player.getPosition ().y, Gameplay.player.getPosition ().z); 
		playSound (rotationSound, pos, 10.0f);
	}



	private void playSound(AudioClip sound, Vector3 sourcePos, float vol){
		AudioSource.PlayClipAtPoint (sound, sourcePos, vol);
	}
}
