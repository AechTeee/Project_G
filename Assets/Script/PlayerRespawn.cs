using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHeath;
    private UIManager uiManager;

    private void Awake()
    {
        playerHeath = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }
    public void CheckRespawn()
    {   
        if (currentCheckpoint == null)
        {
            uiManager.GameOver();
            return;
        }
        playerHeath.Respawn();
        transform.position = currentCheckpoint.position;
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);

    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
    */

}
