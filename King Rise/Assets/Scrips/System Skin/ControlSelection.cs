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
    public Listas skins;
    private int contadorSkins = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("contadorSkins"))
        {
            contadorSkins = 0; // Si no hay una skin seleccionada, usa la básica (primera de la lista)
        }
        else
        {
            Cargar(); // Cargar la skin previamente seleccionada
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            // Asegurarse de que las imágenes hijas estén desactivadas
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

        // Activar la skin seleccionada previamente o la básica si no hay una selección previa
        if (buttons[contadorSkins].childObject != null)
        {
            buttons[contadorSkins].childObject.SetActive(true);
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

    private void Guardar()
    {
        PlayerPrefs.SetInt("contadorSkins", contadorSkins);
    }

    private void Cargar()
    {
        contadorSkins = PlayerPrefs.GetInt("contadorSkins");
    }
}
