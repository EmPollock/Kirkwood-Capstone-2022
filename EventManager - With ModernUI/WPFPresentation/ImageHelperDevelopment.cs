using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPFPresentation
{
    internal class ImageHelperDevelopment : IImageHelper
    {
        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Path to save the image
        /// </summary>
        /// <returns></returns>
        public string PathToSaveImage()
        {
            return AppData.DataPath + @"\";
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Creates a unique name for the image based on the time, a random 4 digit number, and the image name.
        /// Strips whitespace and special characters
        /// </summary>
        /// <param name="imageName">The original name of the image with file extension</param>
        /// <returns></returns>
        public string CreateNameForImage(string imageName)
        {
            string nameToReturn = "";
            Random random = new Random();

            string dateString = Regex.Replace(DateTime.Now.ToString(), "[^a-zA-Z0-9_.]+", "");
            string strippedImageName = Regex.Replace(imageName, "[^a-zA-Z0-9_.]+", "");

            nameToReturn = dateString + "-" + random.Next(0, 9999) + "-" + strippedImageName;

            return nameToReturn;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Path to the image
        /// </summary>
        /// <param name="imageName">Image Name stored in the database</param>
        /// <returns></returns>
        public Uri FindImageSource(string imageName)
        {
            return new Uri(PathToSaveImage() + imageName, UriKind.Absolute);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Saves an image and returns a new image name. Takes the image file and makes a copy at the correct address.
        /// 
        /// </summary>
        /// <param name="fileName">The name of the file and extension</param>
        /// <param name="sourceFile">The full path of the file to be saved</param>
        /// <returns>A new image name as a string</returns>
        public string SaveImageReturnsNewImageName(string fileName, string sourceFile)
        {
            // sources
            // https://wpf-tutorial.com/dialogs/the-openfiledialog/
            //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders

            string newFileName = CreateNameForImage(fileName);
            string targetFile = PathToSaveImage() + "\\" + newFileName;

            try
            {
                System.IO.File.Copy(sourceFile, targetFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newFileName;
        }
    }
}
