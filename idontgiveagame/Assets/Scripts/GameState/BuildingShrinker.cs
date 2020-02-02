using System;
using System.Collections.Generic;
using idgag.AI;
using idgag.GameState.LaneSections;
using UnityEngine;

namespace idgag.GameState
{
    public class BuildingShrinker : MonoBehaviour
    {
        private Vector3 initialPosition;
        public float amountToSink = 190;

        private void Awake()
        {
            initialPosition = transform.position;
        }

        public void SinkByPercent(float percent)
        {
            transform.position = initialPosition - new Vector3(0, amountToSink * percent, 0);
        }

        public void ResetPosition()
        {
            transform.position = initialPosition;
        }
    }
}
