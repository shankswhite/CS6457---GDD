using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]

public class VictoryScript : MonoBehaviour
{

    public Stamina Stamina;
    public CharacterScriptMotionController charaterController;

    // Start is called before the first frame update
    private CanvasGroup canvasGroup;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(charaterController.isWin == true)
    {   
            /*if (canvasGroup.interactable)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
            Time.timeScale = 1f;
        }*/
        if (Input.GetKey(KeyCode.X))
        {
            SceneManager.LoadScene("Title Screen - Climb and Conquer");
        }
            if(!canvasGroup.interactable)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            Time.timeScale = 0f;
        }
    }

        /*if (Stamina.isGameOverStatus() || charaterController.isWin == true)
        {
            /*if (canvasGroup.interactable)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
            Time.timeScale = 1f;
        }
            if (Input.GetKey(KeyCode.X))
            {
                SceneManager.LoadScene("Title Screen - Climb and Conquer");
            }
            if (!canvasGroup.interactable)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
            }
        }*/

    }
}
