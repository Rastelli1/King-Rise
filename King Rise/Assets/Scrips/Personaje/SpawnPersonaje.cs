using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPersonaje : MonoBehaviour
{
    public Listas listSkins;

    private int contadorSkins;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("contadorSkins"))
        {
            contadorSkins = 0;
        }
        else
        {
            Cargar();
        }
        InvocarPersonajes(contadorSkins);
    }

    private void InvocarPersonajes(int contadorSkins)
    {
        Ficha skin=listSkins.ObtenerSkins(contadorSkins);
        Instantiate(skin.object_Skins, this.transform.position, this.transform.rotation);
    }
    
    private void Cargar()
    {
        contadorSkins = PlayerPrefs.GetInt("contadorSkins");
    }
}
