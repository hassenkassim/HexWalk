using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroGame : MonoBehaviour {
	static IntroGame instance;

	public static GameObject playerobj;
	private static GameObject gameName;
	private static Camera cam;
	private static GameObject introText;
	private static GameObject loadingBar;
	private static GameObject swipeIcon;

	public Sprite swipeRight; 
	public Sprite swipeLeft; 
	public Sprite swipeTap; 
	public Sprite swipeUp; 
	public Sprite swipeDown; 

	public static Vector2 pos;
	public float splashOffset = 8.2f;
	public static GameObject field;
	private static GameObject[] fields;
	public float rotationPeriod = 0.25f;		
	float directionX = 0;					
	float directionZ = 0;
	Vector3 startPos;						
	float rotationTime = 0;					
	public static float radius;							
	Quaternion fromRotation;				
	Quaternion toRotation;	
	bool isRotate;					
	Vector2 input;

	static bool waitForFunction; // wait to the end of the description
	static bool waitForEndOfScene;
	int swipeInput; // 0=right; 1=left; 2=up; 3=down; 4=tap;
	public static bool collision; // for the last field no collision
	public static bool explode; // for explosion of the last field
	static bool showPathBool;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	IEnumerator Start () {
		Fade.StartFadeIn (2.0f);

		InputManager.active = false;
		collision = true;
		isRotate = false;
		waitForFunction = false;
		waitForEndOfScene = false;
		explode = false;
		showPathBool = true;
		swipeInput = -1;

		// for loading new scene with loadingbar
		loadingBar = GameObject.Find("Loading");
		loadingBar.SetActive (false);

		introText = GameObject.Find ("IntroText");
		introText.GetComponent<Text> ().text = "";

		swipeIcon = GameObject.Find ("swipeIcon");
		swipeIcon.SetActive (false);

		cam = Camera.main;
		cam.gameObject.AddComponent <CameraPositionIntro>();

		BackgroundManager.loadSkybox (cam);
		BackgroundManager.setParticleSystem (cam);

		initLight ();

		// set pos
//		pos= new Vector2(PlayerPrefs.GetInt(LevelManager.NEXTLEVEL,0), PlayerPrefs.GetInt(LevelManager.NEXTWORLD,0));
		pos= new Vector2(0.0f,0.0f);

		loadPlayer ();

		initPlayer ();


		// start directly the intro
		StartCoroutine (tutorialProcess ());
		while(waitForEndOfScene)
			yield return new WaitForSeconds(0.1f);

		Fade.StartFadeOut (2.0f, cam);
		loadLevelScene ();

		yield return null;

	}

	// Update is called once per frame
	void Update () {
		
		if (swipeInput==0 && !isRotate) {
			swipeInput = -1;
			rotatePlayer (1.0f,0.0f); // to right
		}
		if (swipeInput==1 && !isRotate) {
			swipeInput = -1;
			rotatePlayer (-1.0f,0.0f); // to left
		}
		if (swipeInput==2 && !isRotate) {
			swipeInput = -1;
			rotatePlayer (0.0f,1.0f); // to up
		}
		if (swipeInput==3 && !isRotate) {
			swipeInput = -1;
			rotatePlayer (0.0f,-1.0f); // to down
		}
		if (swipeInput==4 && !isRotate) {// tap 
			swipeInput = -1;
			//tap 
		}

	}
		

	void FixedUpdate() {
		rotation ();
	}


	// the whole intro is here ################ start ##################
	IEnumerator tutorialProcess(){
		waitForEndOfScene = true;

		//show the intro description
		StartCoroutine (description ());
		while(waitForFunction)
			yield return new WaitForSeconds(0.1f);
		
		cam.GetComponent<CameraPositionIntro> ().startTransition ();
//		gameName.SetActive (false);

		//show fields
		setField (2,3);

		yield return new WaitForSeconds (1.0f);


		float waitingTimeBetweenInputs = 0.5f;

		//show the path to follow if you tap
		//1. swipe tap ____________________________________________________
		introText.GetComponent<Text> ().text = "Tap to start the game ...";
		swipeIcon.SetActive (true);
		swipeIcon.GetComponent<Image> ().sprite=swipeTap;
		StartCoroutine (fadeInGameobject(swipeIcon,1.0f));
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (getClickTouchInput()) { 
				CameraPositionIntro.CamID = 1;

				playerobj.GetComponent<Rigidbody> ().useGravity = true;

				swipeInput=4;// tap
				StartCoroutine (fadeOutGameobject(swipeIcon,1.0f));
				StartCoroutine (fadeOutText(introText,1.5f));

				while(showPathBool)
					yield return new WaitForSeconds(0.1f);
				break;
			}
			yield return null;
		}


		introText.GetComponent<Text> ().text = "Swipe right ...";
		//1. first swipe to right:____________________________________________________
		swipeIcon.GetComponent<Image> ().sprite=swipeRight;
		StartCoroutine (fadeInGameobject(swipeIcon,2.0f));
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (SwipeManager.IsSwipingRight ()) {
				StartCoroutine (fadeOutGameobject(swipeIcon,2.0f));
				StartCoroutine (fadeOutText(introText,1.5f));
				swipeInput=0;// swipe to right
				break;
			}
			yield return null;
		}

		yield return new WaitForSeconds (waitingTimeBetweenInputs);


		introText.GetComponent<Text> ().text = "Swipe up ...";
		//2. swipe up____________________________________________________
		swipeIcon.GetComponent<Image> ().sprite=swipeUp;
		StartCoroutine (fadeInGameobject(swipeIcon,2.0f));
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (SwipeManager.IsSwipingUp ()) {
				StartCoroutine (fadeOutGameobject(swipeIcon,1.0f));
				StartCoroutine (fadeOutText(introText,1.5f));
				swipeInput=2;// swipe up
				break;
			}
			yield return null;
		}

		yield return new WaitForSeconds (waitingTimeBetweenInputs);


		IntroGameController.blue = true;
		//3. swipe tap____________________________________________________
		introText.GetComponent<Text> ().text = "Tap to change the color ...";
		swipeIcon.GetComponent<Image> ().sprite=swipeTap;
		StartCoroutine (fadeInGameobject(swipeIcon,1.0f));
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (getClickTouchInput()) { 
				swipeInput=4;// tap
				playerobj.GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.BLAU);
				StartCoroutine (fadeOutGameobject(swipeIcon,1.0f));
				StartCoroutine (fadeOutText(introText,1.5f));
				yield return new WaitForSeconds(0.5f);
				break;
			}
			yield return null;
		}


		introText.GetComponent<Text> ().text = "Swipe up ...";
		StartCoroutine (fadeInText(introText,1.5f));
		//4. swipe up____________________________________________________
		swipeIcon.GetComponent<Image> ().sprite=swipeUp;
		StartCoroutine (fadeInGameobject(swipeIcon,1.0f));
		while (true){
			if (SwipeManager.IsSwipingUp ()) {
				StartCoroutine (fadeOutGameobject(swipeIcon,1.0f));
				StartCoroutine (fadeOutText(introText,1.5f));
				swipeInput=2;// swipe up
				break;
			}
			yield return null;
		}

		yield return new WaitForSeconds (waitingTimeBetweenInputs);


		introText.GetComponent<Text> ().text = "Swipe left ...";
		StartCoroutine (fadeInText(introText,1.5f));
		//4. swipe to the left____________________________________________________
		swipeIcon.GetComponent<Image> ().sprite=swipeLeft;
		StartCoroutine (fadeInGameobject(swipeIcon,1.0f));
		while (true){
			if (SwipeManager.IsSwipingLeft ()) {
				StartCoroutine (fadeOutGameobject(swipeIcon,1.0f));
				StartCoroutine (fadeOutText(introText,1.5f));
				swipeInput=1;// swipe to left
				break;
			}
			yield return null;
		}
			
		yield return new WaitForSeconds (waitingTimeBetweenInputs);

		collision = false; // no collision for the last one


		introText.GetComponent<Text> ().text = "Swipe down ...";
		StartCoroutine (fadeInText(introText,1.5f));
		//5. swipe down____________________________________________________
		swipeIcon.GetComponent<Image> ().sprite=swipeDown;
		StartCoroutine (fadeInGameobject(swipeIcon,1.0f));
		while (true){
			if (SwipeManager.IsSwipingDown ()) {
				StartCoroutine (fadeOutGameobject(swipeIcon,1.0f));
				StartCoroutine (fadeOutText(introText,1.5f));
				swipeInput=3;// swipe down
				break;
			}
			yield return null;
		}
			
		cam.GetComponent<CameraPositionIntro> ().setToFollowPlayerByRotation ();

		yield return new WaitForSeconds (2.0f);

		waitForEndOfScene = false;

		LevelPlayerController.showID=9;

		yield return null;
	}
		



	// help functions:__ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _

	// describing the game:
	IEnumerator description(){
		waitForFunction = true;

		yield return new WaitForSeconds (1.0f);

		introText.GetComponent<Text> ().text = "Welcome to ...";
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (getClickTouchInput()) { 
				StartCoroutine (fadeOutText(introText,1.5f));
				yield return new WaitForSeconds (1.0f);
				break;
			}
			yield return null;
		}

		introText.GetComponent<Text> ().text = "This game is about to keep the way in mind...";
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (getClickTouchInput()) { 
				StartCoroutine (fadeOutText(introText,1.5f));
				yield return new WaitForSeconds (1.0f);
				break;
			}
			yield return null;
		}


		introText.GetComponent<Text> ().text = "... are you ready for a short warm up?";
		StartCoroutine (fadeInText(introText,1.5f));
		while (true){
			if (getClickTouchInput()) { 
				StartCoroutine (fadeOutText(introText,1.5f));
				yield return new WaitForSeconds (1.0f);
				break;
			}
			yield return null;
		}

		waitForFunction = false;
		yield return null;
	}

	public static void startShowPath(){
		instance.StartCoroutine(showPath());
	}
	// show the path
	static IEnumerator showPath(){
		waitForFunction = true;
		float waitTime = 1.0f;
		fields= new GameObject[5];
		fields[0] = GameObject.Find ("field0_0");
		fields[0].GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.GRUEN);
	
		yield return new WaitForSeconds (waitTime);

		fields[1] = GameObject.Find ("field1_0");
		fields[1].GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.GRUEN);

		yield return new WaitForSeconds (waitTime);

		fields[2] = GameObject.Find ("field1_1");
		fields[2].GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.GRUEN);

		yield return new WaitForSeconds (waitTime);

		fields[3] = GameObject.Find ("field1_2");
		fields[3].GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.BLAU);

		yield return new WaitForSeconds (waitTime);

		fields[4] = GameObject.Find ("field0_2");
		fields[4].GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.BLAU);

		yield return new WaitForSeconds (waitTime);

		for (int i = fields.Length-1; i >0 ; i--) {
			fields[i].GetComponent<MeshRenderer> ().material.SetColor ("_Color", Col.WEISS);
			yield return new WaitForSeconds (0.5f);
		}
		waitForFunction = false;
		showPathBool = false;
		yield return null;
	}

	// just by adding the progressbar it should work
	private void loadLevelScene(){ 
		loadingBar.SetActive(true);
	}


	private void setField(int width, int height){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				field=(GameObject)Instantiate(Resources.Load("platte0"));
				field.transform.localScale = new Vector3 (0.4f, 0.4f, 0.05f);
				field.name = "field"+i+"_"+j;
				field.tag = "Field";
				field.AddComponent<MeshRenderer> ().material = Materials.glanz;
				field.GetComponent<MeshRenderer> ().material.SetColor("_Color", Col.STANDARDFIELDCOLOR);
				field.transform.position = new Vector3 (i, 4.0f, j); // 9.2
			}
		}
	}

	private void loadPlayer(){
		//Create Player
		radius = Mathf.Sqrt (2f) / 2f;

		playerobj = (GameObject)Instantiate(Resources.Load("cube0"));
		playerobj.AddComponent<MeshRenderer> ().material = Materials.glanz;
		playerobj.GetComponent<MeshRenderer>().material.SetColor("_Color",Col.WEISS);
		playerobj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
//		playerobj.transform.transform.position = new Vector3 ();
		playerobj.transform.rotation = Quaternion.Euler(0, 0, 0);
		playerobj.AddComponent<IntroGameController> ();
	}
	private void initPlayer(){
		playerobj.GetComponent<Rigidbody>().useGravity=false;

		playerobj.transform.position =  new Vector3(pos.x, 1.39f + splashOffset, pos.y*2); 

		gameName = (GameObject)Instantiate(Resources.Load("gameName"));
		gameName.GetComponent<Rigidbody>().useGravity=false;

		gameName.transform.position = new Vector3 (pos.x-1.25f,1.39f + splashOffset+0.4f,pos.y*2);
		gameName.AddComponent<Splash> ();
		gameName.SetActive (true);

		cam.transform.position =new Vector3 (0.0f, 1.39f + splashOffset, pos.y*2-5);
		cam.transform.rotation= Quaternion.Euler(0.0f, 0.0f, 0.0f);
	}
	void initLight(){
		cam.gameObject.AddComponent<Light> ();
		cam.GetComponent<Light> ().type = LightType.Directional;
		cam.GetComponent<Light> ().transform.eulerAngles= new Vector3 (200.0f,140.0f,15.0f);

		cam.GetComponent<Light> ().intensity = 0.8f;
	}



	private void rotatePlayer(float x, float y){
		directionX = -x;															
		directionZ = y;																
		startPos = playerobj.transform.position;												
		fromRotation = playerobj.transform.rotation;											
		playerobj.transform.Rotate (directionZ * 90, 0, directionX * 90, Space.World);		
		toRotation = playerobj.transform.rotation;											
		playerobj.transform.rotation = fromRotation;											
		rotationTime = 0;															
		isRotate = true;
	}

	private void rotation(){
		if (isRotate) {
			rotationTime += Time.fixedDeltaTime;									
			float ratio = Mathf.Lerp (0, 1, rotationTime / rotationPeriod);			

			float thetaRad = Mathf.Lerp (0, Mathf.PI / 2f, ratio);					
			float distanceX = -directionX * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));		
			float distanceY = radius * (Mathf.Sin (45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin (45f * Mathf.Deg2Rad));						
			float distanceZ = directionZ * radius * (Mathf.Cos (45f * Mathf.Deg2Rad) - Mathf.Cos (45f * Mathf.Deg2Rad + thetaRad));			
			playerobj.transform.position = new Vector3 (startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);						
			playerobj.transform.rotation = Quaternion.Lerp (fromRotation, toRotation, ratio);	

			if (ratio == 1) {
				isRotate = false;
				directionX = 0;
				directionZ = 0;
				rotationTime = 0;
			}
		}
	}

	// ausblenden Image
	IEnumerator fadeOutGameobject(GameObject inp,float fadeTime){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		Color tmp = inp.GetComponent<Image> ().color;
		while (progress<1.0f) {
			tmp.a=Mathf.Lerp (1, 0, progress);
			inp.GetComponent<Image> ().color = tmp;
			progress += rate * Time.deltaTime;
			yield return null;
		}
	}

	// einblenden Image
	IEnumerator fadeInGameobject(GameObject inp,float fadeTime){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		Color tmp = inp.GetComponent<Image> ().color;
		while (progress<1.0f) {
			tmp.a=Mathf.Lerp (0,1, progress);
			inp.GetComponent<Image> ().color = tmp;
			progress += rate * Time.deltaTime;
			yield return null;
		}
	}

	// ausblenden Text
	IEnumerator fadeOutText(GameObject inp,float fadeTime){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		Color tmp = inp.GetComponent<Text> ().color;
		while (progress<1.0f) {
			tmp.a=Mathf.Lerp (1, 0, progress);
			inp.GetComponent<Text> ().color = tmp;
			progress += rate * Time.deltaTime;
			yield return null;
		}
	}
	// einblenden Text
	IEnumerator fadeInText(GameObject inp,float fadeTime){
		float rate = 1.5f/fadeTime;
		float progress = 0.0f;
		Color tmp = inp.GetComponent<Text> ().color;
		while (progress<1.0f) {
			tmp.a=Mathf.Lerp (0,1, progress);
			inp.GetComponent<Text> ().color = tmp;
			progress += rate * Time.deltaTime;
			yield return null;
		}
	}

	public static bool getClickTouchInput(){
		#if UNITY_ANDROID || UNITY_IPHONE
		if(Input.touchCount > 0){
			if (Input.touches[0].phase == TouchPhase.Ended) {
				DebugConsole.Log("Touched");
				return true;
			}else{
				return false;
			}
		}

		#endif

		if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) {
			return true;
		} else {
			return false;
		}
	}


	public static void fractureCube(float scaling, GameObject field){
		GameObject fracture1=null;

		explode=true;

		for (int numFrac = 0; numFrac < field.transform.localScale.x/scaling+1; numFrac++) {
			for (int yAxis = 0; yAxis < field.transform.localScale.y / scaling+1; yAxis++) {
				fracture1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
				fracture1.transform.position = new Vector3(
					field.transform.position.x+(float)numFrac*scaling-field.transform.localScale.x/2,
					field.transform.position.y,
					field.transform.position.z-field.transform.localScale.z/2+yAxis*scaling);
				fracture1.transform.localScale= new Vector3 (scaling+Random.Range(-0.02f,0.2f), 0.1f, scaling+Random.Range(-0.02f,0.2f));
				fracture1.AddComponent<Rigidbody> ();
				fracture1.GetComponent<Rigidbody> ().mass = 0.0001f;
				fracture1.GetComponent<Rigidbody> ().useGravity = true;
				fracture1.name = "fracture"+numFrac+yAxis;
			}
		} 
		field.SetActive (false);
	}
}
