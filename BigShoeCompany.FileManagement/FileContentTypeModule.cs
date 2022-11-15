using BigShoeCompany.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.StaticFiles;
using System.ComponentModel;

namespace BigShoeCompany.FileManagement
{
    public enum FileContentType
    {
        [Description("image/png")]
        Png = 1,
        [Description("image/jpeg")]
        Jpg,
        [Description("application/msword")]
        Doc,
        [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
        Docx,
        [Description("text/plain")]
        Txt,
        [Description("application/octet-stream")]
        Binary,
        [Description("text/xml")]
        Xml,
        [Description("application/pdf")]
        Pdf,
        [Description("application/vnd.ms-excel")]
        Xls,
        [Description("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        Xlsx,
    }

    public static class FileContentTypeModule
    {
        public static readonly FileContentType[] AcceptedFileTypeOrder = new FileContentType[] {
            FileContentType.Xml
        };

        public static void ValidateContentType(string fileName, params FileContentType[] contentTypes)
        {
            var fileProvider = new FileExtensionContentTypeProvider();
            List<string> attributes = new List<string>();
            foreach (var type in contentTypes)
            {
                var attribute = (DescriptionAttribute[])type.GetType()
               .GetField(type.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribute != null && attribute.Length > 0) attributes.Add(attribute[0].Description);
            }
            if (!fileProvider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            var valid = attributes.Contains(contentType);
            if (!valid)
            {
                string errorMessage = "Uploaded format not accepted.";
                if (contentTypes?.Length > 1)
                {
                    errorMessage = $"You may only upload {GetTextListOfAcceptedExtensions(contentTypes)} files. Please ensure your file is in one of these formats.";
                }
                else if (contentTypes?.FirstOrDefault() != null)
                {
                    errorMessage = $"You may only upload {contentTypes.First()} files.";
                }
                throw new ApiException(errorMessage);
            }
        }

        private static string GetTextListOfAcceptedExtensions(params FileContentType[] contentTypes)
        {
            string extensions = string.Join(", ", contentTypes);
            extensions = extensions.ToLower();
            return extensions;
        }
    }
}