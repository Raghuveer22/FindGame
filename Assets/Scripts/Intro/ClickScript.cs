using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClickScript : MonoBehaviour
{
    private LevelManager levelManager;
    private TextMeshProUGUI textUI;
    [SerializeField] 
    private int currentIndex=0; 
    [SerializeField] 
    private int dragCount;
    Vector2 difference=Vector2.zero;
    private List<string> Sentences;
    public static bool locked;
    [SerializeField]
    private bool draggable=false;
    private Vector2 mousePosition;

    private float deltax, deltay;
    // Start is called before the first frame update

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Sentences = levelManager.rectSentences;
        dragCount=levelManager.dragOffset;
        textUI=levelManager.textToChange;
    }


    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse click is over the RectTransform
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePosition, Camera.main, out Vector2 localPoint);

            if (rectTransform.rect.Contains(localPoint))
            {
                // Increment the click count
                ChangeText();
            }
        }
        if(draggable)
        {
            Drag();
        }
    }
    private void ChangeText()
    {
        if(currentIndex<Sentences.Count)
        {
            textUI.text=Sentences[currentIndex];
            currentIndex++;
            if(currentIndex==dragCount)
            {
                //and enable the mouse dragging means bigBrain take a bool flag to flag him
                draggable=true;
            }
        }
        
    }
    private void Drag()
    {
           // Check for mouse button down
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown();
            }
            // Check for mouse button up
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseUp();
            }

            // Check for mouse drag
            if (Input.GetMouseButton(0))
            {
                OnMouseDrag();
            }
    }    
    void OnMouseDown()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(mousePosition.x - transform.position.x) <= 15.5f &&
            Mathf.Abs(mousePosition.y - transform.position.y) <= 10.5f)
        {
            locked=true;
            deltax = transform.position.x-Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            deltay = transform.position.y-Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        }
       
    }

    void OnMouseDrag()
    {
        if(locked==true)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x+deltax, mousePosition.y+deltay);
        }
    }

    void OnMouseUp()
    {
        locked=false;  
    }
}
