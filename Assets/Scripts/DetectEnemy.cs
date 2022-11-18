using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
   private List<Enemy> _enemies = new();
   public Action OnDetectEnemy;

   public bool GetEnemy(out Enemy enemy)
   {
      foreach (var enemy1 in _enemies)
      {
         if (enemy1.isDead) continue;
         enemy = enemy1;
         return true;
      }

      enemy = null;
      return false;
   }
   
   private void OnTriggerEnter2D(Collider2D col)
   {
      var enemy = col.GetComponent<Enemy>();
      if (enemy)
      {
         _enemies.Add(enemy);
         OnDetectEnemy?.Invoke();
      }
   }
}
