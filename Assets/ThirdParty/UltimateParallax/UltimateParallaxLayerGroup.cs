using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateParallax
{
    [Serializable]
    public class UltimateParallaxLayerGroup
    {
        [HideInInspector]
        public bool foldout;


        public string groupName = "Default";
        public Transform groupParent;
        public int startingLayerOrder;
        public float zOffset;

        [Range(-200, 200)]
        public float parallaxSpeedX = 50f;

        [Range(-200, 200)]
        public float parallaxSpeedY;


        public List<UltimateParallaxLayer> layers = new();
    }
}