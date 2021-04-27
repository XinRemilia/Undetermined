using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDoorCheck : MonoBehaviour
{
    public SpriteRenderer doorUpSprite;
    public SpriteRenderer doorDownSprite;
    private bool flag = true;

    private void FixedUpdate()
    {
        if(!flag && Input.GetKeyDown(KeyCode.Z))
        {
            doorUpSprite.enabled = flag;
            doorDownSprite.enabled = flag;
        }
    }

    void Update()
    {
        //if (sign.activeSelf && Input.GetKeyDown(KeyCode.E))
        //{
        //    talkUI.SetActive(true);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            flag = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            flag = true;
            doorUpSprite.enabled = true;
            doorDownSprite.enabled = true;
        }
    }
}
