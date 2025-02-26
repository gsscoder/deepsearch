using System;
using System.Collections.Generic;
using System.Text;
using PuppeteerSharp;

namespace DeepSearch;

public abstract class SearchAgent(ServiceResolver resolver)
{
    protected Lazy<Task<IBrowser>> Browser { get => new(async () => await LaunchBrowserAsync()); }
    protected ServiceResolver Resolver { get => resolver; }

    static async Task<IBrowser> LaunchBrowserAsync()
    {
        var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        var browser = await Puppeteer.LaunchAsync(
            new LaunchOptions
            {
                Args = new[] { "--disable-blink-features=AutomationControlled" },
                Headless = true
            });
        return browser;
    }
}
