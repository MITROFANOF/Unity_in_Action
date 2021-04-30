using Ch12.Scripts;
using UnityEngine;

namespace Ch9.Scripts
{
    [RequireComponent(typeof(Renderer))]
    public class ColorChangeDevice : BaseDevice
    {
        protected override void Operate()
        {
            var randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            GetComponent<Renderer>().material.color = randomColor;
        }
    }
}
