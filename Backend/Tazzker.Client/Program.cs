using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Tazzker.Client.Data.LocalDb;
using Tazzker.Client.Data.Models;
using Tazzker.Client.Interfaces;
using Tazzker.Client.Services;

namespace Tazzker.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);


			builder.Services.AddScoped<IndexedDbBridge>();

            builder.Services.AddScoped<ILocalDb<ListModel>, ListLocalDb>();
            builder.Services.AddScoped<ILocalDb<NoteModel>, NoteLocalDb>();
			builder.Services.AddScoped<ILocalDb<SublistModel>, SublistLocalDb>();
            builder.Services.AddScoped<ILocalDb<TaskModel>, TaskLocalDb>();

            builder.Services.AddScoped<SyncService>();
            builder.Services.AddScoped<ModelMethods>();
            builder.Services.AddSingleton<SyncStatusService>();


            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


			var jsRuntime = (IJSRuntime)builder.Services.BuildServiceProvider().GetRequiredService<IJSRuntime>();

			var apiBase = await jsRuntime.InvokeAsync<string>("getApiBaseUrl");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });

			await jsRuntime.InvokeVoidAsync("indexedDbBridge.safeInitDb", "TazzkerDb", 1, new[] { "Lists", "Sublists", "Tasks", "Notes" });


            await builder.Build().RunAsync();
        }
    }
}
