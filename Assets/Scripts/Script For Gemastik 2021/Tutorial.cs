using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI guideText;
    public GameObject content;
    public Transform rightHand, leftHand;
    private float screenWidth;
    private float tutorialStepTime1 = 2f, tutorialStepTime2;
    private bool isTut_OnePassed, isTut_TwoPassed, isTutorialPassed;
    int i = 0;

    private void Start() {
        screenWidth = Screen.width;
        isTutorialPassed = PlayerPrefs.GetInt("TutorialPassed", 0) == 0 ? false : true;
        tutorialStepTime2 = tutorialStepTime1 / 2;
    }

    private void Update() {
        BeginnerTutorial();
    }

    private void BeginnerTutorial()
    {
        if(isTutorialPassed) return;
        
        tutorialStepTime1 -= Time.deltaTime;

        if(tutorialStepTime1 <= 0f && !isTut_OnePassed)
        {
            //Tuts Turning Right
            content.SetActive(true);
            Time.timeScale = 0f;

            leftHand.gameObject.SetActive(false);

            //Belum berjala

            guideText.text = "Tap Right of The Screen to Turn Right";

            for(int i = 0 ; i < Input.touchCount ; i++)
            {
                if(Input.GetTouch(i).position.x > screenWidth/2)
                {
                    Time.timeScale = 1f;
                    content.SetActive(false);
                    isTut_OnePassed = true;
                }
            }
        }

        if(isTut_OnePassed) tutorialStepTime2 -= Time.deltaTime;
        
        if(tutorialStepTime2 <= 0 && !isTut_TwoPassed)
        {
            //Tuts Turning Left
            content.SetActive(true);
            Time.timeScale = 0f;

            guideText.text = "Tap Left of The Screen to Turn Left";

            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(true);

            for(int i = 0 ; i < Input.touchCount ; i++)
            {
                if(Input.GetTouch(i).position.x < screenWidth/2)
                {
                    content.SetActive(false);
                    Time.timeScale = 1f;

                    isTut_TwoPassed = true;
                    isTutorialPassed = true;
                    PlayerPrefs.SetInt("TutorialPassed", 1);

                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
