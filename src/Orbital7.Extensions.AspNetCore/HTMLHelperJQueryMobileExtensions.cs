using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class IHtmlHelperJQueryMobileExtensions
    {
        public static IHtmlContent DateEditorFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool includeYear, object htmlAttributes = null)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, IHtmlHelper.ViewData);
            DateTime? date = (DateTime?)(modelExplorer.Model);

            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (date != null && date.HasValue)
                attributes.Add("Value", date.Value.ToShortDateString());

            if (includeYear)
                attributes.Add(new KeyValuePair<string, object>("class", "inputDateWithYear"));
            else
                attributes.Add(new KeyValuePair<string, object>("class", "inputDate"));

            return htmlHelper.TextBoxFor(expression, attributes);
        }

        public static IHtmlContent FlipSwitchFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string textTrue, string textFalse, object htmlAttributes = null)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, IHtmlHelper.ViewData);
            bool value = (bool)(modelExplorer.Model ?? false);

            List<SelectListItem> items =
                new List<SelectListItem>()
                    {
                        new SelectListItem() { Text = textFalse, Value = "False", Selected = (!value) },
                        new SelectListItem() { Text = textTrue, Value = "True", Selected = (value) }
                    };

            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            attributes.Add(new KeyValuePair<string, object>("data-role", "slider"));
            attributes.Add(new KeyValuePair<string, object>("data-mini", "true"));

            return htmlHelper.DropDownListFor(expression, items, attributes);
        }

        public static IHtmlContent FlipSwitchYesNoFor<TModel, TProperty>(this IHtmlHelper<TModel> IHtmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return FlipSwitchFor(IHtmlHelper, expression, "Yes", "No", htmlAttributes);
        }

        public static IHtmlContent FlipSwitchOnOffFor<TModel, TProperty>(this IHtmlHelper<TModel> IHtmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return FlipSwitchFor(IHtmlHelper, expression, "On", "Off", htmlAttributes);
        }

        public static IHtmlContent FlipSwitchTrueFalseFor<TModel, TProperty>(this IHtmlHelper<TModel> IHtmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return FlipSwitchFor(IHtmlHelper, expression, "True", "False", htmlAttributes);
        }
    }
}
