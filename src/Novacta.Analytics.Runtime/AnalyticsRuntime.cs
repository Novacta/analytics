using System.Reflection;
using System.Runtime.InteropServices;

namespace Novacta.Analytics.Runtime
{
    /// <summary>
    /// Provides methods to deploy native assets for the 
    /// Novacta.Analytics runtime.
    /// </summary>
    public static class AnalyticsRuntime
    {
        /// <summary>
        /// Deploys the native assets of the runtime required
        /// by the current operating system platform.
        /// </summary>
        /// <seealso cref="OSPlatform"/>
        /// <seealso cref="DeployNativeAssets(string)"/>
        public static void Deploy()
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
                    default:
                        throw new PlatformNotSupportedException();
                }
            }

            #endregion
        }

        /// <summary>
        /// Deploys the native assets for a specific runtime.
        /// </summary>
        /// <param name="runtimeIdentifier">
        /// The identifier of the runtime for which to deploy the native 
        /// assets.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the assembly folder path is null.
        /// </exception>        
        public static void DeployNativeAssets(string runtimeIdentifier)
        {
            string? assemblyFolderPath =
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location)
                ??
                throw new InvalidOperationException(
                    "The assembly folder path is null.");

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

    }
}
