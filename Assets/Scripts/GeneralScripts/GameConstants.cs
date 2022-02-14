using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGGE
{
    public static class GameConstants{
        public static float damping = 5f;
        public static float playerHeight = 2.2f;
        public static float cameraAngleOffset = 20f;

        public static bool blocked = false;
        public static Vector3 blockedCamPos = Vector3.zero;

        public static float maxAmmunitionCount = 20f;
        public static float currentAmmunitionCount = 20f;
        public static string ammoTxt = "";

        public static float maxMagicCount = 100f;
        public static float currentMagicCount = 100f;
        public static string magicTxt = "";

        public static Vector3 pForward = Vector3.zero;

        public static void UpdateAmmoTxt(){
            ammoTxt = "Ammo: " + currentAmmunitionCount + " / " + maxAmmunitionCount;
        }

        public static void ReloadAmmoTxt(){
            ammoTxt = "Reloading...";
        }

        public static void UpdateMagicTxt(){
            magicTxt = "Magic: " + currentMagicCount + " / " + maxMagicCount;
        }

        public static void ReloadMagicTxt(){
            magicTxt = "Recharging...";
        }
    }
}