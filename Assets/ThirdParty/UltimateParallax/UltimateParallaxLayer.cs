using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UltimateParallax
{
    [Serializable]
    [ExecuteInEditMode]
    public class UltimateParallaxLayer : MonoBehaviour
    {
        [HideInInspector]
        public bool foldout;

        [Range(-200, 200)]
        public float parallaxSpeedX = 50f;

        [Range(-200, 200)]
        public float parallaxSpeedY;

        public int parallaxLayer;

        private List<UltimateParallaxRepeatingSprite> repeatingSprites = new();

        public void Start()
        {
            repeatingSprites = transform.GetComponentsInChildren<UltimateParallaxRepeatingSprite>().ToList();
        }

        public void ParallaxUpdate(Vector3 cameraPos)
        {
            foreach (var repeatingSprite in repeatingSprites)
            {
                repeatingSprite.ParallaxUpdate(cameraPos);
            }
        }

        public void SetZOffset(float zOffset)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zOffset);
        }

        public void SetParallaxSpeedX(float speed)
        {
            parallaxSpeedX = speed;
        }

        public void SetParallaxSpeedY(float speed)
        {
            parallaxSpeedY = speed;
        }

        public void SetLayerParent(Transform parent)
        {
            transform.SetParent(parent);
        }

        public void SetParallaxLayer(int layer)
        {
            parallaxLayer = layer;
            ApplyParallaxLayer();
        }

        public void ApplyParallaxLayer()
        {
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.sortingOrder = parallaxLayer;
            }

            foreach (var sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.sortingOrder = parallaxLayer;
            }
        }
    }
}