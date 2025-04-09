using UnityEngine;

public class Lights : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemigo"))
        {
            Enemigo enem = collision.gameObject.GetComponent<Enemigo>();
            enem.inLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            Enemigo enem = collision.gameObject.GetComponent<Enemigo>();
            enem.inLight = false;
        }
    }
}
