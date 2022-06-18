<p align="center">
    <img src="Images/Logo.png" width="400" max-width="90%" alt="StatiC#" />
</p>

<p align="center">
    <a href="https://docs.microsoft.com/en-us/dotnet/csharp/">
        <img src="https://img.shields.io/badge/C%23-10.0-blue?style=flat" alt="C# 10.0" />
    </a>
    <a href="https://dotnet.microsoft.com">
        <img src="https://img.shields.io/badge/.NET-6.0-blueviolet?style=flat" />
    </a>
    <a href="https://github.com/rolandbraun-dev/StatiCsharp">
        <img src="https://img.shields.io/static/v1?label=StatiC%23&message=0.1.0-alpha5&color=informational" />
    </a>
    <img src="https://img.shields.io/badge/Platforms-Win+Mac+Linux-green?style=flat" />
    <img src="https://img.shields.io/badge/Version-0.1.0--alpha2-green?style=flat" />
</p>

A theme for [StatiC#](https://github.com/rolandbraun-dev/StatiCsharp), a static webside generator written in C#.

## Installation

Add **Foundation** to your StatiC# project as a package reference in the .csproj file:

```
<ItemGroup>
    <PackageReference Include="StatiCsharp.Theme.Foundation" Version="0.1.0-alpha2" />
</ItemGroup>
``` 
Build your project to restore packages.  
You can then import Foundation at the top of your `Program.cs` and inject the theme to StatiC#'s website generating process by initializing a new FoundationHtmlFactory:

```C#
using StatiCsharp;
using Foundation;

var myAwesomeWebsite = new Website(
    url: "https://yourdomain.com",
    name: "My Awesome Website",
    description: @"Description of your website",
    language: "en-US",
    sections: "posts, about",
    source: @"/path/to/your/project"
    );

var theme = new FoundationHtmlFactory();

// Set up social icon here if needed.

website.Make(theme);
```

## Advanced settings

You can configure Foundation after initializing FoundationHtmlFactory to show social icons on the top right corner of your website. Set the property to the target page of your social networks.

```C#
theme.Email = "mail@yourdomain.com";
theme.Facebook = "https://facebook.com/yourName";
```

Foundation currently supports social icons for E-Mail, LinkedIn, GitHub, Facebook and Instagram.

 <img src="Images/social_icons_example.png" width="200" max-width="70%" alt="Example for social icons" />

To set legal noctive and/or privacy links in the footer use:

```C#
theme.LegalNotice = "/your/logalNoticePage";
theme.Privacy = "/your/privacyPage";
```

<img src="Images/legal_example.png" width="300" max-width="70%" alt="Example for legal notice" />