using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour 
{
    public Text timerText;
    private bool gameNotNull = false;
    private bool playerNotNull = false;

    public Image baitTimerImage;
    public Image dartTimerImage;

    void Start ()
    {
        if (GameController.Instance != null)
        {
            gameNotNull = true;
        }

        if (PlayerController.Instance != null)
        {
            playerNotNull = true;
        }
        if(PlayerController.Instance.dartPrefab == null)
        {
            dartTimerImage.gameObject.SetActive(false);
            timerText.transform.parent.gameObject.SetActive(false);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (playerNotNull)
        {
            baitTimerImage.fillAmount = PlayerController.Instance.UpdateHUDBait();
            dartTimerImage.fillAmount = PlayerController.Instance.UpdateHUDDart();
        }
        else if (PlayerController.Instance != null)
        {
            playerNotNull = true;
        }
        else
        {
            Debug.LogError("PlayerController not found!!!");
        }

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
        else
        {
            Debug.LogError("GameController not found!!!");
        }
	}
}
