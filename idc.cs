using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
	public class idc : IEqualityComparer<Process>
	{
		public bool Equals(Process x, Process y)
		{
			return x.Id == y.Id;
		}

		public int GetHashCode(Process obj)
		{
			if (Object.ReferenceEquals(obj, null)) return 0;
			return obj.Id == null ? 0 : obj.Id.GetHashCode();
		}
	}

}
