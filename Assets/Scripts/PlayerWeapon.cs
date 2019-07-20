using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCombat
{ 

    public enum DamageType
    {
        Green,
        Red,
        Yellow
    }

    public class PlayerWeapon : MonoBehaviour
    {
        private float damage = 0.5f;
        private DamageType currentDamageType = DamageType.Green;
        private HashSet<AngryFern> currentlyCollidingFerns = new HashSet<AngryFern>();

        private void Update()
        {
           if(Input.GetMouseButton(0)) OnWeaponFire();
           if(Input.GetMouseButton(0)) FinishedFiring();
        }

        public void OnWeaponSwitch()
        {
            currentDamageType++;
            if (currentDamageType >= DamageType.Yellow) currentDamageType = DamageType.Green;
        }

        public void OnWeaponFire()
        {
            FireWeapon();
        }

        public void FinishedFiring()
        {
            if (currentlyCollidingFerns.Count == 0)
            {
                return;
            }
            foreach (AngryFern damagedFern in currentlyCollidingFerns)
            {
                damagedFern.FinishedDamaging();
            }
        }

        private void FireWeapon()
        {
            if(currentlyCollidingFerns.Count == 0)
            {
                return;
            }
            foreach(AngryFern damagedFern in currentlyCollidingFerns)
            {
                damagedFern.OnWeaponDamaged(damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("entered");
            AngryFern fernToCut = collision.GetComponent<AngryFern>();
            if(fernToCut != null)
            {
                currentlyCollidingFerns.Add(fernToCut);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Quit");
            AngryFern fernToCut = other.GetComponent<AngryFern>();
            if (fernToCut != null)
            {
                currentlyCollidingFerns.Remove(fernToCut);
            }
        }
    }
}