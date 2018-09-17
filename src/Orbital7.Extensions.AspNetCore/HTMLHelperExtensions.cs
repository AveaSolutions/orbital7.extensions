using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Orbital7.Extensions;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HTMLHelperExtensions
    {
        public static HtmlString EncodedReplace(this IHtmlHelper helper, string input, string pattern, string replacement)
        {
            return new HtmlString(Regex.Replace(helper.Encode(input), pattern, replacement));
        }

        public static HtmlString DisplayCurrency(this IHtmlHelper helper, double? number, bool addSymbol = false, bool blankIfZero = false)
        {
            if (number.HasValue && (number.Value != 0 || !blankIfZero))
                return new HtmlString("<span style='white-space: nowrap;'>" + number.Value.ToCurrency(addSymbol) + "</span>");
            else
                return new HtmlString(String.Empty);
        }

        public static IHtmlContent NumberEditorFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            //var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IDictionary<string, object> attributes = ToAttributesDictionary(htmlAttributes);
            RangeAttribute rangeAttribute = modelExplorer.Metadata.ContainerType.GetPropertyAttribute<RangeAttribute>(modelExplorer.Metadata.PropertyName);
            if (rangeAttribute != null)
            {
                attributes.Add("type", "number");
                attributes.Add("min", rangeAttribute.Minimum);
                attributes.Add("max", rangeAttribute.Maximum);
            }

            return htmlHelper.TextBoxFor(expression, attributes);
        }

        public static HtmlString EnumSortedDropDownListFor<TModel>(this IHtmlHelper<TModel> htmlHelper, string expression, Type type, object htmlAttributes = null)
        {
            throw new NotImplementedException();
        }

        public static HtmlString EnumSortedDropDownListFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string defaultValue, object htmlAttributes = null)
        {
            throw new NotImplementedException();
        }

        public static HtmlString EnumDropDownListFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            throw new NotImplementedException();
        }

        public static HtmlString EnumDropDownListFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes = null)
        {
            throw new NotImplementedException();
        }

        public static IHtmlContent EnumSortedDropDownListFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes = null)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);

            ModelMetadata metadata = modelExplorer.Metadata;
            var selectList = htmlHelper.GetEnumSelectList(metadata.ModelType).OrderBy(i => i.Text).ToList();

            return htmlHelper.DropDownListFor(expression, selectList, ToAttributesDictionary(htmlAttributes));
        }

        public static IHtmlContent NullableEnumSortedDropDownListFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string nullText, object htmlAttributes = null)
        {
            var modelExplorer = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);
            var selectList = htmlHelper.GetEnumSelectList(modelExplorer.ModelType).OrderBy(i => i.Text).ToList();
            selectList.First().Text = nullText;

            return htmlHelper.DropDownListFor(expression, selectList, ToAttributesDictionary(htmlAttributes));
        }

        internal static IDictionary<string, object> ToAttributesDictionary(object htmlAttributes)
        {
            IDictionary<string, object> attributes = null;

            if (htmlAttributes == null)
                attributes = new RouteValueDictionary();
            else
                attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return attributes;
        }
        public static IHtmlContent LabelAlwaysRequired(this IHtmlHelper htmlHelper, string label)
        {
            return htmlHelper.Label(label);
        }

        public static IHtmlContent LabelForRequired<TModel, TProp>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProp>> expression)
        {
            return htmlHelper.LabelFor(expression);
        }

        public static IHtmlContent LabelForRequired<TModel, TProp>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProp>> expression, bool input)
        {
            return htmlHelper.LabelForRequired(expression, input);
        }

        public static IHtmlContent LabelForRequired<TModel, TProp>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProp>> expression, bool input, object value)
        {
            return htmlHelper.LabelForRequired(expression, input, value);
        }

        public static string GetFullHtmlFieldId<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            throw new NotImplementedException();
            //string tempValue = ExpressionHelper.GetExpressionText(expression);
            //return TagBuilder.CreateSanitizedId
            //    (html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(tempValue));
        }
    }
}
