using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public float tutorialTextTimer = 3f;
    float tut_timer;
    bool game_over = false;


    GameObject flag_counter;
    GameObject resetButton;
    GameObject centerText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flag_counter = transform.GetChild(0).gameObject;
        resetButton = transform.GetChild(1).gameObject;
        centerText = transform.GetChild(2).gameObject;

        resetButton.SetActive(false);

        centerText.GetComponent<TextMeshProUGUI>().SetText("Collect 3 flags to win!");
        tut_timer = tutorialTextTimer;  
    }

    // Update is called once per frame
    void Update()
    {
        if (!game_over)
        {
            if (tut_timer <= 0f) centerText.SetActive(false);
            else tut_timer -= Time.deltaTime;
        }
    }


    public void SetFlagCount(int flag_count)
    {
        flag_counter.GetComponent<TextMeshProUGUI>().SetText("Flags: " + flag_count.ToString());
    }


    public void EndGame(bool win)
    {
        Cursor.visible = true;
        resetButton.SetActive(true);
        centerText.SetActive(true);
        game_over = true;

        if (win)
        {
            centerText.GetComponent<TextMeshProUGUI>().SetText("You Win!");
        }
        else
        {
            centerText.GetComponent<TextMeshProUGUI>().SetText("You Lose!"); 
        }
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
