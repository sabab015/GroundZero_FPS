using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public string mainmenuScene;

    public float timeBetweenShowing = 1f;

    public GameObject textBox, textbox1, returnButton;

    public Image blackScreen;

    public float blackScreenfade = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showObjectCo());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenfade * Time.deltaTime));
    }

    public void mainmenu()
    {
        SceneManager.LoadScene(mainmenuScene);
    }

    public IEnumerator showObjectCo()
    {
        yield return new WaitForSeconds(timeBetweenShowing);

        textBox.SetActive(true);
        
        yield return new WaitForSeconds(timeBetweenShowing);
        
        textbox1.SetActive(true);

        yield return new WaitForSeconds(timeBetweenShowing);

        returnButton.SetActive(true);
    }
}
