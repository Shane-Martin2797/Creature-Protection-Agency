using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour {

    public Image oneCubsImage;
    public Image twoCubsImage;
    public Image oneParentsTwoCubsImage;
    public Image twoParentsTwoCubsImage;
    public Image fullWinImage;

	// Use this for initialization
	void Start () 
    {
        switch (PlayerController.Instance.creatureList.Count)
        {
            case 0:
                oneCubsImage.enabled = false;
                twoCubsImage.enabled = false;
                twoParentsTwoCubsImage.enabled = true;
                fullWinImage.enabled = false;
                break;
            case 1:
                oneCubsImage.enabled = false;
                twoCubsImage.enabled = true;
                twoParentsTwoCubsImage.enabled = false;
                fullWinImage.enabled = false;
                break;
            case 2:
                oneCubsImage.enabled = false;
                twoCubsImage.enabled = true;
                twoParentsTwoCubsImage.enabled = false;
                fullWinImage.enabled = false;
                break;
            case 3:
                oneCubsImage.enabled = true;
                twoCubsImage.enabled = false;
                twoParentsTwoCubsImage.enabled = false;
                fullWinImage.enabled = false;
                break;
            case 4:
                oneCubsImage.enabled = false;
                twoCubsImage.enabled = false;
                twoParentsTwoCubsImage.enabled = false;
                fullWinImage.enabled = true;
                break;
        }
	}
}
