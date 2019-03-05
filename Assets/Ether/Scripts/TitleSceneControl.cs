using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneControl : MonoBehaviour {
   
	private GUIStyle guiStyle;
    private GUIStyleState styleState;

    public string version = "Version_0.2.0";
    public int gameWindowWidth = 414;
    public int gameWindowHeight = 896;


    void Awake()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.OSXPlayer ||
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(gameWindowWidth, gameWindowHeight, false);
        }
    }

    void Start ()
    {
        guiStyle = new GUIStyle();
        styleState = new GUIStyleState();

        guiStyle.fontSize = 48;
        guiStyle.normal = styleState;
        styleState.textColor = Color.black;
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("2_Swipe");
        }
    }

	void OnGUI()
	{
        GUI.Label(new Rect(800, 2500, 200, 200), version, guiStyle);
	}
}
