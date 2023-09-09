using UnityEngine;
using ZXing;
using ZXing.Common;

namespace FeatureNotBug;

public sealed class QrCodeService : IQrCodeService {
    public int Width { get; set; } = 256;
    public int Height { get; set; } = 256;
    public int Margin { get; set; }

    public Texture2D Generate(string contents) {
        var options = new EncodingOptions
            { Width = Width, Height = Height, Margin = Margin, Hints = { [EncodeHintType.CHARACTER_SET] = "UTF-8" } };
        var writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE, Options = options };
        var colors = writer.Write(contents);
        var texture = new Texture2D(Width, Height);
        texture.SetPixels32(colors);
        texture.Apply();
        return texture;
    }

    public bool TryParse(Color32[] colors, int width, int height, out string contents) {
        var reader = new BarcodeReader();
        var result = reader.Decode(colors, width, height);
        if (result == null) {
            contents = string.Empty;
            return false;
        }

        contents = result.Text;
        return true;
    }
}
