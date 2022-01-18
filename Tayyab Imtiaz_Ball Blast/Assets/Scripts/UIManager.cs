using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerController playerScript;
    public Text scoreText;
    public Text healthText;
    public Text gameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScript.score.ToString();
        healthText.text = "Health: " + playerScript.health.ToString();
        if(playerScript.gameOver == true)
        {
            gameOver.text = "!!! Game Over !!!";
        }
    }
}
