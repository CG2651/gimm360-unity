using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PackageDelivered : MonoBehaviour
{
    public AudioSource sound;
    public TMP_Text text;
    private int packagesDelivered = 0;
    private int totalPackages = 7;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Alien"))
        {
            packagesDelivered++;
            text.text = "Pizza Delivered: " + packagesDelivered + " / 7";

            if (packagesDelivered == totalPackages)
            {
                SceneManager.LoadScene("End Screen");
                //Debug.Log("Go to ending");
            }
            else
            {
                sound.Play();
                StartCoroutine(waitDestroy());
                Destroy(other.gameObject);
                //Debug.Log("Go to ending");
            }
        }
    }

    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(1f);
    }
}
