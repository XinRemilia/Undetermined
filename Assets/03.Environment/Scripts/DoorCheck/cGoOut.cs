using UnityEngine;

public class cGoOut : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (!transform.GetComponent<SpriteRenderer>().enabled)
            {
                //"CellInner"   "Normal"
                SpriteRenderer temp = collision.transform.GetComponent<SpriteRenderer>();
                temp.sortingLayerName = temp.sortingLayerName.Equals("CellInner") ? "Normal" : "CellInner";
            }
        }
    }
}
