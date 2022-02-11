using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.Common
{
	public static class Constants
	{
		public static readonly char FILE_PARSE_PACKAGE_WEIGHT_DELIMITER = ':';
		public static readonly char[] FILE_PARSE_PACKAGE_ITEM_DELIMITER = new char[] { '(', ')' };
		public static readonly char FILE_PARSE_PACKAGE_ITEM_DETAIL_DELIMITER = ',';
	}
}
