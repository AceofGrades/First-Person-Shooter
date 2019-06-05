using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Namespace for implementing UI references (Sliders, Toggles, etc.)
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject healthBarUIPrefab; // Prefab to spawn in healthbar parent
    public Transform healthBarParent; // Reference to parent to store healthbar UI
    public Transform healthBarPoint;

    private int health = 0;
    private Slider healthSlider;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max health
        health = maxHealth;
        // Spawn HealthBar UI into parent and get reference to Slider component on clone
        GameObject clone = Instantiate(healthBarUIPrefab, healthBarParent);
        healthSlider = clone.GetComponent<Slider>();

        // Get the Renderer component from this GameObject
        rend = GetComponent<Renderer>();
    }

    private void OnDestroy()
    {
        Destroy(healthSlider.gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Is the Renderer within the Camera's view
        if (rend.isVisible)
        {
            // Enable health slider
            healthSlider.gameObject.SetActive(true);
            // Update position of health bar
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPoint.position); // + offset
            healthSlider.transform.position = screenPosition;
        }
        else
        {
            // Disable the health slider
            healthSlider.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // Update value of slider
        healthSlider.value = (float)health / (float)maxHealth; // Converts 0-100 to 0-1 (current/max)
        // If health is zero
            if(health < 0)
        {
            // Destroy GameObject
            Destroy(gameObject);
        }
    }
}
