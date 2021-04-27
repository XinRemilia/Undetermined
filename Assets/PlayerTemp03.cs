using UnityEngine;

public class PlayerTemp03 : MonoBehaviour
{

    public SpriteRenderer xx;
    public Sprite sp01, sp02;
    public bool flag = true;
    public GameObject kasa;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            flag = !flag;
        }
        kasa.SetActive(!flag);
        if (flag)
        {
            xx.sprite = sp01;
        }
        else
        {
            xx.sprite = sp02;
          
        }
    }
}
