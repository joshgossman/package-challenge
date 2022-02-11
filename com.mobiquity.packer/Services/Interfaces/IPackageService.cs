using com.mobiquity.packer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Services.Interfaces
{
	public interface IPackageService
	{
		PackageFileModel ParsePackageFile(string filePath);
	}
}
