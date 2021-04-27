using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagButtonTemp : MonoBehaviour
{
    public GameObject btn1, btn2;

    public void Btn1()
    {
        btn1.SetActive(true);
        btn2.SetActive(false);
    }
    public void Btn2()
    {
        btn1.SetActive(false);
        btn2.SetActive(true);
    }

}
