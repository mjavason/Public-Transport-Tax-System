using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

public class RazorViewToStringRenderer
{
	private readonly IRazorViewEngine _razorViewEngine;
	private readonly ITempDataProvider _tempDataProvider;
	private readonly IServiceProvider _serviceProvider;

	public RazorViewToStringRenderer(
		IRazorViewEngine razorViewEngine,
		ITempDataProvider tempDataProvider,
		IServiceProvider serviceProvider)
	{
		_razorViewEngine = razorViewEngine;
		_tempDataProvider = tempDataProvider;
		_serviceProvider = serviceProvider;
	}

	public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
	{
		var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
		var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

		// Try to get the view explicitly
		var viewResult = _razorViewEngine.GetView("~/Views/", viewName, false);
		if (!viewResult.Success)
		{
			viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
			if (!viewResult.Success)
			{
				throw new InvalidOperationException($"Unable to find view '{viewName}'. Make sure the file exists in 'Views/Emails/'.");
			}
		}

		var view = viewResult.View;
		using var sw = new StringWriter();
		var viewContext = new ViewContext(
			actionContext, view, new ViewDataDictionary<TModel>(
			new EmptyModelMetadataProvider(), new ModelStateDictionary())
			{
				Model = model
			},
			new TempDataDictionary(httpContext, _tempDataProvider),
			sw, new HtmlHelperOptions());

		await view.RenderAsync(viewContext);
		return sw.ToString();
	}
}
