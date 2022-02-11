using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Services;
using System;
using System.IO;

namespace com.mobiquity.packer
{
    public static class Packer
    {
        public static string Pack(string filePath)
		{
			try
			{
				if (!File.Exists(filePath))
				{
					throw new APIException("File cannot be found.");
				}

				var packageService = new PackageService(new PackageFileParser());
				var fileContents = File.ReadAllText(filePath);
				var packageFile = packageService.ParsePackageFile(fileContents);


				return "";
			}
			catch(Exception e)
			{
				throw new APIException(e.Message, e);
			}
		}
    }
}
