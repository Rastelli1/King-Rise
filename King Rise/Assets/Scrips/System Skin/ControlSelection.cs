using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlSelection : MonoBehaviour
{
    [System.Serializable]
    public class ButtonInfo
    {
        public Button button;              // El botón
        public GameObject childObject;     // La imagen hija
    }

    public ButtonInfo[] buttons;           // Array de botones


    void Start()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            //  asegurarse de que las imágenes hijas estén desactivadas
            if (buttons[i].childObject != null)
            {
                buttons[i].childObject.SetActive(false);
            }

            // Configurar los eventos del botón
            int index = i; // Necesario para capturar el índice correctamente

            // Añadir los eventos de hover para el botón
            EventTrigger trigger = buttons[i].button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => { OnPointerEnter(index); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => { OnPointerExit(index); });
            trigger.triggers.Add(entryExit);
        }
    }

    public void OnPointerEnter(int index)
    {
        // Mostrar la imagen hija y cambiar el color del botón
        if (buttons[index].childObject != null)
        {
            buttons[index].childObject.SetActive(true);
        }

        var colors = buttons[index].button.colors;
        buttons[index].button.colors = colors;
    }

    public void OnPointerExit(int index)
    {
        // Ocultar la imagen hija y restaurar el color original del botón
        if (buttons[index].childObject != null)
        {
            buttons[index].childObject.SetActive(false);
        }

        var colors = buttons[index].button.colors;
        buttons[index].button.colors = colors;
    }
}
