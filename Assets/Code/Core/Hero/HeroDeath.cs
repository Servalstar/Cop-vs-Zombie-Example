using Core.Common;
using System;
using System.Collections;
using UnityEngine;

namespace Core.Hero
{
    public class HeroDeath : Death
    {
        public event Action HeroDied;

        protected override void HandleDeath() => 
            StartCoroutine(DelayDeath());

        private IEnumerator DelayDeath()
        {
            yield return new WaitForSeconds(3);

            HeroDied?.Invoke();
        }
    }
}