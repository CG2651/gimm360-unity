using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Prototype");
    }
}
