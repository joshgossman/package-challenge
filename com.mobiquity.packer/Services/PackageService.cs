using com.mobiquity.packer.Models;
using com.mobiquity.packer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Services
{
	public class PackageService : IPackageService
	{
		private IPackageFileParser _packageFileParser;

		public PackageService(IPackageFileParser packageFileParser)
		{
			_packageFileParser = packageFileParser;
		}

		public PackageFileModel ParsePackageFile(string filePath)
		{
			return _packageFileParser.Parse(filePath);
		}
	}
}
