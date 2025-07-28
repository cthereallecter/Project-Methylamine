using System;
using System.IO;
using System.Linq;
using System.Text;
using ProjectMethylamine.Source.Maps;
using ProjectMethylamine.Source.Utility;

namespace ProjectMethylamine.Source.Utility.Commands.Testing
{
    public class MaprCommand : ICommand
    {
        private const string MapFolder = "Content/Maps/";
        private const string MapClassFolder = MapFolder;

        public void Execute(ConsoleLogger logger, string input)
        {
            var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length < 2 || tokens.Contains("-h"))
            {
                ShowHelp(logger);
                return;
            }

            var operation = tokens[1];

            switch (operation)
            {
                case "-c":
                    if (tokens.Length < 4)
                    {
                        logger.Log("MAPR", "Usage: mapr -c [/S] <mapname> <size>");
                        return;
                    }

                    bool createSeasonal = tokens[2] == "/S";
                    int nameIndex = createSeasonal ? 3 : 2;
                    int sizeIndex = createSeasonal ? 4 : 3;

                    if (tokens.Length < (createSeasonal ? 5 : 4))
                    {
                        logger.Log("MAPR", createSeasonal ? "Usage: mapr -c /S <mapname> <size>" : "Usage: mapr -c <mapname> <size>");
                        return;
                    }

                    string newMapName = ConvertDotsToUnderscores(tokens[nameIndex]);
                    if (!int.TryParse(tokens[sizeIndex], out int newSize) || newSize <= 0)
                    {
                        logger.Log("MAPR", "Invalid size. Size must be a positive integer.");
                        return;
                    }

                    if (createSeasonal)
                    {
                        CreateSeasonalMaps(logger, newMapName, newSize);
                    }
                    else
                    {
                        CreateMap(logger, newMapName, newSize);
                    }
                    break;

                case "-e":
                    if (tokens.Length < 3)
                    {
                        logger.Log("MAPR", "Usage: mapr -e <mapname>");
                        return;
                    }
                    EditMap(logger, ConvertDotsToUnderscores(tokens[2]));
                    break;

                case "-d":
                    if (tokens.Length < 3)
                    {
                        logger.Log("MAPR", "Usage: mapr -d <mapname>");
                        return;
                    }
                    DeleteMap(logger, ConvertDotsToUnderscores(tokens[2]));
                    break;

                case "-L":
                    ListMaps(logger);
                    break;

                default:
                    logger.Log("MAPR", $"Unknown operation: {operation}");
                    ShowHelp(logger);
                    break;
            }
        }

        public void ShowHelp(ConsoleLogger logger)
        {
            logger.Log("USAGE", "mapr -c <mapname> <size>   : Create a new .lmf and update .cs");
            logger.Log("USAGE", "mapr -c /S <mapname> <size>: Create base map + seasonal variants");
            logger.Log("USAGE", "mapr -e <mapname>          : Edit existing .lmf map line by line");
            logger.Log("USAGE", "mapr -d <mapname>          : Delete .lmf and remove from map class");
            logger.Log("USAGE", "mapr -L                    : List all map files");
            logger.Log("USAGE", "mapr -h                    : Show this help message");
            logger.Log("USAGE", "");
            logger.Log("USAGE", "Note: Use dots for variants (nexus.christmas) - stored as nexus_christmas.lmf");
            logger.Log("USAGE", "Seasonal creates: base, base_spring, base_summer, base_autumn, base_winter");
        }

        private void CreateMap(ConsoleLogger logger, string mapName, int size)
        {
            Directory.CreateDirectory(MapFolder);
            string path = Path.Combine(MapFolder, $"{mapName}.lmf");

            if (File.Exists(path))
            {
                logger.Log("MAPR", $"Map '{mapName}.lmf' already exists.");
                return;
            }

            string spacedLine = string.Join(' ', Enumerable.Repeat(".", size));
            var emptyMap = Enumerable.Repeat(spacedLine, size).ToList();
            emptyMap.Insert(0, size.ToString());
            File.WriteAllLines(path, emptyMap);

            logger.Log("MAPR", $"Created map '{mapName}.lmf' with size {size}x{size}.");

            UpdateOrCreateMapClass(logger, mapName);
        }

