using System.Collections.Generic;
using UnityEngine;

namespace UltimateParallax
{
    [ExecuteInEditMode]
    public class UltimateParallaxManager : MonoBehaviour
    {
        public Camera targetCamera;
        public bool previewInEditMode;
        public float parallaxScale = 1;

        public List<UltimateParallaxLayerGroup> layerGroups = new();

        private void LateUpdate()
        {
            if (Application.isPlaying == false && previewInEditMode == false)
                return;

            if (!targetCamera) 
                return;
            
            var camPos = targetCamera.transform.position;
            foreach (var layerGroup in layerGroups)
            {
                foreach (var layer in layerGroup.layers)
                {
                    if(!layer)
                        continue;
                        
                    var layerPos = layer.transform.position;
                        
                    layerPos.x = camPos.x * CameraRemap(layer.parallaxSpeedX) * parallaxScale;
                    //layerPos.y = camPos.y * CameraRemap(layer.parallaxSpeedY) * parallaxScale;
                    layer.transform.position = layerPos;
                    if(Application.isPlaying)
                    {
                        layer.ParallaxUpdate(camPos);
                    }
                }
            }
        }

        private float CameraRemap(float value)
        {
            return value >= 0 ? Remap(value, 0, 200, 1, -2) : Remap(value, -200, 0, 3, 1);
        }

        private static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            // Calculate the ratio of the value within the original range
            var ratio = (value - fromMin) / (fromMax - fromMin);
            // Scale it to the new range
            return toMin + ratio * (toMax - toMin);
        }
    }
}