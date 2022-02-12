using com.mobiquity.packer.Models;
using System.Collections.Generic;

namespace com.mobiquity.packer.Services.Interfaces
{
	public interface IPackageService
	{
		PackageFileModel ParsePackageFile(string filePath);
		string SortPackages(List<PackageModel> packages);
	}
}
