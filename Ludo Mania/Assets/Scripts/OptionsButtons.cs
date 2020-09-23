using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OptionsButtons : MonoBehaviour
{
    Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GameObject.FindObjectOfType<Movement>();
        int num = Convert.ToInt32(gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);

        gameObject.GetComponent<Button>().onClick.AddListener(delegate { movement.move_Player(movement.selected_Button_GameObject, num); });
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { movement.destroy_Instants(); });
    }

    private void Update()
    {
        if (movement.Instants.Count > 0)
        {
            bool check = true;

            for (int counter = 0; counter < movement.Instants.Count; counter++)
            {
                if (EventSystem.current.currentSelectedGameObject == movement.Instants[counter])
                {
                    check = false;
                }
            }

            if (EventSystem.current.currentSelectedGameObject != movement.selected_Button_GameObject && check)
            {
                movement.selected_Button_GameObject = null;
                movement.destroy_Instants();
            }
        }
    }
}