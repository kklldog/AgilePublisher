using AgilePublisher.Application;
using AgilePublisher.Markdown;
using AgilePublisher.Models;
using System.CommandLine;

var platformOption = new Option<string>(name: "--platform", description: "Target platform (zhihu)", getDefaultValue: () => "zhihu");
var markdownOption = new Option<string>(name: "--markdown", description: "Markdown file to publish") { IsRequired = true };
var titleOption = new Option<string?>(name: "--title", description: "Override article title");
var coverOption = new Option<string?>(name: "--cover-image", description: "Optional cover image path");
var tagOption = new Option<string[]>(name: "--tag", description: "Tags to apply", getDefaultValue: Array.Empty<string>);
var publishOption = new Option<bool>(name: "--publish", description: "Publish instead of saving draft", getDefaultValue: () => false);
var userDataDirOption = new Option<string?>(name: "--user-data-dir", description: "Playwright persistent context directory");
var headlessOption = new Option<bool>(name: "--headless", description: "Run browser headless", getDefaultValue: () => true);
var slowMoOption = new Option<float>(name: "--slow-mo", description: "Playwright slow motion delay (ms)", getDefaultValue: () => 0);
var timeoutOption = new Option<int?>(name: "--launch-timeout", description: "Browser launch timeout (ms)");
var zhihuScriptOption = new Option<string?>(name: "--zhihu-script", description: "Path to Zhihu publish script assembly (C#)");

var rootCommand = new RootCommand("Publish Markdown articles using Playwright automation");
rootCommand.AddOption(platformOption);
rootCommand.AddOption(markdownOption);
rootCommand.AddOption(titleOption);
rootCommand.AddOption(coverOption);
rootCommand.AddOption(tagOption);
rootCommand.AddOption(publishOption);
rootCommand.AddOption(userDataDirOption);
rootCommand.AddOption(headlessOption);
rootCommand.AddOption(slowMoOption);
rootCommand.AddOption(timeoutOption);
rootCommand.AddOption(zhihuScriptOption);

rootCommand.SetHandler(async (platform, markdownPath, title, coverImage, tags, publish, userDataDir, headless, slowMo, timeout, zhihuScriptPath) =>
{
    var content = MarkdownLoader.Load(markdownPath, title);
    var request = new PublishRequest(content, tags, coverImage, publish);
    var settings = new PlaywrightSettings(headless, userDataDir, slowMo, timeout);

    var service = new PublisherService(settings, zhihuScriptPath: zhihuScriptPath);
    await service.PublishAsync(platform, request);
}, platformOption, markdownOption, titleOption, coverOption, tagOption, publishOption, userDataDirOption, headlessOption, slowMoOption, timeoutOption, zhihuScriptOption);

return await rootCommand.InvokeAsync(args);
