using System.Collections.Generic;
using System.Data;

namespace IndiansInc
{
	public class BatchSizeCompletedEventArgs
	{
		// Methods
		public BatchSizeCompletedEventArgs()
		{
		}

		// Properties
		public string CompletedRows { get; set; }
		public List<DataRow> ErrorDataRows { get; set; }


	}


}
