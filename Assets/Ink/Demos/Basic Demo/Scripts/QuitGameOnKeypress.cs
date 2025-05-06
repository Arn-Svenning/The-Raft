using UnityEngine;

public class QuitGameOnKeypress : MonoBehaviour {
	
	public KeyCode key = KeyCode.Escape;
	
	void Update () {
		if(UnityEngine.Input.GetKeyDown(key)) Application.Quit();
	}
}