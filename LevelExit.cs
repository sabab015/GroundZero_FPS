using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    public string nextLevel;

    public float waitEndLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            GameManager.instance.levelEnding = true;
            StartCoroutine(EndLevelCo());
            
        }
    }

    private IEnumerator EndLevelCo()
    {

        PlayerPrefs.SetString("CurrentLevel ", nextLevel);

        yield return new WaitForSeconds(waitEndLevel);

        SceneManager.LoadScene(nextLevel);
    }
}
