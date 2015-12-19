using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Model
{
    public class PagingHelper<T>
    {
        //分页数据源
        public List<T> DataSource { get; private set; }
        //每页显示记录的数量
        public int PageSize { get; private set; }
        //当前页数
        public int PageIndex { get; set; }
        //分页总页数
        public int PageCount { get; private set; }

        //是否有前一页
        public bool HasPrev { get { return PageIndex > 1; } }
        //是否有下一页
        public bool HasNext { get { return PageIndex < PageCount; } }

        public int PageNow { get; set; }

        //构造函数
        public PagingHelper(int pageSize, List<T> dataSource)
        {
            this.PageSize = pageSize > 1 ? pageSize : 1;
            this.DataSource = dataSource;
            PageCount = (int)Math.Ceiling(dataSource.Count() / (double)pageSize);
        }

        //获取当前页数据
        public List<T> GetPagingData()
        {
            return DataSource.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        public List<T> GetPagingData(int page)
        {
            PageIndex = page;
            return DataSource.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}