using UnityEngine;
using UnityEngine.UI;

public class swipe : MonoBehaviour
{
    public static swipe instance;
    public GameObject scrollbar;
    private float scroll_pos = 0;
    public float ScrollPos {get {return scroll_pos;}}
    private float[] pos;
    public float[] Pos {get {return pos;}}
    private float distance;
    public float Distance {get {return distance;}}
    public int currentIndex;

    private void Start() 
    {
        instance = this;
        pos = new float[transform.childCount]; //Get the content children length
        distance = 1f / (pos.Length - 1f); //Filter method

        // for scrollbar value, 0, 0.25, 0.50, 0.75, 1 if array length = 5
        // for scrollbar value, 0, 0.2, 0.4, 0.6, 0.8, 1 if array length = 6, etc.
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {   
        SmoothStageSelection();
    }

    private void SmoothStageSelection()
    {
        if (Input.GetMouseButton(0) || (Input.touchCount > 0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    currentIndex = i;

                    if(ShopSystem.Instance.worldsPrice[i] > 500)
                    ShopSystem.Instance.buy.enabled = false;
                    else
                    ShopSystem.Instance.buy.enabled = true;

                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
                    
                    transform.GetChild(i).localScale = Vector3.Lerp(transform.GetChild(i).localScale, new Vector3(1f, 1f, 1f), 0.1f);
                    // Debug.LogWarning("Current Selected Stage " + (i + 1));

                    for (int j = 0; j < pos.Length; j++)
                    {
                        if (j != i)
                        {
                            transform.GetChild(j).localScale = Vector3.Lerp(transform.GetChild(j).localScale, new Vector3(0.8f, 0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }
        }
    }
}