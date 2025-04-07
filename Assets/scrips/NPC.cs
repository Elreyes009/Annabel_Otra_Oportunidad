using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public TextMeshProUGUI dialogoTexto; // Referencia al texto en Unity
    public GameObject dialogoFondo; // Fondo del cuadro de di�logo

    [Header("Di�logos del NPC")]
    [TextArea(3, 10)] // Esto permite editar di�logos m�s largos en el Inspector
    public List<string> dialogos = new List<string>
    {
        "Hola viajero, bienvenido a nuestro pueblo.",
        "Dicen que hay criaturas extra�as en la noche.",
        "Ten cuidado y lleva una linterna contigo.",
        "Si necesitas ayuda, pregunta en la taberna."
    };

    private int indiceTexto = 0;
    private bool dialogoActivo = false;

    // M�todo para activar el di�logo
    public void ActivarDialogo()
    {
        if (dialogoActivo)
        {
            SiguienteTexto();
        }
        else
        {
            MostrarTexto();
        }
    }

    // Mostrar el primer texto del di�logo
    void MostrarTexto()
    {
        indiceTexto = 0;
        dialogoTexto.text = dialogos[indiceTexto];
        dialogoTexto.gameObject.SetActive(true);
        dialogoFondo.SetActive(true);
        dialogoActivo = true;
    }

    // Mostrar el siguiente texto
    void SiguienteTexto()
    {
        if (indiceTexto < dialogos.Count - 1)
        {
            indiceTexto++;
            dialogoTexto.text = dialogos[indiceTexto];
        }
        else
        {
            OcultarTexto();
        }
    }

    // Ocultar el cuadro de di�logo
    void OcultarTexto()
    {
        dialogoTexto.gameObject.SetActive(false);
        dialogoFondo.SetActive(false);
        dialogoActivo = false;
    }
}