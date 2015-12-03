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
            int numSeconds = Mathf.FloorToInt(GameController.Instance.timer % 60);
            int numMinutes = Mathf.FloorToInt(GameController.Instance.timer / 60);
            timerText.text = numMinutes.ToString() + ":" + numSeconds.ToString("00");
        }
        else if (GameController.Instance != null)
        {
            gameNotNull = true;
        }
	}
}
