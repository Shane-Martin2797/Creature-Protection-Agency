using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour 
{
    public Text timerText;
    private bool gameNotNull = false;

    void Start ()
    {
        if (GameController.Instance != null)
        {
            gameNotNull = true;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if(gameNotNull)
        {
            float numSeconds = GameController.Instance.timer % 60;
            int numMinutes = Mathf.FloorToInt(GameController.Instance.timer / 60);
            timerText.text = "Time Left: " + numMinutes.ToString() + ":" + numSeconds.ToString("00");
        }
	}
}
