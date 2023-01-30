using System;
using System.Collections.Generic;

namespace OLXFakedBackend.Utils
{
	public class Paginator<T>
	{
		private int pageSize;

		private int PageNum { get; set; }
        IQueryable<T> inQuery;

        private int StartPosition {
			get
			{
				return (PageNum - 1) * pageSize;
            }
		}

		private int Count
		{
			get
			{
				return (inQuery == null) ? 0 : inQuery.Count();
            }
		}

		private int FinPosition
		{
			get
			{
				return StartPosition + pageSize;
            }
		}


        public Paginator(int pageSize, IQueryable<T> inQuery = null)
		{
            this.pageSize = pageSize;
			this.inQuery = inQuery;
        }

		public IQueryable<T> Get(int pageNum, IQueryable<T> _inQuery=null)
		{
			PageNum = pageNum;

			if (inQuery == null && _inQuery == null) throw new Exception("No Query to paginate");
			else if (inQuery == null && _inQuery != null) inQuery = _inQuery;

			if (pageNum >= 0)
			{
				int cutOffValue = (Count - 1) - FinPosition;
				if (StartPosition > (Count - 1)) return inQuery.Skip(Count);
				else if (StartPosition == (Count - 1))
				{
					return inQuery.Skip(StartPosition).Take(1);
				}
				else if (cutOffValue < 0)
				{
					int repairedFinPosition = FinPosition + cutOffValue - 1;

					return (repairedFinPosition <= (Count - 1)) ? inQuery.Skip(StartPosition).Take((repairedFinPosition - StartPosition)) : inQuery.Skip(StartPosition).Take((Count - StartPosition));
				}
			}


			return inQuery.Skip(StartPosition).Take(FinPosition - StartPosition);

        }

		public int GetPagesNumber(IQueryable<T> _inQuery = null)
		{
            if (inQuery == null && _inQuery == null) throw new Exception("No Query to paginate");
            else if (inQuery == null && _inQuery != null) inQuery = _inQuery;

            return pageSize > Count ? 1 : Count / pageSize;
		}
	}
}

