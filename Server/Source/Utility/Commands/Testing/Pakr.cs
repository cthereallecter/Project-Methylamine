using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ProjectMethylamine.Source.Utility.Cryptography;

namespace ProjectMethylamine.Source.Utility.Commands.Testing
{
    internal class PakrCommand : ICommand
    {         
        private const string DEFAULT_ENCRYPTION_KEY = "PAKR_Key_2025";

        public void Execute(ConsoleLogger logger, string input)
        {
            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                              .SkipWhile(t => t.Equals("pakr", StringComparison.OrdinalIgnoreCase))
                              .ToArray();

            if (tokens.Length < 2 || tokens.Contains("-h"))
            {
                ShowHelp(logger);
                return;
            }

            bool overwrite = !tokens.Contains("-o");
            bool encrypt = tokens.Contains("/E");
            bool decrypt = tokens.Contains("/e");

            if (tokens.Contains("-p") && tokens.Contains("/D"))
            {
                var nonFlagTokens = tokens
                    .Where(t => !t.StartsWith('-') && !t.Equals("/D", StringComparison.Ordinal) && !t.Equals("/E", StringComparison.Ordinal) && !t.Equals("/e", StringComparison.Ordinal))
                    .ToArray();

                if (nonFlagTokens.Length < 3)
                {
                    logger.Log("PAKR", "Invalid pack syntax. Use: pakr -p /D [/E] <source> <dest> <name>");
                    return;
                }

                string sourceDir = nonFlagTokens[0];
                string destDir = nonFlagTokens[1];
                string zipName = nonFlagTokens[2];

                if (sourceDir.Contains('*'))
                {
                    HandleWildcardPack(logger, sourceDir, destDir, overwrite, encrypt);
                }
                else
                {
                    Pack(logger, sourceDir, destDir, zipName, overwrite, encrypt);
                }
            }
            else if (tokens.Contains("-u") && tokens.Contains("/D"))
            {
                var nonFlagTokens = tokens
                    .Where(t => !t.StartsWith('-') && !t.Equals("/D", StringComparison.Ordinal) && !t.Equals("/E", StringComparison.Ordinal) && !t.Equals("/e", StringComparison.Ordinal))
                    .ToArray();

                if (nonFlagTokens.Length < 2)
                {
                    logger.Log("PAKR", "Invalid unpack syntax. Use: pakr -u /D [/e] <archive> <dest>");
                    return;
                }

                string archivePath = nonFlagTokens[0];
                string destDir = nonFlagTokens[1];

                Unpack(logger, archivePath, destDir, decrypt);
            }
            else
            {
                logger.Log("PAKR", "Invalid syntax. Use 'pakr -h' for help.");
            }
        }
        public void ShowHelp(ConsoleLogger logger)
        {
            logger.Log("USAGE", "pakr -p /D [/E] <source_dir> <dest_dir> <zip_name>    : Pack directory into ZIP");
            logger.Log("USAGE", "pakr -u /D [/e] <archive_path> <dest_dir>             : Unpack archive to directory");
            logger.Log("USAGE", "Options:");
            logger.Log("USAGE", "  -o                : Prevent overwrite if archive exists");
            logger.Log("USAGE", "  /E                : Encrypt archive after packing");
            logger.Log("USAGE", "  /e                : Decrypt archive before unpacking");
            logger.Log("USAGE", "  * Wildcards       : Use * to pack multiple folders (e.g., Mods/*)");
            logger.Log("USAGE", "  -h                : Show this help message");
            logger.Log("USAGE", "Examples:");
            logger.Log("USAGE", "  pakr -p /D /E Content/ Data/ Core             : Pack and encrypt");
            logger.Log("USAGE", "  pakr -p /D Content/ Data/ Core                : Pack without encryption");
            logger.Log("USAGE", "  pakr -u /D /e Data/Core.pakr Output/          : Decrypt and unpack");
        }
        private static void Pack(ConsoleLogger logger, string sourceDir, string destDir, string zipName, bool overwrite, bool encrypt = false)
        {
            logger.Log("PAKR", $"Packing: {sourceDir} -> {Path.Combine(destDir, zipName)}{(encrypt ? " (Encrypted)" : "")}");

            if (!Directory.Exists(sourceDir))
            {
                logger.Log("PAKR", $"Source directory not found: {sourceDir}");
                return;
            }

            try
            {
                Directory.CreateDirectory(destDir);
                string finalExtension = encrypt ? ".pakr" : ".zip";
                string baseZipName = zipName.EndsWith(".zip") || zipName.EndsWith(".pakr")
                                     ? Path.GetFileNameWithoutExtension(zipName)
                                     : zipName;
                string fullZipPath = Path.Combine(destDir, $"{baseZipName}{finalExtension}");

                if (File.Exists(fullZipPath))
                {
                    if (overwrite)
                    {
                        logger.Log("PAKR", $"Overwriting existing archive: {fullZipPath}");
                        File.Delete(fullZipPath);
                    }
                    else
                    {
                        logger.Log("PAKR", $"Archive exists and overwrite not allowed (-o). Skipping.");
                        return;
                    }
                }

                // Create manifest
                string manifestPath = Path.Combine(sourceDir, "manifest.pakr.json");
                var manifest = new
                {
                    name = baseZipName,
                    packedAt = DateTime.UtcNow.ToString("o"),
                    source = sourceDir,
                    version = "1.0.0",
                    encrypted = encrypt,
                    files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories)
                                     .Select(f => f.Substring(sourceDir.Length).TrimStart(Path.DirectorySeparatorChar))
                                     .ToArray()
                };
                File.WriteAllText(manifestPath, JsonSerializer.Serialize(manifest, new JsonSerializerOptions { WriteIndented = true }));

                string tempZipPath = encrypt ? Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".zip") : fullZipPath;

