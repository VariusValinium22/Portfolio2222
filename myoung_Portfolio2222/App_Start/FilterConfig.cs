using System.Web;
using System.Web.Mvc;

namespace myoung_Portfolio2222
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
