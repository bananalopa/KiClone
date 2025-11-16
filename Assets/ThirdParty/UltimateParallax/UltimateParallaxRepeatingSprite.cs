using System.Collections.Generic;
using UnityEngine;

namespace UltimateParallax
{
    public class UltimateParallaxRepeatingSprite : MonoBehaviour
    {
        public List<GameObject> repeatingSprites = new();
        private float width;

        public void Start()
        {
            var originalSprite = repeatingSprites[0];
            width = originalSprite.GetComponent<SpriteRenderer>().bounds.size.x;
        }

        public void InitialSetup()
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                var newSr = Instantiate(sr, transform, false);
                newSr.name = name + "_RepeatingSprite_0";
                if(newSr.TryGetComponent<UltimateParallaxRepeatingSprite>(out var repeatingSprite))
                {
                    DestroyImmediate(repeatingSprite);
                }
                if(newSr.TryGetComponent<UltimateParallaxLayer>(out var layer))
                {
                    DestroyImmediate(layer);
                }
                DestroyImmediate(sr);
                repeatingSprites.Add(newSr.gameObject);
            }

            SetupSprites(1);
        }

        private void SetupSprites(int repeatCount)
        {
            if (repeatingSprites.Count > 1)
                while (repeatingSprites.Count > 1)
                {
                    var deletingSprite = repeatingSprites[^1];
                    repeatingSprites.RemoveAt(repeatingSprites.Count - 1);
                    DestroyImmediate(deletingSprite);
                }

            var originalSprite = repeatingSprites[0];
            var sr = originalSprite.GetComponent<SpriteRenderer>();
            var srWidth = sr.bounds.size.x;
            var srHeight = sr.bounds.size.y;
            for (var i = 0; i < repeatCount * 2; i++)
            {
                var newSprite = Instantiate(originalSprite, transform, false);
                newSprite.name = name + "_RepeatingSprite_" + repeatingSprites.Count;
                if (repeatingSprites.Count % 2 == 0)
                {
                    newSprite.transform.position = originalSprite.transform.position + Vector3.right * srWidth * (repeatingSprites.Count - 1);
                }
                else
                {
                    newSprite.transform.position = originalSprite.transform.position + Vector3.left * srWidth * repeatingSprites.Count;
                }
                repeatingSprites.Add(newSprite);
            }
        }

        public void ParallaxUpdate(Vector3 cameraPos)
        {
            foreach (var sprite in repeatingSprites)
            {
                if (sprite is null)
                    continue;

                var cameraX = cameraPos.x;
                var spriteX = sprite.transform.position.x;
                var distance = spriteX - cameraX;

                if (distance < -width * 1.5f)
                {
                    var dis = Vector3.right * (width * repeatingSprites.Count);
                    
                    sprite.transform.position += dis;
                }
                else if (distance > width * 1.5f)
                {
                    var dis = Vector3.right * (width * repeatingSprites.Count);
                    sprite.transform.position -= dis;
                }
            }
        }
    }
}