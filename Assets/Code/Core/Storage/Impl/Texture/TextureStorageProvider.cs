﻿using System.Collections.Generic;
using AssemblyCSharp.Assets.Code.Core.Storage.Interface.Texture;

namespace AssemblyCSharp.Assets.Code.Core.Storage.Impl.Texture
{
    public class TextureStorageProvider : ITextureStorageProvider
    {
        private readonly Dictionary<string, UnityEngine.Texture> _images = new Dictionary<string, UnityEngine.Texture>();

        public UnityEngine.Texture GetTexture(string key)
        {
            var hasImage = _images.TryGetValue(key, out var image);
            return hasImage ? image : null;
        }

        public void SaveTexture(string key, UnityEngine.Texture image)
        {
            _images[key] = image;
        }
    }
}
