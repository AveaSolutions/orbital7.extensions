using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Mvc
{
    public class ControllerEx : Controller
    {
        public FileContentResult FileInline(byte[] fileContents, string contentType, string fileName)
        {
            throw new NotImplementedException(); 
            //this.Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName.Replace(",", ""));
            return File(fileContents, contentType);
        }

        public static SelectList CreateEmptySelectList(string emptyText)
        {
            return new List<Tuple<Guid, string>> { new Tuple<Guid, string>(Guid.Empty, emptyText) }.ToSelectList();
        }

        public SelectList GetEmptySelectList(string emptyText)
        {
            return CreateEmptySelectList(emptyText);
        }

        public ContentResult ClientNavigateTo(string url)
        {
            return Content(GetNavigateJScript(url));
        }

        public static string GetNavigateJScript(string url)
        {
            return "window.location = '" + url + "';";
        }

        public static Stream GetPostedFileStream(IFormFile postedFile)
        {
            if (postedFile != null)
                return postedFile.OpenReadStream(); 
            else
                return null;
        }

        public static byte[] GetPostedFileContents(IFormFile postedFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                if (postedFile == null)
                    return null;

                postedFile.CopyTo(memoryStream); 
                return memoryStream.ToArray();
            }
        }

        public Guid GetUserID()
        {
            return this.User.Identity.GetUserId(); 
        }
    }
}
