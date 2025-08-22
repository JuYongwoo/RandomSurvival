using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Assuming ManagerObject.playerStatObj.PlayerStatDB is accessible
            ManagerObject.playerStatObj.PlayerStatDB.setCurrentWeapon(WeaponDatabase.Weapons.Bow);
            Destroy(gameObject); // Destroy the weapon item after equipping
        }
    }
}
