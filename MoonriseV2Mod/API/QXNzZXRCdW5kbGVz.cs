using System.IO;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace MoonriseV2Mod.API
{
    internal static class QXNzZXRCdW5kbGVz
    {
        //public static AssetBundle assetBundle { get; set; }
        public static AssetBundle UshioUiAssetBundle { get; private set; }
        public static AssetBundle MoonriseAssetBundle { get; private set; }
        public static bool isInitialized = false;

        public static void InitializeAssetBundle()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoonriseV2Mod.ushioui.assetbundle"))
            using (var tempStream = new MemoryStream((int)stream.Length))
            {
                stream.CopyTo(tempStream);

                UshioUiAssetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                UshioUiAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoonriseV2Mod.moonrise.assetbundle"))
            using (var tempStream = new MemoryStream((int)stream.Length))
            {
                stream.CopyTo(tempStream);

                MoonriseAssetBundle = AssetBundle.LoadFromMemory_Internal(tempStream.ToArray(), 0);
                MoonriseAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            }

            isInitialized = true;
        }

        public static Texture2D TextureFromSprite(Sprite sprite)
        {
            if (sprite.rect.width != sprite.texture.width)
            {
                Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                             (int)sprite.textureRect.y,
                                                             (int)sprite.textureRect.width,
                                                             (int)sprite.textureRect.height);
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            }
            else
                return sprite.texture;
        }

        public static Sprite GetSprite(string assetPath)
        {
            return MoonriseAssetBundle.LoadAsset_Internal(assetPath, Il2CppType.Of<Sprite>()).Cast<Sprite>();
        }
    }
}
