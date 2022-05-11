using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.Web;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Shared
{
    public static class ExtensionsMethods
    {
        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }

        public static string GetDescription(this System.Enum value)
        {
            var description = value.ToString();
            var attribute = GetAttribute<System.ComponentModel.DescriptionAttribute>(value);
            if (attribute != null)
            {
                description = attribute.Description;
            }
            return description;
        }

        public static T GetAttribute<T>(System.Enum value) where T : System.Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = ((T[])field.GetCustomAttributes(typeof(T), false)).FirstOrDefault();
            return attribute;
        }
    }
}
