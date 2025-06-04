using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [Header(" >>>Please set the character Max Health<<<")]
    [Header(" >>>From the HealthBar Canvas object for this character,")]
    [Header(" expand and drag the Health Bar Object to the slot below<<<")]
    public float maxHealth; // Maximum health for the character
    private float currentHealth;
    public string characterTag; // Tag for the character (e.g., "Player", "Enemy")

    public GameObject healthBar; // Reference to the health bar script

    void Start()
    {   
        currentHealth = maxHealth; // Initialize current health to max health
        Validate2DComponents(); // Validate and add 2D components if necessary
        FindHealthBar(); // Find the health bar in the scene
        healthBar.GetComponent<HealthBarScript>().SetMaxHealth(maxHealth); // Set the max health on the health bar
    }
    public void TakeDamage(float damage)
    {

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above max
        if (healthBar != null)
        {
            healthBar.GetComponent<HealthBarScript>().SetHealth(currentHealth); // Update the health bar
        }
        monitorHealth(); // Check if health is below or equal to 0
    }
    public void HealDamage(float heal)
    {

        currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above max
        if (healthBar != null)
        {
            healthBar.GetComponent<HealthBarScript>().SetHealth(currentHealth); // Update the health bar
        }

    }
    public void FindHealthBar()
    {
        healthBar = GetComponentInChildren<HealthBarScript>()?.gameObject;
        if (healthBar == null)
        {
            Debug.LogError("HealthBarScript not found in the scene. Please ensure it is present.");
            healthBar = GetComponent<HealthBarScript>()?.gameObject;
        }
    }
    public void Validate2DComponents()
    {
        // Check if the GameObject already has a Rigidbody2D component
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Add a Rigidbody2D component to the GameObject
            rb = gameObject.AddComponent<Rigidbody2D>();

            // Configure the Rigidbody2D properties (optional)
            rb.gravityScale = 0; // Disable gravity
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent rotation
            Debug.Log("Rigidbody2D component added and configured.");
        }
        else
        {
            Debug.Log("Rigidbody2D component already exists.");
        }

            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            // Add a BoxCollider2D component to the GameObject
            boxCollider = gameObject.AddComponent<BoxCollider2D>();

            // Configure the BoxCollider2D properties (optional)
            boxCollider.isTrigger = true; // Set the collider as a trigger
            Debug.Log("BoxCollider2D component added and configured.");
        }
        else
        {
            Debug.Log("BoxCollider2D component already exists.");
        }
    }
    public void monitorHealth()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Character has died!"); // Log the death of the character
 //       GameObject dropList = GameObject.FindGameObjectWithTag("DropList"); // Find the DropList GameObject in the scene
        GameObject dropList = FindGameObjectWithinChildrenDropList(); // Find the DropList GameObject in the children of this GameObject
        if (dropList != null)
        {
            Debug.Log("DropList found: " + dropList.name);
            DropListScript dropListScript = dropList.GetComponent<DropListScript>();
            if (dropListScript != null)
            {
                GameObject randomDrop = dropListScript.GetRandomDrop();
                if (randomDrop != null)
                {
                    Debug.Log("Random drop instantiated: " + randomDrop.name);
                    Instantiate(randomDrop, transform.position, Quaternion.identity);
                }
            }
            else
            {
                Debug.LogError("DropListScript component is missing on the dropList GameObject.");
            }
        }
        // Destroy the character GameObject
        Destroy(gameObject);
    }
    private GameObject FindGameObjectWithinChildrenDropList()
    {
        Transform dropListTransform = transform.Find("EnemyDropList");
        if (dropListTransform != null)
        {
            Debug.Log("DropList found: " + dropListTransform.name);
            return dropListTransform.gameObject;
        }
        else
        {
            Debug.LogError("DropList GameObject not found among the children.");
            return null;
        }
    }
}