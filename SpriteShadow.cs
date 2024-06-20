// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub:   https://github.com/RimuruDev
//
// **************************************************************** //

using UnityEngine;

namespace AbyssMoth
{
#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [AddComponentMenu("0xRimuruDev/" + nameof(SpriteShadow))]
    [HelpURL("https://github.com/RimuruDev/Unity-SpriteShadowShader")]
    public sealed class SpriteShadow : MonoBehaviour
    {
        private static readonly int ShadowColor = Shader.PropertyToID("_ShadowColor");
        private static readonly int ShadowOffset = Shader.PropertyToID("_ShadowOffset");

        [SerializeField] private Color shadowColor = new(0, 0, 0, 0.5f);
        [SerializeField] private Vector2 shadowOffset = new(0.1f, -0.1f);

        private SpriteRenderer spriteRenderer;
        private MaterialPropertyBlock propertyBlock;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            propertyBlock = new MaterialPropertyBlock();
        }

        private void Update() =>
            UpdateShadow();

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void OnValidate() =>
            UpdateShadow();

        private void UpdateShadow()
        {
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component is missing.");
                return;
            }

            if (spriteRenderer.sharedMaterial == null)
            {
                Debug.LogError("SharedMaterial is missing on SpriteRenderer.");
                return;
            }

            if (propertyBlock == null)
            {
                propertyBlock = new MaterialPropertyBlock();
            }

            spriteRenderer.GetPropertyBlock(propertyBlock);
            
            if (propertyBlock == null)
            {
                Debug.LogError("Failed to get PropertyBlock.");
                return;
            }

            propertyBlock.SetColor(ShadowColor, shadowColor);
            propertyBlock.SetVector(ShadowOffset, shadowOffset);
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}
