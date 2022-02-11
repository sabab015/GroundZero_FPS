using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float waitAfterDying = 2f;

    [HideInInspector]

    public bool levelEnding;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void playerDied()
    {
        StartCoroutine(playerDiedCo());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public IEnumerator playerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnPause()
    {
        if(UIController.instance.pauseScreen.activeInHierarchy)
        {
            UIController.instance.pauseScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;

            Time.timeScale = 1f;
        }
        else
        {
            UIController.instance.pauseScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Cursor.visible = true;

            Time.timeScale = 0f;
        }
    }

}
