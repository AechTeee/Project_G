using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHeath;
    private void Awake()
    {
        playerHeath = GetComponent<Health>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Respawn()
    {
        transform.position = currentCheckpoint.position;
    }
}
