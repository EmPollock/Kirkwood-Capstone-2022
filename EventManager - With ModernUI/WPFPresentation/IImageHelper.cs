using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation
{
    internal interface IImageHelper
    {
        string CreateNameForImage(string imageName);
        Uri FindImageSource(string imageName);
        string PathToSaveImage();
        string SaveImageReturnsNewImageName(string fileName, string sourceFile);
    }
}
