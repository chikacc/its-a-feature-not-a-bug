using UnityEngine;

namespace FeatureNotBug;

public interface IQrCodeService {
    int Width { get; set; }
    int Height { get; set; }
    int Margin { get; set; }
    Texture2D Generate(string contents);
    bool TryParse(Color32[] colors, int width, int height, out string contents);
}
