namespace CatAPI.Services
{
    public class ValidationService
    {
        public static bool IsValidCatId(string catId)
        {
            return !string.IsNullOrWhiteSpace(catId) && catId.Length >= 5;
        }

        public static bool IsValidImage(byte[] image)
        {
            return image != null && image.Length > 0;
        }

        public static bool IsValidDimension(int width, int height)
        {
            return width > 0 && height > 0;
        }
    }
}
