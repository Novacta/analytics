// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public static class AnalyticsTests
    {
        private static void DeployNativeAssets(string runtimeIdentifier)
        {
            string assemblyFolderPath = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            string runtimeRelativePath = Path.Combine(
                "runtimes",
                runtimeIdentifier,
                "native");

            string runtimeAbsolutePath = Path.Combine(
                assemblyFolderPath,
                runtimeRelativePath);

            var files = Directory.GetFiles(runtimeAbsolutePath);

            foreach (var file in files)
            {
                File.Copy(
                    file,
                    Path.Combine(
                        assemblyFolderPath,
                        Path.GetFileName(file)),
                    overwrite: true);
            }
        }

        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext _)
        {
            #region OSX

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                switch (RuntimeInformation.ProcessArchitecture)
                {
                    case Architecture.X64:
                        DeployNativeAssets("osx-x64");
                        return;
                    default:
                        throw new PlatformNotSupportedException();
                }
            }

            #endregion

            #region Linux

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                switch (RuntimeInformation.ProcessArchitecture)
                {
                    case Architecture.X64:
                        DeployNativeAssets("linux-x64");
                        return;
                    default:
                        throw new PlatformNotSupportedException();
                }
            }

            #endregion

            #region Windows

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                switch (RuntimeInformation.ProcessArchitecture)
                {
                    case Architecture.X64:
                        DeployNativeAssets("win-x64");
                        break;
                    case Architecture.X86:
                        DeployNativeAssets("win-x86");
                        break;
                    default:
                        throw new PlatformNotSupportedException();
                }
            }

            #endregion
        }
    }
}
