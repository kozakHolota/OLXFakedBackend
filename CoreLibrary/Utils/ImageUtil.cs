using CoreLibrary.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CoreLibrary.Utils
{
    public class ImageUtil
	{
        public static string GetCategoryImagesPAth(IFileProvider _fileProvider) => Path.Combine(_fileProvider.GetFileInfo("/").PhysicalPath, "images", "categories");
        public static string GetItemImagesPath(int itemId) => FileUtils.GetPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), itemId.ToString(), "items");
        public static string GetUserPicPath(string userId) => FileUtils.GetPath(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), userId, "user");

        public static string GetUserImageApiPath(string userId, string fName) => $"/api/images/{userId}/{fName}";
        public static string GetItemImageApiPath(int itemId, string fName) => $"/api/images/item/{itemId}/{fName}";
        public static string GetCategoryImageApiPath(string fName) => $"/api/images/categories/{fName}";

        public static async Task<bool> PlaceImage(string image, string serverPath, string imageName)
        {
            if (image == null || image.Length == 0)
            {
                throw new EmptyImageException("Empty Image");
            }

            var imageDataByteArray = Convert.FromBase64String(image);


            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            System.IO.File.WriteAllBytes(Path.Combine(serverPath, imageName), imageDataByteArray);
            return true;
        }

        public static async Task<FileContentResult> GetImage(string imagePath)
        {
            if (!System.IO.File.Exists(imagePath))
            {
                throw new FileNotFoundException("Image is not found");
            }

            string mType = Path.GetExtension(imagePath) == "jpg" ? "image/jpeg" : $"image/{Path.GetExtension(imagePath)}";

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return new FileContentResult(imageBytes, mType)
            {
                FileDownloadName = Path.GetFileName(imagePath)
            };
        }
    }
}

