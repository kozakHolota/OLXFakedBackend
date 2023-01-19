using System;
using System.Collections.Generic;

namespace OLXFakedBackend.Utils
{
	public class Paginator<T>
	{
		private int pageSize;

		private int PageNum { get; set; }
		List<T> inList;

        private int StartPosition {
			get
			{
				return (PageNum - 1) * pageSize;
            }
		}

		private int FinPosition
		{
			get
			{
				return StartPosition + pageSize;
            }
		}


        public Paginator(int pageSize, List<T> inList)
		{
            this.pageSize = pageSize;
			this.inList = inList;

        }

		public List<T> Get(int pageNum)
		{
			PageNum = pageNum;
			List<T> resList;
			if (pageNum >= 0)
			{
				int cutOffValue = (inList.Count - 1) - FinPosition;
				if (StartPosition > (inList.Count - 1)) resList = new List<T>();
				else if (StartPosition == (inList.Count - 1)) resList = inList.GetRange(StartPosition, 1);

				else if (cutOffValue < 0)
				{
					int repairedFinPosition = FinPosition + cutOffValue - 1;

					resList = (repairedFinPosition <= (inList.Count - 1)) ? inList.GetRange(StartPosition, (repairedFinPosition - StartPosition)) : inList.GetRange(StartPosition, (inList.Count - StartPosition));
				}
				else resList = inList.GetRange(StartPosition, FinPosition - StartPosition);
			}
			else resList = inList;

			return resList;
        }

		public int GetPagesNumber()
		{
			return pageSize > inList.Count ? 1 : inList.Count / pageSize;
		}
	}
}

