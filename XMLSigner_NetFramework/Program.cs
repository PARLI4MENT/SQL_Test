﻿#define TEST
///5.23.0/3.4.16

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SQLNs;
using XmlFTS;
using XmlFTS.OutClass;
using XMLSigner;

namespace XMLSigner
{
    public static class Program
    {       
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            //new PgSql().PgRetriveData("BD2D10AB-2871-4155-8F0A-2CE896EA880F", "BD2D10AB-2871-4155-8F0A-2CE896EA880F", "Общая ошибка при работе системы");
            //new PgSql().PgRetriveData("BD2D10AB-2871-4155-8F0A-2CE896EA880F", "BD2D10AB-2871-4155-8F0A-2CE896EA880F", "Общая ошибка при работе системы");
            //new PgSql().PgSqlCreateDatabase(true);

            Config.BaseConfiguration("C:\\Test");
            Config.DeleteSourceFiles = true;
            Config.EnableBackup = true;
            Config.ReadAllSetting();

            //await ProcessHostXML.CreateHostBuilder(args).RunConsoleAsync();

            //var host = new HostBuilder()
            //       .ConfigureHostConfiguration(hConfig => { })
            //       .ConfigureServices((context, services) =>
            //       {
            //           services.AddHostedService<FileSystemWatcherService>();
            //           services.AddHostedService<ReplyProcessTick>();
            //       })
            //       .UseConsoleLifetime()
            //       .Build();

            //await host.RunAsync();

            string MchdId = "719f90af-f777-4c70-9a33-053958eacc65";
            string MchdINN = "2536287574";
            X509Certificate2 cert = SignXmlGost.FindGostCurrentCertificate("01DAFCE9BC8E41B000087F5E381D0002");

            TemplatingXml.CreateArchive(MchdId, MchdINN, cert, "NewArch_12");
            Console.WriteLine("");
        }
    }
}