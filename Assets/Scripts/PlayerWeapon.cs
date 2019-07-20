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

        public void OnWeaponSwitch()
        {
            currentDamageType++;
            if (currentDamageType >= DamageType.Yellow) currentDamageType = DamageType.Green;
        }

        public void RemoveFern(AngryFern fernToCut)
        {
            currentlyCollidingFerns.Remove(fernToCut);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            AngryFern fernToCut = collision.GetComponent<AngryFern>();
            fernToCut.OnWeaponDamaged(damage, this);
            if(fernToCut != null)
            {
                currentlyCollidingFerns.Add(fernToCut);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            AngryFern fernToCut = collision.GetComponent<AngryFern>();
            if (fernToCut != null)
            {
                currentlyCollidingFerns.Remove(fernToCut);
            }
        }
    }
}