        private void CreateSeasonalMaps(ConsoleLogger logger, string baseName, int size)
        {
            string[] seasons = { "_spring", "_summer", "_autumn", "_winter" };
            int createdCount = 0;
            int skippedCount = 0;

            Directory.CreateDirectory(MapFolder);
            string spacedLine = string.Join(' ', Enumerable.Repeat(".", size));
            var baseMapLines = Enumerable.Repeat(spacedLine, size).ToList();
            baseMapLines.Insert(0, size.ToString());

            foreach (string season in seasons)
            {
                string mapName = baseName + season;
                string path = Path.Combine(MapFolder, $"{mapName}.lmf");

                if (File.Exists(path))
                {
                    logger.Log("MAPR", $"Map '{mapName}.lmf' already exists. Skipping.");
                    skippedCount++;
                    continue;
                }

                File.WriteAllLines(path, baseMapLines);

                logger.Log("MAPR", $"Created seasonal map '{mapName}.lmf' with size {size}x{size}.");
                UpdateOrCreateMapClass(logger, mapName);
                createdCount++;
            }

            logger.Log("MAPR", $"Seasonal creation complete. Created: {createdCount}, Skipped: {skippedCount}");
        }

        private void EditMap(ConsoleLogger logger, string mapName)
        {
            string path = Path.Combine(MapFolder, $"{mapName}.lmf");
            if (!File.Exists(path))
            {
                logger.Log("MAPR", $"Map file '{mapName}.lmf' not found.");
                return;
            }

            var allLines = File.ReadAllLines(path).ToList();
            if (allLines.Count < 2 || !int.TryParse(allLines[0], out int declaredSize) || declaredSize <= 0)
            {
                logger.Log("MAPR", "Invalid map file format: missing or bad size line.");
                return;
            }

            var originalLines = allLines.Skip(1).ToList();
            if (originalLines.Count != declaredSize)
            {
                logger.Log("MAPR", $"Invalid map layout. Expected {declaredSize} lines, found {originalLines.Count}.");
                return;
            }

            var tempLines = new List<string>(originalLines);

            logger.Log("MAPR", $"Editing map '{mapName}.lmf' ({declaredSize}x{declaredSize}).");
            logger.Log("MAPR", "Enter new line values or press Enter to keep current. Line length must be exactly the map size.");

            for (int i = 0; i < declaredSize; i++)
            {
                Console.WriteLine($"[{i}] {tempLines[i]}");
                Console.Write($"New line {i} > ");
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    string raw = input.Trim().Replace(" ", "");
                    if (raw.Length != declaredSize)
                    {
                        logger.Log("MAPR", $"Line must contain exactly {declaredSize} characters. Found {raw.Length}. Skipping.");
                        continue;
                    }
                    string spaced = string.Join(' ', raw.ToCharArray());
                    tempLines[i] = spaced;
                }
            }

            // Delete original file first, then write new content
            File.Delete(path);
            File.WriteAllLines(path, new[] { declaredSize.ToString() }.Concat(tempLines));

