using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class cDialogSystem : MonoBehaviour
{
    [Header("UIコントローラー")]
    public Text textLabel;
    public Image faceImage;

    [Header("TXTファイル")]
    public TextAsset textFile;
    public int index;

    public Sprite face01, face02;
    List<string> textList = new List<string>();
    // Start is called before the first frame update
    void Awake()
    {
        GetTextFormFile(textFile);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        index = 0;
        ExpressSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ExpressSet();
        }

        if (index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Replace("\r","") .Split('\n');
        //Encoding encoding = Encoding.GetEncoding("ASCII");
        //string str =  encoding.GetString(file.bytes);

        foreach (var line in lineData)
        {
            //var tempLine = encoding.GetBytes(line);
            //textList.Add(encoding.GetString(tempLine));
            textList.Add(line);
        }
    }

    void ExpressSet()
    {
        switch (textList[index])
        {
            case "A":
                faceImage.sprite = face01;
                break;
            case "B":
                faceImage.sprite = face02;
                break;
        }
        index++;
        textLabel.text = textList[index];
        index++;
    }
}
