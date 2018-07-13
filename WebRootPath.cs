public async Task<string> GetFooterTemplate(string template)
{            
    string content = null;
    string filePath = $"{HostingEnvironment.WebRootPath}/public/common/partials/{template}.html";
    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
    {
        using (StreamReader reader = new StreamReader(fileStream))
        {
            content = await reader.ReadToEndAsync();
        }
    }

    return content;
}
