using com.mobiquity.packer.Exceptions;
using com.mobiquity.packer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer
{
    public static class Packer
    {
        public static string Pack(string filePath)
		{
			try
			{
				var packageService = new PackageService(new PackageFileParser());

				var packageFile = packageService.ParsePackageFile(filePath);


				return "";
			}
			catch(Exception e)
			{
				throw new APIException(e.Message, e);
			}
		}
    }
}
