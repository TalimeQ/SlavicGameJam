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
        [SerializeField] Quaternion minimalDeltaRot;

        private float damage = 0.5f;
        private DamageType currentDamageType = DamageType.Green;
        private HashSet<AngryFern> currentlyCollidingFerns = new HashSet<AngryFern>();

        Quaternion newRot;
        Quaternion lastRot;

        private void Start()
        {
            newRot = transform.rotation;
            lastRot = transform.rotation;
        }

        public void OnWeaponSwitch()
        {
            currentDamageType++;
            if (currentDamageType >= DamageType.Yellow) currentDamageType = DamageType.Green;
        }

        public void RemoveFern(AngryFern fernToCut)
        {
            currentlyCollidingFerns.Remove(fernToCut);
        }

        public void Update()
        {
            if (Time.frameCount % 2 == 0)
            {
                lastRot = transform.rotation;
            }
            else
            {
                newRot = transform.rotation;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            AngryFern fernToCut = collision.GetComponent<AngryFern>();
           
            if(Quaternion.Angle(newRot,lastRot) > 1.0f)
            {
                fernToCut.OnWeaponDamaged(damage, this);
            }
         /*   if(fernToCut != null)
            {
                currentlyCollidingFerns.Add(fernToCut);
            }*/
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            AngryFern fernToCut = collision.GetComponent<AngryFern>();
            if (fernToCut != null)
            {
                //currentlyCollidingFerns.Remove(fernToCut);
            }
        }
    }
}