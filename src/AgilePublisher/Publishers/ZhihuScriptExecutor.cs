using AgilePublisher.Models;
using Microsoft.Playwright;
using System.Text.Json;

namespace AgilePublisher.Publishers;

public sealed class ZhihuScriptExecutor
{
    private readonly IReadOnlyList<ZhihuScriptStep> _steps;

    public ZhihuScriptExecutor(string scriptPath)
    {
        if (!File.Exists(scriptPath))
        {
            throw new FileNotFoundException($"Zhihu script not found: {scriptPath}");
        }

        using var stream = File.OpenRead(scriptPath);
        var steps = JsonSerializer.Deserialize<List<ZhihuScriptStep>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        _steps = steps ?? throw new InvalidOperationException($"Could not parse Zhihu script: {scriptPath}");
    }

    public async Task ExecuteAsync(IPage page, ArticleContent content, CancellationToken cancellationToken)
    {
        foreach (var step in _steps)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var value = ApplyPlaceholders(step.Value, content);

            switch (step.Action.ToLowerInvariant())
            {
                case "goto":
                    EnsureValue(step.Action, value);
                    await page.GotoAsync(value!, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });
                    break;
                case "waitforselector":
                    EnsureSelector(step.Action, step.Selector);
                    await page.WaitForSelectorAsync(step.Selector!, new PageWaitForSelectorOptions { Timeout = 15000 });
                    break;
                case "click":
                    EnsureSelector(step.Action, step.Selector);
                    await page.ClickAsync(step.Selector!);
                    break;
                case "fill":
                    EnsureSelector(step.Action, step.Selector);
                    EnsureValue(step.Action, value);
                    await page.FillAsync(step.Selector!, value!);
                    break;
                default:
                    throw new InvalidOperationException($"Unsupported Zhihu script action: {step.Action}");
            }
        }
    }

    private static string? ApplyPlaceholders(string? value, ArticleContent content)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return value
            .Replace("{{title}}", content.Title)
            .Replace("{{body}}", content.Body);
    }

    private static void EnsureSelector(string action, string? selector)
    {
        if (string.IsNullOrWhiteSpace(selector))
        {
            throw new InvalidOperationException($"Missing selector for action '{action}' in Zhihu script");
        }
    }

    private static void EnsureValue(string action, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Missing value for action '{action}' in Zhihu script");
        }
    }
}
