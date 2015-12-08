using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CutScene : MonoBehaviour {

    Image imageComp;

    public Image[] images;
    public Image radarSpinner;

    
    public float[] logoTime;

    //float[] initialLogoTime;

    void Start()
    {
        //for (int i = 0; i <= images.Length; i++)
        //{
            //imageComp = images[0].GetComponent<Image>();
           // tempColor = imageComp.color;
            //initialLogoTime[0] = logoTime[0];
       // } 
    }

    Color tempColor;
    void Update()
    {
        /*for (int i = 1; i <= images.Length; i++)
        {
            tempColor.a = Mathf.Sin(logoTime[i] / 0 * Mathf.PI);

            imageComp.color = tempColor;
        }*/ 

        logoTime[0] -= Time.deltaTime;

        if (logoTime[0] < 0)
        {
            Debug.Log("Image2");
            images[0].gameObject.SetActive(false);
            images[1].gameObject.SetActive(true);
            radarSpinner.gameObject.SetActive(true);
            logoTime[1] -= Time.deltaTime;
        }

        if (logoTime[1] < 0)
        {
            Debug.Log("Image3");
            radarSpinner.gameObject.SetActive(false);
            images[1].gameObject.SetActive(false);
            images[2].gameObject.SetActive(true);
            logoTime[2] -= Time.deltaTime;
        }

        if (logoTime[2] < 0)
        {
            Debug.Log("Image4");
            images[2].gameObject.SetActive(false);
            images[3].gameObject.SetActive(true);
            logoTime[3] -= Time.deltaTime;
        }

        if (logoTime[3] < 0)
        {
            Debug.Log("Image5");
            images[3].gameObject.SetActive(false);
            images[4].gameObject.SetActive(true);
            logoTime[4] -= Time.deltaTime;
        }

        if (logoTime[4] < 0)
        {
            Debug.Log("Image6");
            images[4].gameObject.SetActive(false);
            images[5].gameObject.SetActive(true);
            logoTime[5] -= Time.deltaTime;
        }

        if (logoTime[5] < 0)
        {
            Debug.Log("Image7");
            images[5].gameObject.SetActive(false);
            images[6].gameObject.SetActive(true);
            logoTime[6] -= Time.deltaTime;
        }

        if (logoTime[6] < 0)
        {
            Debug.Log("Change Scene");
            Application.LoadLevel("Main");
        }
    }
}
