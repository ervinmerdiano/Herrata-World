using UnityEngine;

public class PlayerInteractAnimals : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
    Animal animal = other.GetComponent<Animal>();
    if (animal == null)
        animal = other.GetComponentInParent<Animal>();

    if (animal != null)
    {
        Debug.Log(animal.animalSoundName);
            animal.PlaySound();
    }
    }

}
