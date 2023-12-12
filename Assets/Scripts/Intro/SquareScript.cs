using UnityEngine;

public class SquareScript : MonoBehaviour
{
    [SerializeField] private Transform bearPlace;
    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float deltax, deltay;
    public static bool locked;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
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

        if (Mathf.Abs(mousePosition.x - bearPlace.position.x) <= 0.5f &&
            Mathf.Abs(mousePosition.y - bearPlace.position.y) <= 0.5f)
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
            bearPlace.transform.position = new Vector2(mousePosition.x - deltax, mousePosition.y - deltay);
        }
    }

    void OnMouseUp()
    {
        locked=false;  
    }
}
