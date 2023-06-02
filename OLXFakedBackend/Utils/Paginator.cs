namespace OLXFakedBackend.Utils
{
	public class Paginator<T>
	{
		private int pageSize;

		private int PageNum { get; set; }
		IQueryable<T> InQuery { get; set; }

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
				return (InQuery == null) ? 0 : InQuery.Count();
            }
		}

		private int FinPosition
		{
			get
			{
				return StartPosition + pageSize;
            }
		}


        public Paginator(int pageSize)
		{
            this.pageSize = pageSize;
        }

		public IQueryable<T> Get(int pageNum, IQueryable<T> _inQuery=null)
		{
			PageNum = pageNum;

			if (InQuery == null && _inQuery == null) throw new Exception("No Query to paginate");
			else InQuery = _inQuery;

			if (pageNum >= 0)
			{
				int cutOffValue = (Count) - FinPosition;
				if (StartPosition > (Count)) return InQuery.Skip(Count);
				else if (StartPosition == (Count))
				{
					return InQuery.Take(1);
				}
				else if (cutOffValue < 0)
				{
					int repairedFinPosition = FinPosition + cutOffValue;

					return (repairedFinPosition <= (Count)) ? InQuery.Take((repairedFinPosition - StartPosition)).Skip(StartPosition) : InQuery.Take((Count - StartPosition)).Skip(StartPosition);
				}
			}


			return InQuery.Take(FinPosition).Skip(StartPosition);

        }

		public int GetPagesNumber(IQueryable<T> _inQuery = null)
		{
            if (InQuery == null && _inQuery == null) throw new Exception("No Query to paginate");
            else if (InQuery == null && _inQuery != null) InQuery = _inQuery;

            return pageSize > Count ? 1 : Count / pageSize;
		}
	}
}

