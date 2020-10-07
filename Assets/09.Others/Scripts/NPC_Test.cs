using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Test : MonoBehaviour
{
    public GameObject sign;
    public GameObject talkUI;

    void Start()
    {
        sign.SetActive(false);
    }
    void Update()
    {
        if (sign.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            talkUI.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sign.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        sign.SetActive(false);
    }
}
