# Changelog

All notable changes to Project Methylamine will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.0.2]

### Added
- **Content Generator GUI Application**
  - Windows Forms interface for content management
  - P.A.K.R. system with pack/unpack operations
  - M.A.P.R. system for map creation and management
  - Visual logging with ListBox display
  - Encryption/decryption checkboxes for secure operations
  - Real-time feedback and status updates
  - Data pack management with visual lists

- **Web Server Infrastructure**
  - Built-in HTTP/HTTPS web server
  - Flash/SWF game hosting capabilities
  - Crossdomain.xml support for Flash security
  - Static file serving with MIME type detection
  - CORS support for cross-origin requests
  - API endpoints (/status, /api/test)
  - Domain and port configuration

- **Enhanced Architecture**
  - Dual application structure (Server + Content Generator)
  - Shared utility library via Server.dll reference
  - Modular networking components (HttpServer, FileServer, WebServer)
  - Improved project organization with separate concerns
  - NuGet package generation capability
  - Enhanced build configuration with embedded debug symbols

- **Security Enhancements**
  - AES-256 encryption with CBC mode
  - SHA-256 key derivation
  - Cryptographically secure IV generation
  - PKCS7 padding implementation
  - Async file encryption/decryption methods

### Changed
- **Project Structure Reorganization**
  - Split into Server and Content Generator applications
  - Moved web assets to Server/Web directory
  - Consolidated utility classes under Server project
  - Updated namespace organization for better clarity
  - Improved build output structure with Binaries directory

- **Enhanced Command System**
  - Improved error handling and validation
  - Better user input feedback
  - Enhanced help system with detailed usage information
  - Streamlined command registration and execution

- **Map Management Improvements**
  - Enhanced seasonal map creation with better validation
  - Improved map editing with line-by-line interface
  - Better class generation and naming conventions
  - Enhanced file format validation and error handling

- **Content Packaging Enhancements**
  - Improved manifest generation with detailed metadata
  - Enhanced wildcard support for batch operations
  - Better archive validation and integrity checking
  - Improved encryption key management

### Fixed
- **Stability Improvements**
  - Better exception handling throughout the application
  - Improved file operation error recovery
  - Enhanced validation for user inputs
  - Fixed potential memory leaks in encryption operations

- **User Interface Fixes**
  - Improved Windows Forms designer integration
  - Better control layout and sizing
  - Enhanced event handling for GUI operations
  - Fixed potential threading issues

### Technical Details
- **Framework**: Upgraded to .NET 8.0 Windows
- **Dependencies**: Added Microsoft.CodeAnalysis packages
- **Build System**: Enhanced MSBuild configuration with multiple targets
- **Deployment**: Improved package generation and distribution

## [0.0.1] (Initial Release)

### Added
- **Core Command System**
  - Interactive terminal interface
  - Extensible command architecture with ICommand interface
  - Built-in command registration and execution system
  - Help system for all commands

- **Map Management System (`mapr`)**
  - Create square maps with customizable dimensions
  - Seasonal map variants (spring, summer, autumn, winter)
  - Interactive line-by-line map editing
  - Automatic C# class generation and updates
  - Level Map Format (.lmf) support
  - Map listing and validation

- **Content Packaging System (`pakr`)**
  - Directory compression with ZIP format
  - AES-256 encryption for secure archives
  - Batch operations with wildcard support
  - Manifest generation for archive metadata
  - Automatic source directory cleanup
  - Support for .zip and .pakr formats

- **Logging Infrastructure**
  - Console and file logging system
  - Customizable log levels and categories
  - Timestamp formatting
  - Silent mode and file-only logging options
  - Structured logging with consistent formatting

- **Abstract Base Classes**
  - Map base class for extensible map types
  - Item base class for game items (future expansion)
  - Consistent inheritance hierarchy

- **Cryptography System**
  - BitShiftCrypto utility class
  - AES-256 encryption with CBC mode
  - SHA-256 key derivation
  - Secure random IV generation
  - PKCS7 padding implementation

### Technical Foundation
- **Framework**: .NET 8.0 Console Application
- **Architecture**: Modular command-based system
- **File Formats**: 
  - .lmf (Level Map Format) for maps
  - .zip/.pakr for compressed archives
  - JSON manifests for archive metadata
- **Security**: AES-256 encryption with secure key derivation
- **Extensibility**: Interface-based command system for easy expansion

### Initial Commands
- `mapr -c <name> <size>`: Create new map
- `mapr -c /S <name> <size>`: Create seasonal map variants
- `mapr -e <name>`: Edit existing map
- `mapr -d <name>`: Delete map and references
- `mapr -L`: List all maps
- `pakr -p /D [/E] <source> <dest> <name>`: Pack directory
- `pakr -u /D [/e] <archive> <dest>`: Unpack archive
- `clear`: Clear console

### Known Limitations
- Console-only interface (GUI planned for v0.0.3 or v0.0.4)
- Basic error handling in some edge cases
- Limited configuration options
- No network/web server capabilities (added in v0.0.2)

---

## Version History Summary

- **v0.0.2**: GUI Application + Web Server + Enhanced Architecture
- **v0.0.1**: Initial Console Application with Core Features

## Upgrade Notes

### From v0.0.1 to v0.0.2
- **New Applications**: Two separate executables (Server.exe, Content Generator.exe)
- **Web Capabilities**: Built-in web server for hosting content
- **GUI Interface**: Windows Forms application for visual management
- **Enhanced Security**: Improved encryption with better key management
- **Breaking Changes**: Project structure reorganized, some file locations changed

## Development Roadmap

### Future Versions (v0.0.3+)
- Item management system (`itemz` command)
- Real-time content validation
- Network content distribution
- Configuration management
- Database integration
- Plugin architecture
- Performance optimizations
- Cross-platform support
- Advanced encryption options
- Web-based administration interface

---

*This changelog follows the [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) format and [Semantic Versioning](https://semver.org/spec/v2.0.0.html) principles.*