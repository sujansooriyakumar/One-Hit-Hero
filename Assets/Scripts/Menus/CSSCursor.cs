using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class CSSCursor : MonoBehaviour
{
    public int playerID;
    CharacterSelect css;
    GameController gc;
    Vector3 velocity;
    public int currentIndex;
    GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);
    private string currentSelection;

    private void Start()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        gc = FindObjectOfType<GameController>();
        
    }
    private void Update()
    {
        transform.position += velocity * Time.deltaTime * 200.0f;
        pointerEventData.position = transform.position;
        List<RaycastResult> results = new List<RaycastResult>();

        gr.Raycast(pointerEventData, results);
        Debug.Log(results.Count);
        if (results.Count > 1)
        {
            Debug.Log(results[1]);
            currentSelection = results[1].gameObject.name;
        }
    }


    public void CSSMoveEvent(InputAction.CallbackContext context)
    {
        velocity = context.ReadValue<Vector2>();


        
       
    }

    public void Submit(InputAction.CallbackContext context)
    {
        gc.SetCharacter(currentSelection, playerID);
    }
}
