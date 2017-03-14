using CC.UI.Webhost.Infrastructure;

namespace OCC.UI.Webhost.Utilities
{
    /// <summary>
    /// Helper functions to deal with images.
    /// </summary>
    public static class ImageUtils
    {
        /// <summary>
        /// Creates a WebImageOCC from a Byte Array.
        /// </summary>
        /// <param name="imageBytes">Byte array containing the image data.</param>
        /// <returns>WebImageOCC created from the image data, or null if no data was present.</returns>
        /// <exception cref=""
        public static WebImageOCC ImageFromBytes(byte[] imageBytes)
        {
            // If there isn't any data, just return null
            if (imageBytes == null || imageBytes.Length == 0)
            {
                return null;
            }

            try
            {
                return new WebImageOCC(imageBytes);
            }
            catch
            {
                // Just eat the exception and return Null for now.
                // It would probably be better to throw some kind of "corrupt image data"
                // exception and then setup logging to notify us of that, but for now this is
                // good enough I think.
                return null;
            }
        }
    }
}