using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class OnKeyPress : MonoBehaviour
{

    [SerializeField] private UnityEvent[] events;

    void OnInteract(InputValue value) {
        if (value.Get<float>() > 0)
        {
            foreach (UnityEvent action in events)
            {
                action.Invoke();
            }
        }
    }

}
