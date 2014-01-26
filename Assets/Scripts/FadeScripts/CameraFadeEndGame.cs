﻿
// simple fading script
// A texture is stretched over the entire screen. The color of the pixel is set each frame until it reaches its target color.

//Found on Wiki Unity
using UnityEngine;


public class CameraFadeEndGame : MonoBehaviour
{   
	private GUIStyle m_BackgroundStyle = new GUIStyle();		// Style for background tiling
	private Texture2D m_FadeTexture;				// 1x1 pixel texture used for fading
	private Color m_CurrentScreenOverlayColor = new Color(0,0,0,0);	// default starting color: black and fully transparrent
	private Color m_TargetScreenOverlayColor = new Color(0,0,0,0);	// default target color: black and fully transparrent
	private Color m_DeltaColor = new Color(0,0,0,0);		// the delta-color is basically the "speed / second" at which the current color should change
	private int m_FadeGUIDepth = -1000;				// make sure this texture is drawn on top of everything
	public bool isFading = false;
	public float times = 0;
	
	// initialize the texture, background-style and initial color:
	private void Awake()
	{		
		m_FadeTexture = new Texture2D(1, 1);        
		m_BackgroundStyle.normal.background = m_FadeTexture;
		SetScreenOverlayColor(m_CurrentScreenOverlayColor);
		
		// TEMP:
		// usage: use "SetScreenOverlayColor" to set the initial color, then use "StartFade" to set the desired color & fade duration and start the fade
		//SetScreenOverlayColor(new Color(0,0,0,1));
		//StartFade(new Color(1,0,0,1), 5);
	}

	 void Update(){
		if(isFading == true){
			times += Time.deltaTime;
		}
		//Debug.Log("duh");
		if(times > 5.0f){
			Debug.Log ("WE HAVE ARRIVED");
			Application.Quit ();
		}
	}
	
	// draw the texture and perform the fade:
	private void OnGUI()
	{   
		// if the current color of the screen is not equal ato the desired color: keep fading!
		if (m_CurrentScreenOverlayColor != m_TargetScreenOverlayColor)
		{			
			// if the difference between the current alpha and the desired alpha is smaller than delta-alpha * deltaTime, then we're pretty much done fading:
			if (Mathf.Abs(m_CurrentScreenOverlayColor.a - m_TargetScreenOverlayColor.a) < Mathf.Abs(m_DeltaColor.a) * Time.deltaTime)
			{
				m_CurrentScreenOverlayColor = m_TargetScreenOverlayColor;
				SetScreenOverlayColor(m_CurrentScreenOverlayColor);
				m_DeltaColor = new Color(0,0,0,0);
			}
			else
			{
				// fade!
				SetScreenOverlayColor(m_CurrentScreenOverlayColor + m_DeltaColor * Time.deltaTime);
			}
		}
		
		// only draw the texture when the alpha value is greater than 0:
		if (m_CurrentScreenOverlayColor.a > 0)
		{			
			GUI.depth = m_FadeGUIDepth;
			GUI.Label(new Rect(-10, -10, Screen.width + 10, Screen.height + 10), m_FadeTexture, m_BackgroundStyle);
		}
	}
	
	
	// instantly set the current color of the screen-texture to "newScreenOverlayColor"
	// can be usefull if you want to start a scene fully black and then fade to opague
	public void SetScreenOverlayColor(Color newScreenOverlayColor)
	{
		m_CurrentScreenOverlayColor = newScreenOverlayColor;
		m_FadeTexture.SetPixel(0, 0, m_CurrentScreenOverlayColor);
		m_FadeTexture.Apply();
	}
	
	
	// initiate a fade from the current screen color (set using "SetScreenOverlayColor") towards "newScreenOverlayColor" taking "fadeDuration" seconds
	public void StartFade(Color newScreenOverlayColor, float fadeDuration)
	{
		if (fadeDuration <= 0.0f)		// can't have a fade last -2455.05 seconds!
		{
			SetScreenOverlayColor(newScreenOverlayColor);
		}
		else					// initiate the fade: set the target-color and the delta-color
		{
			m_TargetScreenOverlayColor = newScreenOverlayColor;
			m_DeltaColor = (m_TargetScreenOverlayColor - m_CurrentScreenOverlayColor) / fadeDuration;
		}

	}

	public void OnTriggerEnter(Collider other){

		if(other.gameObject.tag == "Player"){
			Debug.Log ("THE THING");
			StartFade (Color.black, 10.0f);
			isFading = true;
			PlayerPrefs.SetFloat ("PlayerX", GameObject.Find("spawn").transform.position.x);
			PlayerPrefs.SetFloat ("PlayerY", GameObject.Find("spawn").transform.position.y);
			PlayerPrefs.SetFloat ("PlayerZ", GameObject.Find("spawn").transform.position.z);
			PlayerPrefs.SetString("isDead", "false");
		}
		
	}
}