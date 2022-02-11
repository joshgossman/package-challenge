using com.mobiquity.packer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageChallengeConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var result = Packer.Pack("C:\\temp\\packageInput");
		}
	}
}
