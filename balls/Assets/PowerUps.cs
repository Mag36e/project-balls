using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : ScriptableObject
{
   public string name;
   public Sprite icon;
   public PowerUpType Type;
   public Target HitType;
   public enum PowerUpType
   {
      Momentum,
      Placementing,
      StatsBuff,
   }

   public enum Target
   {
      Yourself,
      Nearest,
      Aim,
   }
}
