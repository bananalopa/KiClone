using UnityEngine.UIElements;

namespace UltimateParallax
{
    public class UltimateParallaxLayerGroupElement : VisualElement
    {
        public static VisualElement CreateVisualElement()
        {
            var root = new VisualElement();

            root.Add(new TextField("groupName")
            {
                bindingPath = "groupName",
                name = "groupName",
                label = "Group Name"
            });

            root.Add(new IntegerField("startingLayerOrder")
            {
                bindingPath = "startingLayerOrder",
                name = "startingLayerOrder",
                label = "Starting Layer"
            });

            root.Add(new FloatField("zOffset")
            {
                bindingPath = "zOffset",
                name = "zOffset",
                label = "Z Offset"
            });

            root.Add(new ListView
            {
                bindingPath = "layers",
                name = "layers",
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                makeItem = MakeItem
            });

            return root;
        }

        private static VisualElement MakeItem()
        {
            var ve = new VisualElement();

            ve.Add(new Label("TestVal"));

            return ve;
        }
    }
}