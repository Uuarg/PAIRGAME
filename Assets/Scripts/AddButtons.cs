using UnityEngine;
using System.Collections;
public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;
    public int count = 128;


    void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = i.ToString();
            button.transform.SetParent(puzzleField, false);
        }
    }

}
