﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using XmlFTS;
using XmlFTS.OutClass;
using static BgServices.WatchBaseFolder;

namespace BgServices
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host1 = new HostBuilder()
                .ConfigureHostConfiguration(hconfig => { })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<WatchBaseFolder>();
                    services.AddHostedService<MyProcessor2>();
                })
                .UseConsoleLifetime().Build();

            await host1.RunAsync();
        }
    }

    /// <summary> Отслеживание начальной папки</summary>
    public class WatchBaseFolder : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var rawSrcFolders = Directory.GetDirectories(StaticPathConfiguration.PathExtractionFolder);

                if (rawSrcFolders != null)
                {
                    foreach (var rawSrcFolder in rawSrcFolders)
                    {
                        int SummaryFiles = 0;

                        /// For statistics
                        SummaryFiles += rawSrcFolder.Count();

                        ///// #3 Сортировка
                        //SortXml(Directory.GetFiles(StaticPathConfiguration.PathExtractionFolder, "*.xml"));
                    }
                    Debug.WriteLine("Process main done!");
                }

            }
        }

        /// <summary>Отслеживание приходящих файлов</summary>
        public class MyProcessor2 : BackgroundService
        {
            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    /// Сюда
                    Console.WriteLine($"MyProcessor2: {DateTime.Now}");
                    await Task.Delay(500);
                }
                return;
            }
        }
    }
}
