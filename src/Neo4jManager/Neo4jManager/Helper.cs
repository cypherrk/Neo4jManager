﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace Neo4jManager
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Helper
    {
        public static string FindJavaExe()
        {
            const string javaRoot = @"C:\Program Files\Java";
            return Directory.GetFiles(javaRoot, "java.exe", SearchOption.AllDirectories)
                .ToList().OrderByDescending(p => p).FirstOrDefault();
        }

        public static void Download(Neo4jVersion neo4jVersion, string neo4jBasePath)
        {
            var zipFile = Path.Combine(neo4jBasePath, neo4jVersion.ZipFileName);

            if (File.Exists(zipFile)) return;

            var fileInfo = new FileInfo(zipFile);
            Directory.CreateDirectory(fileInfo.DirectoryName);

            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(neo4jVersion.DownloadUrl, zipFile);
            }
        }

        public static void Extract(Neo4jVersion neo4jVersion, string neo4jBasePath)
        {
            var zipFile = Path.Combine(neo4jBasePath, neo4jVersion.ZipFileName);
            var extractFolder = Path.Combine(neo4jBasePath, Path.GetFileNameWithoutExtension(neo4jVersion.ZipFileName));

            if (Directory.Exists(extractFolder)) return;

            ZipFile.ExtractToDirectory(zipFile, extractFolder);
        }

        public static void CopyDeployment(Neo4jVersion neo4jVersion, string neo4jBasePath, string targetDeploymentPath)
        {
            var extractFolder = Path.Combine(neo4jBasePath, Path.GetFileNameWithoutExtension(neo4jVersion.ZipFileName));

            new FileCopy().MirrorFolders(extractFolder, targetDeploymentPath);
        }

    }
}