                try
                {
                    // Create the ZIP archive
                    ZipFile.CreateFromDirectory(sourceDir, tempZipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

                    File.Delete(manifestPath); // Clean up

                    if (encrypt)
                    {
                        logger.Log("PAKR", "Encrypting archive...");
                        byte[] zipData = File.ReadAllBytes(tempZipPath);
                        byte[] encryptedData = BitShiftCrypto.Encrypt(zipData, DEFAULT_ENCRYPTION_KEY);
                        File.WriteAllBytes(fullZipPath, encryptedData);
                        logger.Log("PAKR", "Archive encrypted successfully");
                    }

                    logger.Log("PAKR", $"Packed successfully: {fullZipPath}");

                    // Clear the source folder after successful packing
                    logger.Log("PAKR", $"Clearing source directory: {sourceDir}");
                    foreach (var file in Directory.GetFiles(sourceDir))
                        File.Delete(file);
                    foreach (var dir in Directory.GetDirectories(sourceDir))
                        Directory.Delete(dir, true);
                }
                finally
                {
                    if (encrypt && File.Exists(tempZipPath))
                    {
                        try
                        {
                            File.Delete(tempZipPath);
                        }
                        catch (InvalidOperationException cleanupEx)
                        {
                            logger.Log("PAKR", $"Warning: Failed to delete temp file: {cleanupEx.Message}");
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                logger.Log("PAKR", $"Packing failed: {ex.Message}");
            }
        }
        private static void Unpack(ConsoleLogger logger, string archivePath, string destDir, bool decrypt = false)
        {
            logger.Log("PAKR", $"Unpacking: {archivePath} -> {destDir}{(decrypt ? " (Decrypting)" : "")}");

            if (!File.Exists(archivePath))
            {
                logger.Log("PAKR", $"Archive not found: {archivePath}");
                return;
            }

            try
            {
                Directory.CreateDirectory(destDir);
                string workingArchivePath = archivePath;

                if (decrypt)
                {
                    logger.Log("PAKR", "Decrypting archive...");
                    byte[] encryptedData = File.ReadAllBytes(archivePath);
                    byte[] decryptedData = BitShiftCrypto.Decrypt(encryptedData, DEFAULT_ENCRYPTION_KEY);
                    workingArchivePath = Path.GetTempFileName();
                    File.WriteAllBytes(workingArchivePath, decryptedData);
                    logger.Log("PAKR", "Archive decrypted successfully");
                }

                ZipFile.ExtractToDirectory(workingArchivePath, destDir, overwriteFiles: true);

                if (decrypt && workingArchivePath != archivePath)
                {
                    File.Delete(workingArchivePath);
                }

                string manifestPath = Path.Combine(destDir, "manifest.pakr.json");
                if (File.Exists(manifestPath))
                {
                    try
                    {
                        string manifestJson = File.ReadAllText(manifestPath);
                        var manifest = JsonSerializer.Deserialize<JsonElement>(manifestJson);

                        if (manifest.TryGetProperty("name", out var nameElement))
                            logger.Log("PAKR", $"Archive name: {nameElement.GetString()}");
                        if (manifest.TryGetProperty("packedAt", out var dateElement))
                            logger.Log("PAKR", $"Packed at: {dateElement.GetString()}");
                        if (manifest.TryGetProperty("files", out var filesElement) && filesElement.ValueKind == JsonValueKind.Array)
                            logger.Log("PAKR", $"Files extracted: {filesElement.GetArrayLength()}");
                    }
                    catch (InvalidDataException manifestEx)
                    {
                        logger.Log("PAKR", $"Warning: Could not read manifest: {manifestEx.Message}");
                    }
                }

                logger.Log("PAKR", $"Unpacked successfully: {destDir}");
            }
            catch (InvalidOperationException ex)
            {
                logger.Log("PAKR", $"Unpacking failed: {ex.Message}");
            }
        }
        private static void HandleWildcardPack(ConsoleLogger logger, string wildcardSource, string destDir, bool overwrite, bool encrypt = false)
        {
            string parent = Path.GetDirectoryName(wildcardSource)!;
            string pattern = Path.GetFileName(wildcardSource);

            var matches = Directory.GetDirectories(parent, pattern);
            if (matches.Length == 0)
            {
                logger.Log("PAKR", $"No directories matched wildcard: {wildcardSource}");
                return;
            }

            foreach (var dir in matches)
            {
                string name = Path.GetFileName(dir);
                Pack(logger, dir, destDir, name, overwrite, encrypt);
            }
        }
    }
}
