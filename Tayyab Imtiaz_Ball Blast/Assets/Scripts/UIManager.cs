using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private PlayerController playerScript;
    public Text scoreText;
    public Text healthText;
    public Text gameOver;
    public Button reloadButton;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerController>();
        reloadButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScript.score.ToString();
        healthText.text = "Health: " + playerScript.health.ToString();
        if(playerScript.gameOver == true)
        {
            reloadButton.gameObject.SetActive(true);
            gameOver.text = "!!! Game Over !!!";
        }
    }
}