            logger.Log("MAPR", $"Saved changes to '{mapName}.lmf'.");
        }

        private void ListMaps(ConsoleLogger logger)
        {
            if (!Directory.Exists(MapFolder))
            {
                logger.Log("MAPR", "Map folder does not exist.");
                return;
            }

            var mapFiles = Directory.GetFiles(MapFolder, "*.lmf");

            if (mapFiles.Length == 0)
            {
                logger.Log("MAPR", "No map files found.");
                return;
            }

            logger.Log("MAPR", $"Found {mapFiles.Length} map file(s):");

            foreach (var filePath in mapFiles.OrderBy(f => f))
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                try
                {
                    var lines = File.ReadAllLines(filePath);
                    if (lines.Length > 0 && int.TryParse(lines[0], out int size))
                    {
                        logger.Log("MAP", $"{fileName} ({size}x{size})");
                    }
                    else
                    {
                        logger.Log("MAP", $"{fileName} (invalid format)");
                    }
                }
                catch (Exception)
                {
                    logger.Log("MAP", $"{fileName} (error reading file)");
                }
            }
        }

        private void UpdateOrCreateMapClass(ConsoleLogger logger, string mapName)
        {
            string baseMapName = GetBaseMapName(mapName);
            string className = ToPascalCase(baseMapName);
            string mapVar = $"private readonly string {mapName} = \"{mapName}.lmf\";";
            string classPath = Path.Combine(MapClassFolder, $"{className}.cs");

            if (!File.Exists(classPath))
            {
                var template = new StringBuilder();
                template.AppendLine("using ProjectMethylamine.Source.Maps;");
                template.AppendLine();
                template.AppendLine("namespace ProjectMethylamine.Content.Maps");
                template.AppendLine("{");
                template.AppendLine($"    public class {className} : Map");
                template.AppendLine("    {");
                template.AppendLine($"        {mapVar}");
                template.AppendLine("    }");
                template.AppendLine("}");

                File.WriteAllText(classPath, template.ToString());
                logger.Log("MAPR", $"Created new map class file: {className}.cs");
                return;
            }

            var lines = File.ReadAllLines(classPath).ToList();
            if (lines.Any(l => l.Contains($"{mapName} =")))
            {
                logger.Log("MAPR", $"Map variable already exists in {className}.cs");
                return;
            }

            int insertIndex = lines.FindLastIndex(l => l.Trim().StartsWith("private readonly")) + 1;
            if (insertIndex == 0)
                insertIndex = lines.FindIndex(l => l.Contains("{")) + 1;

            lines.Insert(insertIndex, $"        {mapVar}");
            File.WriteAllLines(classPath, lines);
            logger.Log("MAPR", $"Added reference to '{mapName}.lmf' in {className}.cs");
        }

        private void DeleteMap(ConsoleLogger logger, string mapName)
        {
            string lmfPath = Path.Combine(MapFolder, $"{mapName}.lmf");
            if (!File.Exists(lmfPath))
            {
                logger.Log("MAPR", $"Map file '{mapName}.lmf' does not exist.");
            }
            else
            {
                File.Delete(lmfPath);
                logger.Log("MAPR", $"Deleted map file '{mapName}.lmf'.");
            }

            string baseMapName = GetBaseMapName(mapName);
            string className = ToPascalCase(baseMapName);
            string classPath = Path.Combine(MapClassFolder, $"{className}.cs");

            if (!File.Exists(classPath))
            {
                logger.Log("MAPR", $"Map class file '{className}.cs' not found.");
                return;
            }

            var lines = File.ReadAllLines(classPath).ToList();
            int removedCount = lines.RemoveAll(l => l.Contains($"\"{mapName}.lmf\""));

            if (removedCount == 0)
            {
                logger.Log("MAPR", $"No reference to '{mapName}.lmf' found in {className}.cs.");
                return;
            }

            bool hasMapFields = lines.Any(l => l.TrimStart().StartsWith("private readonly string"));
            if (!hasMapFields)
            {
                File.Delete(classPath);
                logger.Log("MAPR", $"Removed last map reference. Deleted {className}.cs entirely.");
            }
            else
            {
                File.WriteAllLines(classPath, lines);
                logger.Log("MAPR", $"Removed reference to '{mapName}.lmf' from {className}.cs.");
            }
        }

        // Converts dots to underscores for file names
        // e.g., "nexus.christmas" → "nexus_christmas"
        private string ConvertDotsToUnderscores(string mapName)
        {
            return mapName.Replace('.', '_');
        }

        // Gets the base map name for class generation (removes seasonal suffixes but keeps main name intact)
        // e.g., "void_cradle_ruins" → "void_cradle_ruins", "void_cradle_ruins_spring" → "void_cradle_ruins"
        private string GetBaseMapName(string mapName)
        {
            // Strip seasonal suffixes first
            string[] seasonalSuffixes = { "_spring", "_summer", "_autumn", "_winter" };
            foreach (string suffix in seasonalSuffixes)
            {
                if (mapName.EndsWith(suffix))
                {
                    mapName = mapName.Substring(0, mapName.Length - suffix.Length);
                    break;
                }
            }

            // Split on dot (not underscore)
            return mapName.Split('.')[0];
        }

        // Converts "void_cradle_ruins" -> "VoidCradleRuins"
        private string ToPascalCase(string input)
        {
            return string.Join("", input
                .Split('_', StringSplitOptions.RemoveEmptyEntries)
                .Select(part => char.ToUpper(part[0]) + part.Substring(1)));
        }
    }
}