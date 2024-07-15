namespace TovarischAndruha.Summary.Web.Extensions;

public static class SvelteSpaExtensions {
  public static void UseSvelteSpaRouting(this WebApplication app, string[]? exclude = null) {
    app.Use(async (context, next) => {
      var url = context.Request.Path;

      if (context.Request.Headers.TryGetValue("Accept", out var accept) &&
          accept.ToString().StartsWith("text/html") &&
          (exclude == null || !exclude.Any(x => x.StartsWith(url)))) {
        context.Request.Path = new PathString(url + ".html");
      }

      await next();
    });
  }
}