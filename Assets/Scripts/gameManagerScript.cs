using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    GameObject target;
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        if (!target)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void startScene(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }
}
