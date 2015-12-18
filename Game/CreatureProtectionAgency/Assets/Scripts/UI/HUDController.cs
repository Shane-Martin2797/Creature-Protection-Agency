using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour 
{
    public Text timerText;
    private bool gameNotNull = false;
    private bool playerNotNull = false;

    //public Image baitTimerImage;
    public Image dartTimerImage;

    public Image tiger0;
    public Image tiger1;
    public Image tiger2;
    public Image tiger3;

    public void UpdateHUDTigers()
    {
        if (playerNotNull)
        {
            switch (PlayerController.Instance.creatureList.Count)
            {
                case 0:
                    tiger0.enabled = true;
                    tiger1.enabled = true;
                    tiger2.enabled = true;
                    tiger3.enabled = true;
                    break;
                case 1:
                    tiger0.enabled = true;
                    tiger1.enabled = true;
                    tiger2.enabled = true;
                    tiger3.enabled = true;
                    break;
                case 2:
                    tiger0.enabled = true;
                    tiger1.enabled = true;
                    tiger2.enabled = true;
                    tiger3.enabled = false;
                    break;
                case 3:
                    tiger0.enabled = true;
                    tiger1.enabled = true;
                    tiger2.enabled = false;
                    tiger3.enabled = false;
                    break;
                case 4:
                    tiger0.enabled = true;
                    tiger1.enabled = false;
                    tiger2.enabled = false;
                    tiger3.enabled = false;
                    break;
                default:
                    Debug.LogError("Value returned by creatureList.Count is out of range");
                    break;
            }
        }
    }

    void Start ()
    {
        {
            tiger0.enabled = false;
            tiger1.enabled = false;
            tiger2.enabled = false;
            tiger3.enabled = false;
        }

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
            //baitTimerImage.fillAmount = PlayerController.Instance.UpdateHUDBait();
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
