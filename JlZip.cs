using System;
using System.IO;
using System.IO.Packaging;
using System.Web;

namespace JasonLib
{
    public class JlZip
    {
        #region 解压缩

        /// <summary>
        /// Extract a container Zip. NOTE: container must be created as Open Packaging Conventions (OPC) specification
        /// </summary>
        /// <param name="folderName">The folder to extract the package to</param>
        /// <param name="compressedFileName">The package file</param>
        /// <param name="overrideExisting">override existing files</param>
        /// <returns></returns>
        public static bool UncompressFile(string folderName, string compressedFileName, bool overrideExisting)
        {
            var result = false;
            try
            {
                if (!File.Exists(compressedFileName))
                {
                    return result;
                }

                var directoryInfo = new DirectoryInfo(folderName);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                using (var package = Package.Open(compressedFileName, FileMode.Open, FileAccess.Read))
                {
                    foreach (var packagePart in package.GetParts())
                    {
                        ExtractPart(packagePart, folderName, overrideExisting);
                    }
                }

                result = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error unzipping file " + compressedFileName, e);
            }

            return result;
        }

        public static void ExtractPart(PackagePart packagePart, string targetDirectory, bool overrideExisting)
        {
            var stringPart = targetDirectory + HttpUtility.UrlDecode(packagePart.Uri.ToString()).Replace('\\', '/');

            if (!Directory.Exists(Path.GetDirectoryName(stringPart)))
                Directory.CreateDirectory(Path.GetDirectoryName(stringPart));

            if (!overrideExisting && File.Exists(stringPart))
                return;
            using (var fileStream = new FileStream(stringPart, FileMode.Create))
            {
                packagePart.GetStream().CopyTo(fileStream);
            }
        }

        #endregion
    }
}
