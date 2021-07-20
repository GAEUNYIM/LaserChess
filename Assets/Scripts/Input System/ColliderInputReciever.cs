using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInputReciever : InputReciever
{
    private Vector3 clickPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.point;
                int text = 0;
                if (hit.collider.gameObject.tag == "Left")
                {
                    text = 1;
                }
                else if(hit.collider.gameObject.tag == "Right")
                {
                    text = 2;
                }
                OnInputReceived(text);
            }
        }
    }

    public override void OnInputReceived(int text)
    {
        if (text==0)
        {
            foreach (var handler in inputHandlers)
            {
                handler.ProcessInput(clickPosition, null, null);
            }

        }
        if (text == 1 || text ==2) {
            foreach (var handler in inputHandlers)
            {
                handler.ProcessInput(text);
            }
        }
        
    }

    

}
