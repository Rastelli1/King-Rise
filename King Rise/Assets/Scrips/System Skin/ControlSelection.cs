using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        // Cargar la skin previamente seleccionada o usar la básica
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

            // Asignar el evento de selección al botón
            buttons[i].button.onClick.AddListener(() => SeleccionarSkin(index));

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
        buttons[contadorSkins].childObject.SetActive(false);
        // Mostrar la imagen hija y cambiar el color del botón
        if (buttons[index].childObject != null)
        {
            buttons[index].childObject.SetActive(true);
        }

        var colors = buttons[index].button.colors;
        colors.normalColor = Color.red; // Ejemplo de cambio de color
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
        colors.normalColor = Color.white; // Restaurar el color original
        buttons[index].button.colors = colors;
    }

    private void SeleccionarSkin(int index)
    {
        // Desactivar la imagen de la skin actualmente seleccionada
        if (buttons[contadorSkins].childObject != null)
        {
            buttons[contadorSkins].childObject.SetActive(false);
        }

        // Actualizar el contador de skins
        contadorSkins = index;

        // Activar la imagen de la nueva skin seleccionada
        if (buttons[contadorSkins].childObject != null)
        {
            buttons[contadorSkins].childObject.SetActive(true);
        }

        // Guardar la selección en PlayerPrefs
        Guardar();
    }

    private void Guardar()
    {
        PlayerPrefs.SetInt("contadorSkins", contadorSkins);
        ControllerScenes.instance.Pruebas();
    }

    private void Cargar()
    {
        contadorSkins = PlayerPrefs.GetInt("contadorSkins");
    }
}
