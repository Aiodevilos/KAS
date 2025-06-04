using UnityEngine;

public class DamageDealt : MonoBehaviour
{
    public float damageAmount; // Amount of damage to deal
    public string ExcludedTag; // Tag to exclude from damage dealing
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ExcludedTag))
        {
        return; // Exit the method without executing further logic
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the CharacterHealth script from the collided object
            CharacterHealth characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
            if (characterHealth != null)
            {
                // Send the damage value to the CharacterHealth script
                characterHealth.TakeDamage(damageAmount);
                Destroy(gameObject); // Destroy the bullet after dealing damage
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the CharacterHealth script from the collided object
            CharacterHealth characterHealth = collision.gameObject.GetComponent<CharacterHealth>();
            if (characterHealth != null)
            {
                // Send the damage value to the CharacterHealth script
                characterHealth.TakeDamage(damageAmount);
                Destroy(gameObject); // Destroy the bullet after dealing damage
            }
        }        
    }
}
