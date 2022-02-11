using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;

namespace com.mobiquity.packer.Services
{
	public class PackageService : IPackageService
	{
		private IPackageFileParser _packageFileParser;

		public PackageService(IPackageFileParser packageFileParser)
		{
			_packageFileParser = packageFileParser;
		}

		public PackageFileModel ParsePackageFile(string fileContents)
		{
			return _packageFileParser.Parse(fileContents);
		}
	}
}
