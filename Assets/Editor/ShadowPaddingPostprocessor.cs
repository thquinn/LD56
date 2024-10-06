using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

public class ShadowPaddingPostprocessor : AssetPostprocessor {
    public void OnPostprocessTexture(Texture2D texture) {
        // Rename frames.
        var factory = new SpriteDataProviderFactories();
        factory.Init();
        var dataProvider = factory.GetSpriteEditorDataProviderFromObject(assetImporter);
        dataProvider.InitSpriteEditorDataProvider();
        var rects = dataProvider.GetSpriteRects();
        if (rects.Length > 0) {
            for (int i = 0; i < rects.Length; i++) {
                rects[i].name = $"{texture.name}_{i}";
            }
            dataProvider.SetSpriteRects(rects);
            dataProvider.Apply();
        }
        // Create shadows.
        if (!assetPath.StartsWith("Assets/Sprites/Creatures")) return;
        Debug.Log("ShadowPaddingPostprocessor");
        int spriteCount = texture.width / 12;
        Texture2D copy = new Texture2D(spriteCount * 16, 16, TextureFormat.ARGB32, false);
        copy.SetPixels(Enumerable.Repeat(Color.clear, copy.width * copy.height).ToArray());
        for (int i = 0; i < spriteCount; i++) {
            Color[] pixels = texture.GetPixels(i * 12, 0, 12, 12);
            Color32[] color32s = new Color32[pixels.Length];
            for (int j = 0; j < pixels.Length; j++) {
                color32s[j] = pixels[j];
            }
            copy.SetPixels32(i * 16 + 2, 2, 12, 12, color32s);
        }
        string assetFile = assetPath.Split('/').Last();
        File.WriteAllBytes($"Assets/Sprites/Shadows/{assetFile}", copy.EncodeToPNG());
    }
}
