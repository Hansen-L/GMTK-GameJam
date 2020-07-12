using UnityEngine;
using TMPro;

public class ContinueTextUI : MonoBehaviour 
{
    public TextMeshProUGUI continueText;
    public GameManager gameManager;

    void Start() 
    {
        continueText = this.GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();


        if (gameManager.stage == 5)
        {
            continueText.text = "Congrats! You have finished the game. Thanks for playing! :)";
        }
        else if (gameManager.stage == 6)
        {
            continueText.text = "Things are going to get serious now.";
        }
        else if (gameManager.stage == 7)
        {
            continueText.text = "You're still here?";
        }
        else if (gameManager.stage == 8)
        {
            continueText.text = "I desperately need sleep. Please help me.";
        }
        else if (gameManager.stage == 9)
        {
            continueText.text = "Wake me from my hibernation for GMTK 2021...";
        }
        else if (gameManager.stage == 10)
        {
            continueText.text = "Where did the sheep get a hair cut? At the baaah-baaah shop.";
        }
        else if (gameManager.stage == 11)
        {
            continueText.text = "What is a sheep's favorite car? A Lamb-orghini!";
        }
        else if (gameManager.stage == 12)
        {
            continueText.text = "Ok see you next year friends!";
        }
        else
        {
            continueText.text = "";
        }
    }
}
