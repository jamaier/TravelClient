// public class Pager
// {
//     public int CurrentPage { get; set; }
//     public int PageCount { get; set; }

//     public Pager(int currentPage, int pageCount)
//     {
//         CurrentPage = currentPage;
//         PageCount = pageCount;
//     }

//     public IEnumerable<int> Pages()
//     {
//         if (PageCount <= 1)
//         {
//             return Enumerable.Empty<int>();
//         }

//         const int maxPagesToDisplay = 7;

//         int startPage = Math.Max(1, CurrentPage - (maxPagesToDisplay / 2));
//         int endPage = Math.Min(PageCount, startPage + maxPagesToDisplay - 1);

//         if (endPage - startPage < maxPagesToDisplay - 1)
//         {
//             startPage = Math.Max(1, endPage - maxPagesToDisplay + 1);
//         }

//         return Enumerable.Range(startPage, endPage - startPage + 1);
//     }
// }